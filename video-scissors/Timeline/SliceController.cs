using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class SliceController : IFrameController, IControlController, IChildController, IDraggableController
    {
        public static readonly int padding = 6;
        public static readonly int layerMargin = 2;
        public static readonly int controlsWidth = 66;
        public static readonly int dragWidth = 16;
                
        private int layersHeight = 40;
        private int top = 0;
        private bool toggleLock = false;
        private bool toggleVisibility = false;
        private int id;
        private TimelineController timeline;
        private RectangleProvider rectangleProvider;
                
        private List<LayerController> layers;
        private ButtonController lockButton;
        private ButtonController visibilityButton;
        private ButtonController addSliceButton;
        private ButtonController removeSliceButton;

        private Rectangle sliceRectangle;
        private Rectangle controlRectangle;
        private Color backColor;
        private Color lockColor;
        private Color hiddenColor;
        private Color hiddenLockColor;

        public int LayerCount { get { return layers.Count; } } 
        
        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }
        public bool IsLocked { get { return timeline.IsLocked || toggleLock; } }
        public bool IsVisible { get { return timeline.IsLocked || toggleVisibility; } }        
        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }
        public Color BackColor
        {
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

        public Rectangle LayersRectangle
        { get { return new Rectangle(sliceRectangle.X, sliceRectangle.Y + padding, sliceRectangle.Width, layersHeight); } }
                
        public Rectangle Rectangle
        { get { return sliceRectangle; } }

        public Rectangle ParentRectangle
        { get { return rectangleProvider.ContentContainerRectangle; } }

        public Rectangle ControlRectangle
        { get { return controlRectangle; } }

        public Rectangle ControlParentRectangle
        { get { return rectangleProvider.ControlContainerRectangle; } }

        public Rectangle MoveHandleRectangle
        { get { return new Rectangle(controlRectangle.X, controlRectangle.Y, dragWidth, controlRectangle.Height); } }

        public TimelineController ParentTimeline { get { return timeline; } }        

        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region(sliceRectangle);
                region.Union(controlRectangle);
                return region;
            }
        }

        public Region FullParentRegion
        {
            get
            {
                Region region = new Region(ParentRectangle);
                region.Union(ControlParentRectangle);
                return region;
            }
        }

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

        private void Initialize(TimelineController timeline)
        {
            layers = new List<LayerController>();

            this.timeline = timeline;
            toggleLock = false;
            toggleVisibility = true;
            
            rectangleProvider = timeline.RectangleProvider;
            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            timeline.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            timeline.LocationChanged += Timeline_LocationChanged;

            BackColor = ColorProvider.GetRandomSliceColor();

            SetId();

            CreateLayer();

            lockButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                padding + ButtonController.margin));
            lockButton.ButtonClicked += LockButton_ButtonClicked;
            lockButton.Icon = Properties.Resources.open_lock;

            visibilityButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width,
                padding + ButtonController.margin));
            visibilityButton.ButtonClicked += VisibilityButton_ButtonClicked;
            visibilityButton.Icon = Properties.Resources.open_eye;

            addSliceButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin,
                padding + ButtonController.margin * 3 + ButtonController.height));
            addSliceButton.ButtonClicked += AddSliceButton_ButtonClicked;
            addSliceButton.Icon = Properties.Resources.plus;

            removeSliceButton = new ButtonController(this, new Point(
                5 + dragWidth + ButtonController.margin * 3 + ButtonController.width,
                padding + ButtonController.margin * 3 + ButtonController.height));
            removeSliceButton.ButtonClicked += RemoveSliceButton_ButtonClicked;
            removeSliceButton.Icon = Properties.Resources.minus;

            UpdateCache();
            UpdateUI();
        }

        private void RemoveSliceButton_ButtonClicked(object sender, EventArgs e)
        {
            timeline.DeleteSlice(this);
        }

        private void AddSliceButton_ButtonClicked(object sender, EventArgs e)
        {
            timeline.CreateSlice(id + 1);
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

        private void Timeline_LocationChanged(object sender, LocationChangeEventArgs e)
        {            
            UpdateCache();
            InvokeLocationChanged(e);

            UpdateContentUI();
            if (e.TopChanged) UpdateControlUI();
        }

        private void Timeline_TimelineZoomChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateContentUI();
        }

        private void Timeline_TimelineLengthChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateContentUI();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            UpdateCache();
            UpdateUI();
        }

        private void UpdateCache()
        {
            top = 0;
            if (id > 0)
            {
                SliceController lastSlice = timeline.GetSlices(id - 1, 1).First();
                top = lastSlice.top + lastSlice.sliceRectangle.Height;
            }
            layersHeight = layers.Count * LayerController.height + Math.Max(layers.Count - 1, 0) * layerMargin;
            int height = padding * 2 + layersHeight;
            int offset = top - rectangleProvider.VerticalScroll;
            sliceRectangle.X = timeline.Rectangle.X;
            sliceRectangle.Y = ParentRectangle.Y + offset;
            sliceRectangle.Width = timeline.Rectangle.Width;
            sliceRectangle.Height = height;

            controlRectangle.X = ControlParentRectangle.X;
            controlRectangle.Y = ControlParentRectangle.Y + offset;
            controlRectangle.Width = ControlParentRectangle.Width;
            controlRectangle.Height = height;
        }
        
        internal SliceController(TimelineController timeline)
        {
            id = timeline.SliceCount;
            Initialize(timeline);
        }

        internal SliceController(TimelineController timeline, int id)
        {
            this.id = id;
            Initialize(timeline);
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
            this.id = id;
            SetId();
        }

        internal int GetLayerId(LayerController layer)
        {
            return layers.IndexOf(layer);
        }

        internal LayerController CreateLayer()
        {
            return CreateLayer(LayerCount);            
        }

        private void UpdateSlicesCache(List<SliceController> slices)
        {
            foreach (SliceController slice in slices)
            {
                slice.UpdateCache();
            }
        }

        private void UpdateSlicesUI(List<SliceController> slices)
        {
            foreach (SliceController slice in slices)
            {
                slice.UpdateUI();
                slice.InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            }
        }

        internal LayerController CreateLayer(int id)
        {
            LayerController layer = new LayerController(this, id);
            AddLayer(layer, id);

            UpdateCache();

            List<SliceController> slices = timeline.GetSlices(this.id, timeline.SliceCount - this.id);
            UpdateSlicesCache(slices);
            UpdateSlicesUI(slices);

            UpdateUI();

            InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            InvokeSizeChanged();

            return layer;
        }

        internal void DeleteLayer(int id)
        {
            DeleteLayer(layers[id]);
        }

        /// <summary>
        /// Removes the layer from slice and deletes it. To move layer to another slice use TransferLayer() instead.
        /// </summary>
        internal void DeleteLayer(LayerController layer)
        {
            CheckLayerExist(layer);
            RemoveLayer(layer);
            layer.Delete();

            if (layers.Count == 0) AddLayer(new LayerController(this));

            if (id < timeline.SliceCount)
            {
                List<SliceController> slices = timeline.GetSlices(id, timeline.SliceCount - id);
                UpdateSlicesCache(slices);
                UpdateSlicesUI(slices);
            }

            InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            InvokeSizeChanged();

            rectangleProvider.Invalidate();
        }

        private void AddLayer(LayerController layer, int id = 0)
        {
            layers.Insert(id, layer);

            for (int i = LayerCount - 1; i >= id; i -= 1)
            {
                layers[i].SetId(i);
            }            
        }

        private void RemoveLayer(LayerController layer)
        {
            int layerId = layer.GetId();
            layers.RemoveAt(layerId);

            for (int i = layerId; i < LayerCount; i += 1)
            {
                layers[i].SetId(i);
            }
        }

        internal void TransferLayer(LayerController layer, SliceController slice, int id = 0)
        {
            if (slice == this) throw new ArgumentException("New slice can't be this slice.");
            CheckLayerExist(layer);

            int slicesStart, slicesEnd;
            if (this.id < slice.id)
            {
                slicesStart = this.id;
                slicesEnd = slice.id;
            }
            else
            {
                slicesStart = slice.id;
                slicesEnd = this.id;
            }
            List<SliceController> slices = timeline.GetSlices(slicesStart, slicesEnd - slicesStart + 1);

            RemoveLayer(layer);
            slice.AddLayer(layer, id);

            foreach (SliceController timelineSlice in slices)
            {
                timelineSlice.UpdateCache();
            }

            if (Disowning != null) Disowning.Invoke(this, new DisownEventArgs(layer, slice));
                        
            foreach (SliceController timelineSlice in slices)
            {
                timelineSlice.UpdateUI();
                timelineSlice.InvokeLocationChanged(new LocationChangeEventArgs(false, true));
            }

            InvokeSizeChanged();
            slice.InvokeSizeChanged();
        }

        internal void SwapLayers(int id1, int id2)
        {
            layers[id1].SetId(id2);
            layers[id2].SetId(id1);

            LayerController layer1 = layers[id1];
            layers[id1] = layers[id2];
            layers[id2] = layer1;

            UpdateUI();
        }

        internal void SwapLayers(LayerController layer1, LayerController layer2)
        {
            SwapLayers(layer1.GetId(), layer2.GetId());
        }

        private void CheckLayerExist(LayerController layer)
        {
            if (!layers.Exists(x => x == layer)) throw new ArgumentException("This slice doesn't contain given layer.");
        }       

        public void UpdateUI()
        {            
            UpdateControlUI();
            UpdateContentUI();
        }

        private void UpdateControlUI()
        { rectangleProvider.InvalidateVerticalContainerRectangle(controlRectangle); }

        private void UpdateContentUI()
        { rectangleProvider.InvalidateContentContainerRectangle(sliceRectangle); }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            bool redrawContent = e.ClipRectangle.IntersectsWith(sliceRectangle);
            bool redrawControl = e.ClipRectangle.IntersectsWith(controlRectangle);

            if (redrawContent || redrawControl)
            {
                Region graphicsClip = e.Graphics.Clip;                
                Brush brush = new SolidBrush(RealBackColor);

                if (redrawContent)
                {
                    Region region = new Region(ParentRectangle);
                    region.Exclude(timeline.Cursor.FullOccupiedRegion);

                    if (redrawControl)
                    {
                        Rectangle rectangle = ControlParentRectangle;
                        rectangle.X += controlsWidth;
                        rectangle.Width -= controlsWidth;
                        region.Union(rectangle);                        
                    }

                    e.Graphics.Clip = region;

                    for (int i = 1; i < layers.Count; i += 1)
                    {
                        int y = sliceRectangle.Y + padding + LayerController.height * i + (i - 1) * layerMargin;
                        e.Graphics.FillRectangle(brush, new Rectangle(
                            e.ClipRectangle.X, y, e.ClipRectangle.Width, layerMargin));
                    }
                    
                    e.Graphics.FillRectangle(brush, new Rectangle(
                        e.ClipRectangle.X, sliceRectangle.Y, e.ClipRectangle.Width, padding));
                    
                    e.Graphics.FillRectangle(brush, new Rectangle(
                        e.ClipRectangle.X, sliceRectangle.Bottom - padding, e.ClipRectangle.Width, padding));

                    e.Graphics.Clip = graphicsClip;
                }

                if (redrawControl)
                {
                    Region region = new Region(ControlParentRectangle);
                    region.Exclude(lockButton.Rectangle);
                    region.Exclude(visibilityButton.Rectangle);
                    region.Exclude(addSliceButton.Rectangle);
                    region.Exclude(removeSliceButton.Rectangle);
                    e.Graphics.Clip = region;

                    e.Graphics.FillRectangle(brush, new Rectangle(
                      controlRectangle.X + dragWidth, controlRectangle.Y, 
                      controlsWidth - dragWidth, controlRectangle.Height));

                    e.Graphics.FillRectangle(Brushes.DimGray, MoveHandleRectangle);
                }

                e.Graphics.Clip = graphicsClip;
            }
        }

        public void ProcessFrame(Frame frame, int position)
        {
            Frame sliceFrame = new Frame(new Bitmap(ProjectFrameWidth, ProjectFrameHeight), false);

            foreach (LayerController layer in layers)
            {
                layer.ProcessFrame(sliceFrame, position);
            }

            frame.CombineWith(sliceFrame);
            sliceFrame.Dispose();
        }

        public List<IController> GetChildren()
        {
            List<IController> children = new List<IController>();
            children.AddRange(layers);
            return children;
        }

        public List<IController> GetChildrenDeep()
        {
            List<IController> children = new List<IController>();
            
            foreach (LayerController layer in layers)
            {
                children.Add(layer);
                children.AddRange(layer.GetChildrenDeep());
            }

            return children;
        }

        public void Delete()
        {            
            foreach (LayerController layer in layers) layer.Delete();
            lockButton.Delete();
            visibilityButton.Delete();
            addSliceButton.Delete();
            removeSliceButton.Delete();

            rectangleProvider.Paint -= TimelineContent_Paint;
            rectangleProvider.Resize -= TimelineContent_Resize;

            timeline.TimelineZoomChanged -= Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged -= Timeline_TimelineLengthChanged;
            timeline.LocationChanged -= Timeline_LocationChanged;

            lockButton.ButtonClicked -= LockButton_ButtonClicked;
            visibilityButton.ButtonClicked -= VisibilityButton_ButtonClicked;
            addSliceButton.ButtonClicked -= AddSliceButton_ButtonClicked;
            removeSliceButton.ButtonClicked -= RemoveSliceButton_ButtonClicked;
        }

        public override string ToString()
        {
            return $"id:{id}";
        }
    }
}
