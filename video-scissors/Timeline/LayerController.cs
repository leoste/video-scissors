using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class LayerController : IFrameController, IControlController, IChildController
    {
        public static readonly int height = 40;
        public static readonly int controlsWidth = 72;

        private bool toggleLock;
        private bool toggleVisibility;
        private int id;
        private SliceController slice;
        private RectangleProvider rectangleProvider;

        private Rectangle layerRectangle;
        private Rectangle controlRectangle;
        private Color backColor;

        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }

        private List<ItemController> items;
        
        internal Panel ItemContentsPanel { get { return new Panel(); } }

        public int TimelineLength { get { return slice.TimelineLength; } }
        public float TimelineZoom { get { return slice.TimelineZoom; } }
        public int ProjectFramerate { get { return slice.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return slice.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return slice.ProjectFrameHeight; } }
        public bool IsLocked { get { return slice.IsLocked || toggleLock; } }
        public bool IsVisible { get { return slice.IsVisible || toggleVisibility; } }

        public Color BackColor {
            get { return backColor; }
            set
            {
                backColor = value;
                UpdateUI();
            }
        }
        public Color ForeColor { get; set; }

        public Rectangle Rectangle { get { return layerRectangle; } }
        public Rectangle ParentRectangle { get { return slice.ParentRectangle; } }

        public Rectangle ControlRectangle { get { return controlRectangle; } }

        public Rectangle ControlParentRectangle
        { get { return slice.ControlParentRectangle; } }

        public TimelineController Timeline { get { return slice.Timeline; } }

        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region();
                region.Union(layerRectangle);
                region.Union(controlRectangle);
                return region;
            }
        }

        public Region FullParentRegion
        { get { return slice.FullParentRegion; } }

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        private void Initialize(SliceController slice)
        {
            this.slice = slice;

            rectangleProvider = slice.RectangleProvider;
            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            slice.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            slice.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            slice.LocationChanged += Slice_LocationChanged;

            backColor = ColorProvider.GetRandomLayerColor();

            /*control = new LayerControl();
            control.BackColor = color;
            controlsPanel.Controls.Add(control);
            control.AddClicked += Control_AddClicked;
            control.RemoveClicked += Control_RemoveClicked;
            control.MoveUpClicked += Control_MoveUpClicked;
            control.MoveDownClicked += Control_MoveDownClicked;
            control.ToggleLockClicked += Control_ToggleLockClicked;
            control.ToggleVisibilityClicked += Control_ToggleVisibilityClicked;
            toggleLock = control.IsLockToggled;
            toggleVisibility = control.IsVisibilityToggled;

            content = new LayerContent();
            content.BackColor = color;
            contentsPanel.Controls.Add(content);*/

            SetId();

            items = new List<ItemController>();

            Random rnd = new Random();
            int r, o;

            items.Add(new ItemController(this, r = rnd.Next(0, 10), o = rnd.Next(5, 50)));
            System.Threading.Thread.Sleep(5);
            items.Add(new ItemController(this, r = rnd.Next(r + o + 10, r + o + 50), o = rnd.Next(10, 70)));
            System.Threading.Thread.Sleep(5);
            items.Add(new ItemController(this, r = rnd.Next(r + 0 + 5, r + o + 100), o = rnd.Next(5, 60)));
            System.Threading.Thread.Sleep(5);

            UpdateUI();
        }

        private void Slice_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged(e);

            UpdateContentUI();
            if (e.TopChanged) UpdateControlUI();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            UpdateCache();
            UpdateUI();
        }

        private void Timeline_TimelineZoomChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void Timeline_TimelineLengthChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void UpdateCache()
        {
            int offset = id * (height + SliceController.layerMargin);
            layerRectangle.X = slice.LayersRectangle.X;
            layerRectangle.Y = slice.LayersRectangle.Y + offset;
            layerRectangle.Width = slice.LayersRectangle.Width;
            layerRectangle.Height = height;

            controlRectangle.X = slice.ControlRectangle.X + SliceController.controlsWidth;
            controlRectangle.Y = slice.ControlRectangle.Y + offset;
            controlRectangle.Width = controlsWidth;
            controlRectangle.Height = height;
        }

        private void Control_ToggleVisibilityClicked(object sender, ToggleEventArgs e)
        {
            toggleVisibility = e.ToggleValue;
        }

        private void Control_ToggleLockClicked(object sender, ToggleEventArgs e)
        {
            toggleLock = e.ToggleValue;
        }

        private void Control_AddClicked(object sender, EventArgs e)
        {
            slice.CreateLayer(id + 1);
        }

        private void Control_RemoveClicked(object sender, EventArgs e)
        {
            slice.RemoveLayer(id);
        }

        private void Control_MoveUpClicked(object sender, EventArgs e)
        {
            if (id > 0) slice.SwapLayers(id, id - 1);
        }

        private void Control_MoveDownClicked(object sender, EventArgs e)
        {
            if (id < slice.LayerCount - 1) slice.SwapLayers(id, id + 1);
        }

        internal LayerController(SliceController slice)
        {
            id = slice.LayerCount;
            Initialize(slice);
        }

        internal LayerController(SliceController slice, int id)
        {
            this.id = id;
            Initialize(slice);
        }

        private void SetId()
        {
            /*controlsPanel.Controls.SetChildIndex(control, id);
            contentsPanel.Controls.SetChildIndex(content, id);*/
        }

        internal int GetId()
        {
            return id;
        }

        internal void SetId(int id)
        {
            this.id = id;
            SetId();
        }

        public void UpdateUI()
        {
            UpdateControlUI();
            UpdateContentUI();
        }

        public void UpdateControlUI()
        { rectangleProvider.InvalidateVerticalContainerRectangle(controlRectangle); }

        public void UpdateContentUI()
        { rectangleProvider.InvalidateContentContainerRectangle(layerRectangle); }
        
        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            bool redrawContent = e.ClipRectangle.IntersectsWith(layerRectangle);
            bool redrawControl = e.ClipRectangle.IntersectsWith(controlRectangle);

            if (redrawContent || redrawControl)
            {
                Region graphicsClip = e.Graphics.Clip;
                Brush brush = new SolidBrush(backColor);

                if (redrawContent)
                {
                    Region region = new Region(ParentRectangle);
                    region.Exclude(Timeline.Cursor.Rectangle);

                    foreach (ItemController item in items)
                    {
                        region.Exclude(item.Rectangle);
                    }

                    if (redrawControl)
                    {
                        Rectangle rectangle = ControlParentRectangle;
                        rectangle.X += SliceController.controlsWidth;
                        rectangle.Width -= SliceController.controlsWidth;
                        region.Union(rectangle);
                    }
                    e.Graphics.Clip = region;

                    e.Graphics.FillRectangle(brush, new Rectangle(
                        e.ClipRectangle.X, layerRectangle.Y,
                        e.ClipRectangle.Width, height));

                    //writes scroll position for debug
                    e.Graphics.DrawString(Rectangle.X.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.Black), ParentRectangle.X + 3, Rectangle.Y + 3);

                }

                if (redrawControl)
                {
                    e.Graphics.Clip = new Region(ControlParentRectangle);
                }

                e.Graphics.Clip = graphicsClip;
            }
        }

        public void ProcessFrame(Frame frame, int position)
        {
            int c = items.Count;
            for (int i = 0; i < c; i += 1)
            {
                if (items[i].IsOverlapping(position))
                {
                    items[i].ProcessFrame(frame, position);
                    break;
                }
            }
        }

        public void Dispose()
        {
            /*foreach (ItemController item in items)
            {
                item.Dispose();
            }

            controlsPanel.Controls.Remove(control);
            contentsPanel.Controls.Remove(content);
            control.Dispose();
            content.Dispose();*/
        }

        public bool IsPositionOkay(ItemController item)
        {
            bool okay = true;

            foreach (ItemController buddy in items)
            {
                if (item == buddy) continue;

                if (item.EndPosition > buddy.StartPosition && item.StartPosition < buddy.EndPosition)
                {
                    okay = false;
                    break;
                }
            }

            return okay;
        }

        public List<IController> GetChildren()
        {
            List<IController> children = new List<IController>();
            children.AddRange(items);
            return children;
        }

        public List<IController> GetChildrenDeep()
        {
            return GetChildren();
        }
    }
}
