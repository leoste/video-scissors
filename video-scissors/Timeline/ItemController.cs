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
        private RectangleProvider timelineContent;

        private Rectangle itemRectangle;
        private Rectangle oldRectangle;
        private Color backColor;      

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
                        UpdateCache();
                        InvokeLocationChanged(new LocationChangeEventArgs(true, false));
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
                        UpdateCache();
                        InvokeSizeChanged();
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
                        UpdateCache();
                        InvokeSizeChanged();
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

        public Color BackColor { get { return backColor; } set { backColor = value; } }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Rectangle Rectangle
        { get { return itemRectangle; } }

        public Rectangle ParentRectangle
        { get { return timelineContent.ContentContainerRectangle; } }

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        internal ItemController(LayerController layer, int startPosition, int length)
        {
            backColor = Color.DarkGray;
            
            ui_change = 0;

            this.layer = layer;
            timelineContent = layer.TimelineRectangleProvider;
            UpdateCache();
            oldRectangle = itemRectangle;

            timelineContent.Paint += TimelineContent_Paint;
            timelineContent.Resize += TimelineContent_Resize;

            layer.TimelineZoomChanged += Layer_TimelineZoomChanged;
            layer.TimelineLengthChanged += Layer_TimelineLengthChanged;
            layer.LocationChanged += Layer_LocationChanged;

            /*contentsPanel = layer.ItemContentsPanel;
            content = new ItemContent();
            contentsPanel.Controls.Add(content);
            content.MouseDown += Content_MouseDown;
            content.MouseMove += Content_MouseMove;
            content.MouseUp += Content_MouseUp;
            content.MouseLeave += Content_MouseLeave;*/

            this.startPosition = startPosition;
            ItemLength = length;

            UpdateUI();
        }

        private void Layer_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged(e);
            UpdateUI();
        }

        private void Layer_TimelineZoomChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void Layer_TimelineLengthChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            UpdateCache();
            UpdateUI();
        }
        
        private void UpdateCache()
        {
            itemRectangle.X = (int)(startPosition * TimelineZoom) + layer.Rectangle.X;
            itemRectangle.Y = layer.Rectangle.Y;
            itemRectangle.Width = (int)(itemLength * TimelineZoom);
            itemRectangle.Height = layer.Rectangle.Height;
        }

        private byte ui_change;
        private int mouseOffsetX;
        private int originalStart;
        private int originalLength;
        
        //drag clip up or down to change layer
        private int mouseOffsetY;
        
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
            timelineContent.InvalidateContentContainerRectangle(oldRectangle);
            timelineContent.InvalidateContentContainerRectangle(itemRectangle);
            oldRectangle = itemRectangle;
        }

        public void Dispose()
        {
            /*contentsPanel.Controls.Remove(content);
            content.Dispose();*/
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(itemRectangle))
            {
                Region graphicsClip = e.Graphics.Clip;
                e.Graphics.Clip = new Region(ParentRectangle);

                Brush brush = new SolidBrush(backColor);
                e.Graphics.FillRectangle(brush, itemRectangle);

                e.Graphics.Clip = graphicsClip;
            }
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
