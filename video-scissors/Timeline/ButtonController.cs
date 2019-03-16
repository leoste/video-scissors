using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class ButtonController : IController
    {
        public static readonly int width = 18;
        public static readonly int height = 18;
        public static readonly int margin = 1;

        IControlController control;
        RectangleProvider rectangleProvider;

        private Rectangle buttonRectangle;

        private Color backColor;
        private int left;
        private int top;

        public int TimelineLength { get { return control.TimelineLength; } }
        public float TimelineZoom { get { return control.TimelineZoom; } }
        public int ProjectFramerate { get { return control.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return control.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return control.ProjectFrameHeight; } }

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

        public Rectangle Rectangle { get { return buttonRectangle; } }

        public Rectangle ParentRectangle { get { return control.ControlParentRectangle; } }

        public Region FullOccupiedRegion => throw new NotImplementedException();

        public Region FullParentRegion => throw new NotImplementedException();

        public TimelineController ParentTimeline { get { return control.ParentTimeline; } }
        
        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        public event EventHandler SizeChanged;
        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        public ButtonController(IControlController control, Point relativePosition)
        {
            buttonRectangle = new Rectangle();
            buttonRectangle.Width = width;
            buttonRectangle.Height = height;

            this.control = control;
            rectangleProvider = this.control.RectangleProvider;
            left = relativePosition.X;
            top = relativePosition.Y;

            control.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            control.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            control.LocationChanged += Control_LocationChanged;
            rectangleProvider.Paint += RectangleProvider_Paint;            

            UpdateCache();
            UpdateUI();
        }

        private void Control_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            if (e.TopChanged)
            {
                UpdateCache();
                InvokeLocationChanged(e);
                UpdateUI();
            }
        }

        private void UpdateCache()
        {
            buttonRectangle.X = control.ControlRectangle.X + left;
            buttonRectangle.Y = control.ControlRectangle.Y + top;
        }

        public void UpdateUI()
        {
            rectangleProvider.InvalidateVerticalContainerRectangle(Rectangle);
        }

        private void RectangleProvider_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(Rectangle))
            {
                Region graphicsClip = e.Graphics.Clip;
                Region region = new Region(ParentRectangle);
                region.Exclude(ParentTimeline.Cursor.FullOccupiedRegion);
                e.Graphics.Clip = region;

                e.Graphics.FillRectangle(Brushes.Black, Rectangle);

                e.Graphics.Clip = graphicsClip;
            }
        }

        private void Timeline_TimelineZoomChanged(object sender, EventArgs e)
        {
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
        }

        private void Timeline_TimelineLengthChanged(object sender, EventArgs e)
        {
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
        }

        public void Delete()
        {
            control.TimelineZoomChanged -= Timeline_TimelineZoomChanged;
            control.TimelineLengthChanged -= Timeline_TimelineLengthChanged;
            control.LocationChanged -= Control_LocationChanged;
            rectangleProvider.Paint -= RectangleProvider_Paint;
        }
    }
}
