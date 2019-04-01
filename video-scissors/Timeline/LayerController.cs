using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Scissors.Objects;

namespace Scissors.Timeline
{
    class LayerController : IFrameController, IControlController, IChildController, IDraggableController
    {
        public static readonly int height = 40;
        public static readonly int controlsWidth = 66;
        public static readonly int dragWidth = 16;

        private bool toggleLock;
        private bool toggleVisibility;
        private int id;
        private SliceController slice;
        private RectangleProvider rectangleProvider;

        private Rectangle layerRectangle;
        private Rectangle controlRectangle;
        private Color backColor;
        private Color lockColor;
        private Color hiddenColor;
        private Color hiddenLockColor;

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
        public bool IsVisible { get { return slice.IsVisible && toggleVisibility; } }

        public Color BackColor {
            get { return backColor; }
            set
            {
                backColor = value;
                lockColor = ColorProvider.Mix(backColor, Color.DimGray, 0.8f);
                hiddenColor = ColorProvider.Mix(backColor, Color.Blue, 0.3f);
                hiddenLockColor = ColorProvider.Mix(lockColor, Color.Blue, 0.2f);
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

        public Color RealBackColor
        {
            get
            {
                if (IsLocked)
                {
                    if (IsVisible) return lockColor;
                    else return hiddenLockColor;
                }
                else if (IsVisible) return backColor;
                else return hiddenColor;
            }
        }

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
            toggleLock = false;
            toggleVisibility = true;

            rectangleProvider = slice.RectangleProvider;
            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            AddSliceEvents();

            BackColor = ColorProvider.GetRandomLayerColor();

            SetId();

            items = new List<ItemController>();
            
            lockButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                ButtonController.margin));
            lockButton.ButtonClicked += LockButton_ButtonClicked;
            lockButton.Icon = Properties.Resources.open_lock;

            visibilityButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width, 
                ButtonController.margin));
            visibilityButton.ButtonClicked += VisibilityButton_ButtonClicked;
            visibilityButton.Icon = Properties.Resources.open_eye;

            addLayerButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                ButtonController.margin * 3 + ButtonController.height));
            addLayerButton.ButtonClicked += AddLayerButton_ButtonClicked;
            addLayerButton.Icon = Properties.Resources.plus;

            removeLayerButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width,
                ButtonController.margin * 3 + ButtonController.height));
            removeLayerButton.ButtonClicked += RemoveLayerButton_ButtonClicked;
            removeLayerButton.Icon = Properties.Resources.minus;

            UpdateUI();
        }

        private void RemoveLayerButton_ButtonClicked(object sender, EventArgs e)
        {
            slice.DeleteLayer(this);
        }

        private void AddLayerButton_ButtonClicked(object sender, EventArgs e)
        {
            slice.CreateLayer(id + 1);
        }

        private void VisibilityButton_ButtonClicked(object sender, EventArgs e)
        {
            toggleVisibility = !toggleVisibility;

            if (toggleVisibility) visibilityButton.Icon = Properties.Resources.open_eye;
            else visibilityButton.Icon = Properties.Resources.closed_eye;

            UpdateUI();
        }

        private void LockButton_ButtonClicked(object sender, EventArgs e)
        {
            toggleLock = !toggleLock;

            if (toggleLock) lockButton.Icon = Properties.Resources.closed_lock;
            else lockButton.Icon = Properties.Resources.open_lock;

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
                Brush brush = new SolidBrush(RealBackColor);

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
            lockButton.ButtonClicked -= LockButton_ButtonClicked;
            visibilityButton.ButtonClicked -= VisibilityButton_ButtonClicked;
            addLayerButton.ButtonClicked -= AddLayerButton_ButtonClicked;            
            removeLayerButton.ButtonClicked -= RemoveLayerButton_ButtonClicked;
        }

        public override string ToString()
        {
            return $"id:{id}";
        }
    }
}
