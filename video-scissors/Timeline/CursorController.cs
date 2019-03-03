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
    class CursorController : IController
    {
        private static readonly int cursorWidth = 2;
        
        private int oldLeft;
        private int oldWidth;
        private bool oldLockToControl;
        private bool lockToControl;

        private TimelineController timeline;
        private RectangleProvider rectangleProvider;

        private Rectangle mainCursorRectangle;
        private Rectangle oldRectangle;
                        
        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        public Rectangle Rectangle
        { get { return mainCursorRectangle; } }

        public Rectangle ParentRectangle
        { get { return rectangleProvider.HorizontalContainerRectangle; } }

        public TimelineController Timeline { get { return timeline; } }

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        public CursorController(TimelineController timeline)
        {
            oldLeft = 0;
            oldWidth = 0;
            oldLockToControl = false;
            lockToControl = false;

            this.timeline = timeline;
            rectangleProvider = timeline.RectangleProvider;

            mainCursorRectangle = new Rectangle();
            mainCursorRectangle.X = rectangleProvider.HorizontalContainerRectangle.X;
            UpdateCache();
            oldRectangle = mainCursorRectangle;
            
            timeline.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            timeline.LocationChanged += Timeline_LocationChanged;

            rectangleProvider.MouseDown += RectangleProvider_MouseDown;
            rectangleProvider.MouseMove += RectangleProvider_MouseMove;
            rectangleProvider.MouseUp += RectangleProvider_MouseUp;

            rectangleProvider.Paint += RectangleProvider_Paint;
        }

        private void RectangleProvider_MouseDown(object sender, MouseEventArgs e)
        {
            IController controller = GetTargettedController(e.Location);

            //add logic to behave differently depending on if control is item or layer etc
            //to allow for repositioning by dragging stuff
        }

        private void RectangleProvider_MouseMove(object sender, MouseEventArgs e)
        {
            if (rectangleProvider.HorizontalContainerRectangle.Contains(e.Location))
            {
                UpdateCache(e.X);
                UpdateUI();
            }
        }

        private void RectangleProvider_MouseUp(object sender, MouseEventArgs e)
        {
            
        }

        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            lockToControl = true;
        }

        private void Item_MouseUp(object sender, MouseEventArgs e)
        {
            lockToControl = false;

            Control control = sender as Control;

            control.Invalidate(new Rectangle(0, 0, cursorWidth, control.Height));
            control.Invalidate(new Rectangle(control.Width - cursorWidth, 0, cursorWidth, control.Height));
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            int cx;
            int width = oldWidth;
            if (lockToControl)
            {
                cx = 0;
                width = control.Width - cursorWidth;
            }
            else cx = e.X;

            int left = /*GetCursorAbsoluteX(control, cx);*/0;
            int diff;
            if (left == oldLeft) diff = width - oldWidth;
            else diff = left - oldLeft;

            int diffw;
            int abs = Math.Abs(diff);
            if (abs < cursorWidth)
            {
                diffw = abs;
                if (diff < 0) diff = -cursorWidth;
            }
            else diffw = cursorWidth;

            /*foreach (KeyValuePair<Control, ControlInfo> pair in generations)
            {
                int x = GetCursorRelativeX(pair.Key, left);
                if (abs > 0)
                {
                    pair.Key.Invalidate(new Rectangle(x - diff, 0, diffw, pair.Key.Height));
                    if (lockToControl || oldLockToControl)
                    {
                        pair.Key.Invalidate(new Rectangle(x - diff + width, 0, diffw, pair.Key.Height));
                    }
                }
                pair.Key.Update();
                if (lockToControl)
                {
                    pair.Value.Graphics.FillRectangle(GlobalConfig.CursorMoveItemBrush, x, 0, cursorWidth, pair.Key.Height);
                    pair.Value.Graphics.FillRectangle(GlobalConfig.CursorMoveItemBrush, x + width, 0, cursorWidth, pair.Key.Height);
                }
                else pair.Value.Graphics.FillRectangle(GlobalConfig.CursorRegularBrush, x, 0, cursorWidth, pair.Key.Height);
            }*/

            oldLeft = left;
            oldWidth = width;
            oldLockToControl = lockToControl;
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            /*Control control = sender as Control;
            if (generations.TryGetValue(control, out ControlInfo info))
            {                
                generations[control] = new ControlInfo(info.Level, Graphics.FromHwnd(control.Handle));
                info.Dispose();
            }*/
        }

        private void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            //AddEventsDeep(e.Control);
        }

        private void RemoveEventsDeep(Control control)
        {
            /*generations.Remove(control);
            control.ControlAdded -= Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;
            control.MouseMove -= Control_MouseMove;
            control.Resize -= Control_Resize;

            if (control is ItemContent)
            {
                ItemContent item = control as ItemContent;
                item.MouseDown -= Item_MouseDown;
                item.MouseUp -= Item_MouseUp;
            }

            foreach (Control child in control.Controls)
            {
                RemoveEventsDeep(child);
            }*/
        }

        private void Control_ControlRemoved(object sender, ControlEventArgs e)
        {
            RemoveEventsDeep(e.Control);
        }

        /*private int GetCursorAbsoluteX(Control control, int x)
        {
            if (generations.TryGetValue(control, out ControlInfo info))
            {

                if (info.Level > 0) x += GetCursorAbsoluteX(control.Parent, control.Left);

                return x;
            }
            else throw new ArgumentException();
        }*/

        /*private int GetCursorRelativeX(Control control, int x)
        {
            return x - GetCursorAbsoluteX(control, 0);
        }*/

        public void Dispose()
        {
            /*foreach (KeyValuePair<Control, ControlInfo> pair in generations)
            {
                pair.Value.Dispose();
            }*/
        }

        private void UpdateCache(int x)
        {
            mainCursorRectangle.X = x;
            mainCursorRectangle.Y = rectangleProvider.ContainerRectangle.Y;
            mainCursorRectangle.Width = cursorWidth;
            mainCursorRectangle.Height = rectangleProvider.ContainerRectangle.Height;            
        }

        private void UpdateCache()
        {
            UpdateCache(mainCursorRectangle.X);
        }

        public void UpdateUI()
        {
            rectangleProvider.Invalidate(oldRectangle);
            rectangleProvider.Invalidate(mainCursorRectangle);
            oldRectangle = mainCursorRectangle;
        }

        private void RectangleProvider_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(mainCursorRectangle))
            {
                e.Graphics.FillRectangle(GlobalConfig.CursorRegularBrush, mainCursorRectangle);
            }
        }

        class ControlInfo : IDisposable
        {
            public int Level { get; private set; }
            public Graphics Graphics { get; private set; }

            public ControlInfo(int level, Graphics graphics)
            {
                Level = level;
                Graphics = graphics;
            }

            public void Dispose()
            {
                Graphics.Dispose();
            }
        }

        private void Timeline_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            InvokeLocationChanged(e);
        }

        private void Timeline_TimelineZoomChanged(object sender, EventArgs e)
        {
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
        }

        private void Timeline_TimelineLengthChanged(object sender, EventArgs e)
        {
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
        }

        private IController GetTargettedController(Point mouseLocation)
        {
            if (!ParentRectangle.Contains(mouseLocation)) return null;
            
            List<IController> timelineChildren = timeline.GetChildren();
            foreach (IController timelineChild in timelineChildren)
            {
                if (timelineChild.Rectangle.Contains(mouseLocation))
                {
                    if (timelineChild is SliceController)
                    {
                        List<IController> sliceChildren = (timelineChild as SliceController).GetChildren();
                        foreach (IController sliceChild in sliceChildren)
                        {
                            if (sliceChild.Rectangle.Contains(mouseLocation))
                            {
                                if (sliceChild is LayerController)
                                {
                                    List<IController> layerChildren = (sliceChild as LayerController).GetChildren();
                                    foreach (IController layerChild in layerChildren)
                                    {
                                        if (layerChild.Rectangle.Contains(mouseLocation))
                                        {
                                            if (layerChild is ItemController) return layerChild;
                                        }
                                    }
                                    return sliceChild;
                                }
                            }
                        }
                        return timelineChild;
                    }
                    else if (timelineChild is RulerController)
                    {
                        return timelineChild;
                    }
                }
            }
            return timeline;
        }
    }
}
