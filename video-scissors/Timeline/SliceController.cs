using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class SliceController : IFrameController, IControlController, IChildController
    {
        public static readonly int padding = 3;
        public static readonly int layerMargin = 2;
        public static readonly int controlsWidth = 72;
        
        private int layersHeight = 40;

        private bool toggleLock = false;
        private bool toggleVisibility = false;
        private int id;
        private TimelineController timeline;
        private RectangleProvider rectangleProvider;
                
        private List<LayerController> layers;
        
        private Rectangle sliceRectangle;
        private Rectangle controlRectangle;
        private Color backColor;

        internal int LayerCount { get { return layers.Count; } }
                
        internal FlowLayoutPanel LayerControlsPanel { get { return new FlowLayoutPanel(); } }
        internal FlowLayoutPanel LayerContentsPanel { get { return new FlowLayoutPanel(); } }
        
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

        public TimelineController Timeline { get { return timeline; } }

        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region();
                region.Union(sliceRectangle);
                region.Union(controlRectangle);
                return region;
            }
        }

        public Region FullParentRegion
        {
            get
            {
                Region region = new Region();
                region.Union(ControlParentRectangle);
                region.Union(ParentRectangle);
                return region;
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

        private void Initialize(TimelineController timeline)
        {
            layers = new List<LayerController>();

            this.timeline = timeline;

            rectangleProvider = timeline.RectangleProvider;
            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            timeline.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            timeline.LocationChanged += Timeline_LocationChanged;

            BackColor = ColorProvider.GetRandomSliceColor();
            
            /*control = new SliceControl();
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

            content = new SliceContent();
            content.BackColor = color;
            contentsPanel.Controls.Add(content);*/

            SetId();

            CreateLayer();
            CreateLayer();
            CreateLayer();            
            UpdateCache();
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
            layersHeight = layers.Count * LayerController.height + Math.Max(layers.Count - 1, 0) * layerMargin;
            int height = padding * 2 + layersHeight;
            int offset = id * height - rectangleProvider.VerticalScroll;
            sliceRectangle.X = timeline.Rectangle.X;
            sliceRectangle.Y = ParentRectangle.Y + offset;
            sliceRectangle.Width = timeline.Rectangle.Width;
            sliceRectangle.Height = height;

            controlRectangle.X = ControlParentRectangle.X;
            controlRectangle.Y = ControlParentRectangle.Y + offset;
            controlRectangle.Width = ControlParentRectangle.Width;
            controlRectangle.Height = height;
        }

        /*
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
            timeline.CreateSlice(id + 1);
        }

        private void Control_RemoveClicked(object sender, EventArgs e)
        {
            timeline.RemoveSlice(id);
        }

        private void Control_MoveUpClicked(object sender, EventArgs e)
        {
            if (id > 0) timeline.SwapSlices(id, id - 1);
        }

        private void Control_MoveDownClicked(object sender, EventArgs e)
        {
            if (id < timeline.SliceCount - 1) timeline.SwapSlices(id, id + 1);
        }*/

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
            /*controlsPanel.Controls.SetChildIndex(control, id);
            contentsPanel.Controls.SetChildIndex(content, id);*/
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

        internal void CreateLayer()
        {
            CreateLayer(LayerCount);            
        }

        internal void CreateLayer(int id)
        {
            LayerController layer = new LayerController(this, id);
            layers.Insert(id, layer);
            for (int i = LayerCount - 1; i > id; i -= 1)
            {
                layers[i].SetId(i);
            }
            UpdateCache();
            InvokeSizeChanged();
            UpdateUI();
        }

        internal void RemoveLayer(int id)
        {
            LayerController layer = layers[id];
            layers.Remove(layer);
            layer.Dispose();
            for (int i = id; i < LayerCount; i += 1)
            {
                layers[i].SetId(i);
            }

            if (LayerCount == 0)
            {
                layers.Add(new LayerController(this));
            }

            UpdateCache();
            InvokeSizeChanged();
            UpdateUI();
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
                Brush brush = new SolidBrush(backColor);

                if (redrawContent)
                {
                    Region region = new Region(ParentRectangle);
                    region.Exclude(timeline.Cursor.Rectangle);

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
                    e.Graphics.Clip = new Region(ControlParentRectangle);

                    e.Graphics.FillRectangle(brush, new Rectangle(
                      controlRectangle.X, controlRectangle.Y, controlsWidth, controlRectangle.Height));
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

        public void Dispose()
        {
            /*foreach (LayerController layer in layers)
            {
                layer.Dispose();
            }

            controlsPanel.Controls.Remove(control);
            contentsPanel.Controls.Remove(content);
            control.Dispose();
            content.Dispose();*/
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
    }
}
