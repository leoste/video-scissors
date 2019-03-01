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
        
        private int layersHeight = 40;

        private bool toggleLock = false;
        private bool toggleVisibility = false;
        private int id;
        private TimelineController timeline;
        private TimelineContent timelineContent;
        private TimelineControl timelineControl;
                
        private List<LayerController> layers;
        
        private Rectangle sliceRectangle;
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
        public TimelineContent TimelineContent { get { return timelineContent; } }
        public TimelineControl TimelineControl { get { return timelineControl; } }
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
        { get { throw new NotImplementedException(); } }
                
        public Rectangle SliceRectangle
        { get { return sliceRectangle; } }

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        private void Initialize(TimelineController timeline)
        {
            this.timeline = timeline;
            timelineControl = timeline.TimelineControl;

            timelineContent = timeline.TimelineContent;
            timelineContent.Paint += TimelineContent_Paint;
            timelineContent.VerticalScrolled += TimelineContent_VerticalScrolled;
            timelineContent.HorizontalScrolled += TimelineContent_HorizontalScrolled;
            timelineContent.Resize += TimelineContent_Resize;

            timeline.TimelineZoomChanged += Timeline_Changed;
            timeline.TimelineLengthChanged += Timeline_Changed;

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

            layers = new List<LayerController>();
            CreateLayer();
            CreateLayer();

            UpdateCache();
            UpdateUI();
        }

        private void Timeline_Changed(object sender, EventArgs e)
        {
            UpdateCache();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            UpdateCache();
        }

        private void TimelineContent_HorizontalScrolled(object sender, ScrollEventArgs e)
        {
            UpdateCache();
        }

        private void TimelineContent_VerticalScrolled(object sender, ScrollEventArgs e)
        {
            UpdateCache();
            UpdateUI();
        }

        private void UpdateCache()
        {
            layersHeight = layers.Count * LayerController.height + Math.Max((layers.Count - 1) * layerMargin, 0);
            int height = padding * 2 + layersHeight;
            sliceRectangle.X = timelineContent.SlicesContainerRectangle.X - timelineContent.HorizontalScroll;
            sliceRectangle.Y = timelineContent.SlicesContainerRectangle.Y + id * height - timelineContent.VerticalScroll;
            sliceRectangle.Width = (int)(timeline.TimelineLength * timeline.TimelineZoom);
            sliceRectangle.Height = height;
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
            Rectangle rect = sliceRectangle;
            if (rect.Y < timelineContent.SlicesContainerRectangle.Y)
            {
                rect.Height -= timelineContent.SlicesContainerRectangle.Y - rect.Y;
                rect.Y = timelineContent.SlicesContainerRectangle.Y;
            }
            timelineContent.Invalidate(rect);
            timelineContent.Update();
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(sliceRectangle))
            {
                Brush brush = new SolidBrush(backColor);
                
                e.Graphics.FillRectangle(brush, new Rectangle(
                    e.ClipRectangle.X, Math.Max(sliceRectangle.Y, timelineContent.SlicesContainerRectangle.Y),
                    e.ClipRectangle.Width, padding));

                for (int i = 1; i < layers.Count; i += 1)
                {
                    int y = sliceRectangle.Y + padding + LayerController.height * i + (i - 1) * layerMargin;
                    e.Graphics.FillRectangle(brush, new Rectangle(
                        e.ClipRectangle.X, y,
                        e.ClipRectangle.Width, layerMargin));
                }

                e.Graphics.FillRectangle(brush, new Rectangle(
                    e.ClipRectangle.X, sliceRectangle.Y + padding + layersHeight,
                    e.ClipRectangle.Width, padding));
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
