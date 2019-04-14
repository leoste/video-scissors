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
    partial class CursorController : IController
    {
        private static readonly int cursorWidth = 2;

        private ControllerHandler handler;
        private CursorState state;

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

        public TimelineController ParentTimeline { get { return timeline; } }

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
            state = CursorState.Hover;
            this.timeline = timeline;
            rectangleProvider = timeline.RectangleProvider;
            handler = new ControllerHandler(this);

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

            rectangleProvider.MouseEnter += RectangleProvider_MouseEnter;
        }

        private void RectangleProvider_MouseEnter(object sender, EventArgs e)
        {
            if (GlobalMouseInfo.LastKnownHolder == MouseHolder.Menu)
            {
                GlobalMouseInfo.LastKnownHolder = MouseHolder.None;
                //need to create an item from menu

                Point screen = Control.MousePosition;
                Point mouse = rectangleProvider.PointToClient(screen);

                IController controller = handler.GetTargettedController(mouse);
                LayerController layer;
                if (controller is LayerController targetLayer)
                {
                    layer = targetLayer;
                }
                else if (controller is SliceController slice)
                {
                    layer = slice.GetChildren().First() as LayerController;
                }
                else return;

                int x = (int)Math.Round((mouse.X - timeline.Rectangle.Left) / timeline.TimelineZoom);
                layer.CreateItem(GlobalMouseInfo.MenuItemEffect, x, 10);
            }
        }

        private void RectangleProvider_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!handler.Active)
                {
                    IController controller = handler.GetTargettedController(e.Location);
                    state = handler.GetCursorState(e.Location, controller);

                    if (state == CursorState.Hover) return;
                    else handler.BeginControllerAction(e.Location, controller, state);

                    UpdateMouse(e);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (!handler.Active)
                {
                    IController controller = handler.GetTargettedController(e.Location);
                    handler.RightClick(controller);
                }
            }
        }

        private void RectangleProvider_MouseMove(object sender, MouseEventArgs e)
        {
            IController controller = handler.GetTargettedController(e.Location);

            if (state == CursorState.Hover)
            {
                CursorState protoState = handler.GetCursorState(e.Location, controller);

                switch (protoState)
                {
                    default:
                    case CursorState.Hover:
                        rectangleProvider.Cursor = Cursors.Default; break;
                    case CursorState.MoveItem:
                    case CursorState.MoveLayer:
                    case CursorState.MoveSlice:
                        rectangleProvider.Cursor = Cursors.SizeAll; break;
                    case CursorState.ResizeItemLeft:
                    case CursorState.ResizeItemRight:
                        rectangleProvider.Cursor = Cursors.SizeWE; break;
                }
            }

            UpdateMouse(e);
        }

        private void RectangleProvider_MouseUp(object sender, MouseEventArgs e)
        {
            if (handler.Active)
            {
                handler.EndControllerAction();
                state = CursorState.Hover;
            }
        }
        
        private void UpdateMouse(MouseEventArgs e)
        {
            if (rectangleProvider.ContainerRectangle.Contains(e.Location))
            {
                if (state == CursorState.Hover)
                {
                    if (ParentRectangle.Contains(e.Location)) UpdateCache(e.X);
                }
                else
                {
                    IController controller = handler.GetTargettedController(e.Location);
                    handler.UpdateControllerAction(e.Location, controller);

                    if (state == CursorState.MoveItem || state == CursorState.ResizeItemLeft || state == CursorState.ResizeItemRight)
                    {
                        CursorType[] types = new CursorType[2];
                        types[0] = types[1] = CursorType.ItemEdge;

                        if (state == CursorState.MoveItem)
                        {
                            types[0] = types[1] = CursorType.ItemEdge;
                        }
                        else
                        {
                            if (state == CursorState.ResizeItemLeft)
                            {
                                types[0] = CursorType.ItemResize;
                                types[1] = CursorType.ItemAnchor;
                            }
                            else if (state == CursorState.ResizeItemRight)
                            {
                                types[0] = CursorType.ItemAnchor;
                                types[1] = CursorType.ItemResize;
                            }
                        }

                        ItemController item = handler.ActionController as ItemController;

                        int x1 = item.Rectangle.X;
                        int x2 = item.Rectangle.Right;
                        UpdateCache(new int[] { x1, x2 }, types);
                    }
                }
                UpdateUI();
            }
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
            Region graphicsClip = e.Graphics.Clip;
            e.Graphics.Clip = new Region(rectangleProvider.HorizontalContainerRectangle);

            foreach (Cursor cursor in cursors)
            {
                if (e.ClipRectangle.IntersectsWith(cursor.Rectangle))
                {
                    Brush brush;
                    if (cursor.Type == CursorType.Main) brush = GlobalConfig.CursorMainBrush;
                    else if (cursor.Type == CursorType.ItemEdge) brush = GlobalConfig.CursorItemEdgeBrush;
                    else if (cursor.Type == CursorType.ItemResize) brush = GlobalConfig.CursorItemResizeBrush;
                    else if (cursor.Type == CursorType.ItemAnchor) brush = GlobalConfig.CursorItemAnchorBrush;
                    else brush = Brushes.Black;

                    e.Graphics.FillRectangle(brush, cursor.Rectangle);
                }
            }

            e.Graphics.Clip = graphicsClip;
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

        private Cursor CreateCursor(int x, CursorType type)
        {
            Rectangle rectangle = protoCursor;
            rectangle.X = x;
            return new Cursor(rectangle, type);
        }

        public void Delete()
        {
            timeline.TimelineZoomChanged -= Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged -= Timeline_TimelineLengthChanged;
            timeline.LocationChanged -= Timeline_LocationChanged;

            rectangleProvider.MouseDown -= RectangleProvider_MouseDown;
            rectangleProvider.MouseMove -= RectangleProvider_MouseMove;
            rectangleProvider.MouseUp -= RectangleProvider_MouseUp;

            rectangleProvider.Paint -= RectangleProvider_Paint;
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
            ItemResize,
            ItemAnchor
        }

        private enum CursorState
        {
            Hover,
            MoveItem,
            ResizeItemLeft,
            ResizeItemRight,
            MoveLayer,
            MoveSlice
        }
    }
}
