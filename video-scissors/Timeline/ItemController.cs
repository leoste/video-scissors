using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scissors.Config;

namespace Scissors.Timeline
{
    internal class ItemController : IFrameController
    {
        private LayerController layer;
        private Panel contentsPanel;

        private ItemContent content;
        
        private int oldItemLength;
        private int oldStartPosition;
        private float oldZoom;        

        private int startPosition;
        private int endPosition;
        private int itemLength;
        
        internal int StartPosition
        {
            get { return startPosition; }
            set
            {
                if (value >= 0)
                {
                    if (value + itemLength <= layer.TimelineLength)
                    {
                        startPosition = value;
                        endPosition = startPosition + itemLength;
                        UpdateUI();
                    }
                    else throw Exceptions.EndTooBigException;
                }
                else throw Exceptions.StartTooSmallException;
            }
        }

        internal int EndPosition
        {
            get { return endPosition; }
            set
            {
                if (value < layer.TimelineLength)
                {
                    if (value > startPosition)
                    {
                        endPosition = value;
                        itemLength = endPosition - startPosition;
                        UpdateUI();
                    }
                    else throw Exceptions.EndTooSmallException;
                }
                else throw Exceptions.EndTooBigException;
            }
        }

        internal int ItemLength
        {
            get { return endPosition - startPosition; }
            set
            {
                if (value > 0)
                {
                    if (startPosition + value <= layer.TimelineLength)
                    {
                        itemLength = value;
                        endPosition = startPosition + value;
                        UpdateUI();
                    }
                    else throw Exceptions.EndTooBigException;
                }
                else throw Exceptions.LengthTooSmallException;
            }
        }

        public int TimelineLength { get { return layer.TimelineLength; } }
        public int ProjectFramerate { get { return layer.ProjectFramerate; } }
        public float TimelineZoom { get { return layer.TimelineZoom; } }
        public int ProjectFrameWidth { get { return layer.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return layer.ProjectFrameHeight; } }

        public RectangleProvider TimelineRectangleProvider => throw new NotImplementedException();

        public Color BackColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Rectangle Rectangle => throw new NotImplementedException();

        public Rectangle ParentRectangle => throw new NotImplementedException();

        internal ItemController(LayerController layer, int startPosition, int length)
        {
            oldZoom = -1;
            oldItemLength = -1;
            oldStartPosition = -1;
            ui_change = 0;

            this.layer = layer;
            contentsPanel = layer.ItemContentsPanel;
            content = new ItemContent();
            contentsPanel.Controls.Add(content);
            content.MouseDown += Content_MouseDown;
            content.MouseMove += Content_MouseMove;
            content.MouseUp += Content_MouseUp;
            content.MouseLeave += Content_MouseLeave;

            this.startPosition = startPosition;
            ItemLength = length;

            UpdateUI();
        }

        private byte ui_change;
        private int mouseOffsetX;
        private int originalStart;
        private int originalLength;
        
        //drag clip up or down to change layer
        private int mouseOffsetY;

        public event EventHandler SizeChanged;
        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;
        public event EventHandler LocationChanged;

        private void Content_MouseDown(object sender, MouseEventArgs e)
        {
            if (layer.IsLocked) return;

            Control control = sender as Control;

            if (e.X > control.Width - GlobalConfig.ItemResizeHandleWidth * 2)
            {
                ui_change = 2;
            }
            else if (e.X < GlobalConfig.ItemResizeHandleWidth)
            {
                ui_change = 3;
            }
            else
            {
                ui_change = 1;
            }

            mouseOffsetX = e.X;
            mouseOffsetY = e.Y;
            originalStart = startPosition;
            originalLength = itemLength;
        }

        private void Content_MouseUp(object sender, MouseEventArgs e)
        {
            ui_change = 0;

            if (!layer.IsPositionOkay(this))
            {
                StartPosition = originalStart;
                ItemLength = originalLength;
            }
        }

        private void Content_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;

            if (e.X > control.Width - GlobalConfig.ItemResizeHandleWidth * 2)
                control.Cursor = Cursors.SizeWE;
            else if (e.X < GlobalConfig.ItemResizeHandleWidth)
                control.Cursor = Cursors.SizeWE;
            else
                control.Cursor = Cursors.SizeAll;

            if (ui_change == 0) return;

            int x = (int)((e.X - mouseOffsetX) / TimelineZoom);

            if (ui_change == 1)
            {
                try { StartPosition = x + startPosition; } catch { }
            }
            else if (ui_change == 2)
            {
                try { ItemLength = x + originalLength; } catch { }
            }
            else if (ui_change == 3)
            {
                int oldLength = itemLength;
                int oldStart = startPosition;
                try
                {
                    ItemLength = -x + itemLength;
                    StartPosition = x + startPosition;
                }
                catch
                {
                    ItemLength = oldLength;
                    StartPosition = oldStart;
                }
            }
        }

        private void Content_MouseLeave(object sender, EventArgs e)
        {
            if (ui_change == 0) return;
        }

        public void UpdateUI()
        {
            bool updatePos = false;
            bool updateWidth = false;

            if (startPosition != oldStartPosition)
            {
                oldStartPosition = startPosition;
                updatePos = true;
            }
            if (itemLength != oldItemLength)
            {
                oldItemLength = ItemLength;
                updateWidth = true;
            }
            if (TimelineZoom != oldZoom)
            {
                oldZoom = TimelineZoom;
                updatePos = true;
                updateWidth = true;
            }

            if (updatePos)
            {
                content.Left = (int)(startPosition * TimelineZoom);
            }

            if (updateWidth)
            {
                content.Width = (int)(itemLength * TimelineZoom);
            }
        }

        public void Dispose()
        {
            contentsPanel.Controls.Remove(content);
            content.Dispose();
        }

        public void ProcessFrame(Frame frame, int position)
        {
            
        }
        
        public int GetRelativePosition(int timelinePosition)
        {
            return timelinePosition - startPosition;
        }

        public bool IsOverlapping(int timelinePosition)
        {
            return timelinePosition >= startPosition && timelinePosition < endPosition;
        }
    }
}
