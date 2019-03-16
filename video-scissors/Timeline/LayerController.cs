using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class LayerController : IFrameController, IControlController, IChildController, IDraggableController
    {
        public static readonly int height = 40;
        public static readonly int controlsWidth = 56;
        public static readonly int dragWidth = 16;

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
        private ButtonController lockButton;
        private ButtonController visibilityButton;
        private ButtonController addLayerButton;
        private ButtonController removeLayerButton;
        
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

        public Rectangle MoveHandleRectangle
        { get { return new Rectangle(controlRectangle.X, controlRectangle.Y, dragWidth, controlRectangle.Height); } }

        public TimelineController ParentTimeline { get { return slice.ParentTimeline; } }
        public SliceController ParentSlice { get { return slice; } }

        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region(layerRectangle);
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
        public event EventHandler<DisownEventArgs> Disowning;

        private void Initialize(SliceController slice)
        {
            this.slice = slice;

            rectangleProvider = slice.RectangleProvider;
            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            AddSliceEvents();

            backColor = ColorProvider.GetRandomLayerColor();

            SetId();

            items = new List<ItemController>();
            Random rnd = new Random();
            items.Add(new ItemController(this, rnd.Next(0, 20), rnd.Next(3, 13)));
            System.Threading.Thread.Sleep(5);
            items.Add(new ItemController(this, rnd.Next(45, 50), rnd.Next(6, 30)));
            System.Threading.Thread.Sleep(5);
            items.Add(new ItemController(this, rnd.Next(70, 90), rnd.Next(10, 15)));
            System.Threading.Thread.Sleep(5);
            
            lockButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                ButtonController.margin));
            visibilityButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width, 
                ButtonController.margin));
            addLayerButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                ButtonController.margin * 3 + ButtonController.height));
            removeLayerButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width,
                ButtonController.margin * 3 + ButtonController.height));

            UpdateUI();
        }

        private void AddSliceEvents()
        {
            slice.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            slice.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            slice.LocationChanged += Slice_LocationChanged;
            slice.Disowning += Slice_Disowning;
        }

        private void RemoveSliceEvents()
        {
            slice.TimelineZoomChanged -= Timeline_TimelineZoomChanged;
            slice.TimelineLengthChanged -= Timeline_TimelineLengthChanged;
            slice.LocationChanged -= Slice_LocationChanged;
            slice.Disowning -= Slice_Disowning;
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
            controlRectangle.Y = layerRectangle.Y;
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
            slice.DeleteLayer(id);
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
            UpdateCache();
            UpdateUI();
        }

        internal int GetId()
        {
            return id;
        }

        internal void SetId(int id)
        {
            if (this.id != id)
            {
                this.id = id;
                SetId();
                InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            }
        }

        internal void TransferItem(ItemController item, LayerController layer)
        {
            if (layer == this) throw new ArgumentException("Layer can't be this layer.");
            if (!items.Exists(x => x == item)) throw new ArgumentException("This layer doesn't contain given item.");            

            RectangleProvider.InvalidateContentContainerRectangle(item.Rectangle);

            items.Remove(item);
            layer.items.Add(item);
            if (Disowning != null) Disowning.Invoke(this, new DisownEventArgs(item, layer));

            RectangleProvider.InvalidateContentContainerRectangle(item.Rectangle);
        }

        internal void AddItem(ItemController item)
        {
            items.Add(item);
            RectangleProvider.InvalidateContentContainerRectangle(item.Rectangle);
        }

        internal void RemoveItem(ItemController item)
        {
            items.Remove(item);
            RectangleProvider.InvalidateContentContainerRectangle(item.Rectangle);
        }

        private void Slice_Disowning(object sender, DisownEventArgs e)
        {
            if (e.DisownedChild == this)
            {
                RemoveSliceEvents();
                slice = e.NewParent as SliceController;
                AddSliceEvents();
                id = slice.GetLayerId(this);
                UpdateCache();
                UpdateUI();
                InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            }
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
                    region.Exclude(ParentTimeline.Cursor.FullOccupiedRegion);

                    foreach (ItemController item in items)
                    {
                        region.Exclude(item.Rectangle);
                    }

                    if (redrawControl)
                    {
                        int offset = SliceController.controlsWidth + dragWidth;
                        Rectangle rectangle = ControlParentRectangle;
                        rectangle.X += offset;
                        rectangle.Width -= offset;
                        region.Union(rectangle);
                        region.Exclude(lockButton.Rectangle);
                        region.Exclude(visibilityButton.Rectangle);
                        region.Exclude(addLayerButton.Rectangle);
                        region.Exclude(removeLayerButton.Rectangle);
                    }
                    e.Graphics.Clip = region;

                    e.Graphics.FillRectangle(brush, new Rectangle(
                        e.ClipRectangle.X, layerRectangle.Y,
                        e.ClipRectangle.Width, height));                    
                }

                if (redrawControl)
                {
                    e.Graphics.Clip = new Region(ControlParentRectangle);
                    e.Graphics.FillRectangle(Brushes.DimGray, MoveHandleRectangle);

                    //for debugging
                    e.Graphics.DrawString(ToString(), SystemFonts.DefaultFont, Brushes.Black, new Point(MoveHandleRectangle.Right + 3, controlRectangle.Y + 3));
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

        public void Delete()
        {
            foreach (ItemController item in items) item.Delete();
            lockButton.Delete();
            visibilityButton.Delete();
            addLayerButton.Delete();
            removeLayerButton.Delete();
            rectangleProvider.Paint -= TimelineContent_Paint;
            rectangleProvider.Resize -= TimelineContent_Resize;
            RemoveSliceEvents();
        }

        public override string ToString()
        {
            return $"id:{id}";
        }
    }
}
