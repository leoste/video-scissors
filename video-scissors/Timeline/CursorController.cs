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
        private int offset;
        private CursorState state;
        private ItemController targettedItem;

        private TimelineController timeline;
        private RectangleProvider rectangleProvider;

        private Rectangle protoCursor;
        private List<Cursor> cursors;
        private List<Cursor> oldCursors;

        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }

        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }

        //will return a rectangle containing all cursors in the future
        public Rectangle Rectangle
        {
            get
            {
                Rectangle rectangle = new Rectangle();

                rectangle.X = cursors[0].Rectangle.X;
                rectangle.Y = cursors[0].Rectangle.Y;
                rectangle.Height = cursors[0].Rectangle.Height;
                rectangle.Width = cursors.Last().Rectangle.Right - rectangle.X;

                return rectangle;
            }
        }

        public Rectangle ParentRectangle
        { get { return rectangleProvider.HorizontalContainerRectangle; } }

        //will return a region containing all cursors in the future
        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region(cursors[0].Rectangle);
                
                for (int i = 1; i < cursors.Count; i += 1)
                {
                    region.Union(cursors[i].Rectangle);
                }

                return region;
            }
        }

        public Region FullParentRegion
        { get { return new Region(ParentRectangle); } }

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
            //oldLockToControl = false;
            state = CursorState.Hover;

            this.timeline = timeline;
            rectangleProvider = timeline.RectangleProvider;

            protoCursor = new Rectangle();
            cursors = new List<Cursor>();
            oldCursors = new List<Cursor>();
            UpdateCache();
            oldCursors.AddRange(cursors);


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

            if (controller is ItemController)
            {
                targettedItem = controller as ItemController;
                state = CalculateCursorState(targettedItem, e.X);                
                oldLeft = targettedItem.StartPosition;
                offset = e.X - controller.Rectangle.X;
            }
            else return;
            
            UpdateMouse(e);

            //add logic to behave differently depending on if control is item or layer etc
            //to allow for repositioning by dragging stuff
        }

        private void RectangleProvider_MouseMove(object sender, MouseEventArgs e)
        {
            IController controller = GetTargettedController(e.Location);

            if (controller is ItemController)
            {
                CursorState protoState = CalculateCursorState(controller as ItemController, e.X);
                if (protoState == CursorState.ResizeItemLeft || protoState == CursorState.ResizeItemRight)
                    rectangleProvider.Cursor = Cursors.SizeWE;
                else rectangleProvider.Cursor = Cursors.SizeAll;
            }
            else rectangleProvider.Cursor = Cursors.Default;

            UpdateMouse(e);
        }

        private void RectangleProvider_MouseUp(object sender, MouseEventArgs e)
        {
            state = CursorState.Hover;
        }
        
        private void UpdateMouse(MouseEventArgs e)
        {
            if (rectangleProvider.HorizontalContainerRectangle.Contains(e.Location))
            {
                if (state == CursorState.Hover)
                {
                    UpdateCache(e.X);
                }
                else
                {
                    int x1 = targettedItem.Rectangle.X;
                    int x2 = targettedItem.Rectangle.Right;
                    CursorType[] types = new CursorType[2];
                    types[0] = types[1] = CursorType.ItemEdge;

                    if (state == CursorState.ResizeItemLeft) types[0] = CursorType.ItemResize;
                    else if (state == CursorState.ResizeItemRight) types[1] = CursorType.ItemResize;

                    UpdateCache(new int[] { x1, x2 }, types);
                }
                UpdateUI();
            }
        }

        private CursorState CalculateCursorState(ItemController item, int x)
        {
            if (x - item.Rectangle.X <= GlobalConfig.ItemResizeHandleWidth)
                return CursorState.ResizeItemLeft;
            else if (x >= item.Rectangle.Right - GlobalConfig.ItemResizeHandleWidth)
                return CursorState.ResizeItemRight;
            else
                return CursorState.MoveItem;
        }

        private void UpdateCache(int[] xs, CursorType[] types)
        {
            protoCursor.X = 0;
            protoCursor.Y = rectangleProvider.ContainerRectangle.Y;
            protoCursor.Width = cursorWidth;
            protoCursor.Height = rectangleProvider.ContainerRectangle.Height;

            oldCursors.AddRange(cursors);
            cursors.Clear();

            for (int i = 0; i < xs.Length; i += 1)
            {
                Rectangle rect = protoCursor;
                rect.X = xs[i];
                cursors.Add(new Cursor(rect, types[i]));
            }
        }

        private void UpdateCache(int x)
        {
            UpdateCache(new int[] { x }, new CursorType[] { CursorType.Main });
        }

        private void UpdateCache()
        {
            UpdateCache(new int[] { ParentRectangle.X }, new CursorType[] { CursorType.Main });
        }

        public void UpdateUI()
        {
            foreach (Cursor cursor in oldCursors)
            {
                rectangleProvider.Invalidate(cursor.Rectangle);
            }
            oldCursors.Clear();

            foreach (Cursor cursor in cursors)
            {
                rectangleProvider.Invalidate(cursor.Rectangle);
            }
        }

        private void RectangleProvider_Paint(object sender, PaintEventArgs e)
        {
            foreach (Cursor cursor in cursors)
            {
                if (e.ClipRectangle.IntersectsWith(cursor.Rectangle))
                {
                    Brush brush;
                    if (cursor.Type == CursorType.Main) brush = GlobalConfig.CursorRegularBrush;
                    else if (cursor.Type == CursorType.ItemEdge) brush = GlobalConfig.CursorMoveItemBrush;
                    else brush = Brushes.Black;

                    e.Graphics.FillRectangle(brush, cursor.Rectangle);
                }
            }
        }

        private void Timeline_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            UpdateCache(cursors.First().Rectangle.X);
            UpdateUI();
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

        private Cursor CreateCursor(int x, CursorType type)
        {
            Rectangle rectangle = protoCursor;
            rectangle.X = x;
            return new Cursor(rectangle, type);
        }

        private class Cursor
        {
            public Cursor(Rectangle rectangle, CursorType type)
            {
                Rectangle = rectangle;
                Type = type;
            }

            public Rectangle Rectangle { get; set; }
            public CursorType Type { get; set; }
        }

        private enum CursorType
        {
            Main,
            ItemEdge,
            ItemResize
        }

        private enum CursorState
        {
            Hover,
            MoveItem,
            ResizeItemLeft,
            ResizeItemRight
        }
    }
}
