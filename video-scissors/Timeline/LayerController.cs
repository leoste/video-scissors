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

        private bool toggleLock;
        private bool toggleVisibility;
        private int id;
        private SliceController slice;
        private TimelineContent timelineContent;
        private TimelineControl timelineControl;

        private Rectangle layerRectangle;
        private Color backColor;

        public TimelineContent TimelineContent { get { return timelineContent; } }
        public TimelineControl TimelineControl { get { return timelineControl; } }

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

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler LocationChanged;
        private void InvokeLocationChanged()
        { if (LocationChanged != null) LocationChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        private void Initialize(SliceController slice)
        {
            this.slice = slice;
            timelineControl = slice.TimelineControl;

            timelineContent = slice.TimelineContent;
            timelineContent.Paint += TimelineContent_Paint;
            timelineContent.Resize += TimelineContent_Resize;

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

            items.Add(new ItemController(this, 2, 10));
            items.Add(new ItemController(this, 15, 10));
            items.Add(new ItemController(this, 40, 5));

            UpdateUI();
        }

        private void Slice_LocationChanged(object sender, EventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged();
            UpdateUI();
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
            layerRectangle.X = slice.LayersRectangle.X;
            layerRectangle.Y = slice.LayersRectangle.Y + id * (height + SliceController.layerMargin);
            layerRectangle.Width = slice.LayersRectangle.Width;
            layerRectangle.Height = height;
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
            timelineContent.InvalidateSlicesContainerRectangle(layerRectangle);
        }
        
        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(layerRectangle))
            {
                Region graphicsClip = e.Graphics.Clip;
                e.Graphics.Clip = new Region(ParentRectangle);

                Brush brush = new SolidBrush(backColor);

                e.Graphics.FillRectangle(brush, new Rectangle(
                    e.ClipRectangle.X, layerRectangle.Y,
                    e.ClipRectangle.Width, height));

                //writes scroll position for debug
                //e.Graphics.DrawString(Rectangle.X.ToString(), SystemFonts.DefaultFont, new SolidBrush(Color.Black), ParentRectangle.X + 3, Rectangle.Y + 3);

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
