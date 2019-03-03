using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class RulerController : IController
    {
        private int markWidth = 2;
        private Color markColor = Color.Black;
        private Color backColor = Color.AntiqueWhite;        

        private TimelineController timeline;
        private RectangleProvider rectangleProvider;
        
        private int[] markPattern;
        private Rectangle oldRect;
        private Rectangle oldScreenRect;
        private Rectangle rulerRectangle;
        private int oldScroll;

        public event EventHandler SizeChanged;

        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }


        public Rectangle Rectangle
        { get { return rulerRectangle; } }

        public Rectangle ParentRectangle
        { get { return rectangleProvider.RulerContainerRectangle; } }

        public int MarkWidth
        {
            get { return markWidth; }
            set
            {
                if (value > 0)
                {
                    markWidth = value;
                    UpdateUI();
                }
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                if (value != null)
                {
                    backColor = value;
                    UpdateUI();
                }
            }
        }

        public Color ForeColor
        {
            get { return markColor; }
            set
            {
                if (value != null)
                {
                    markColor = value;
                    UpdateUI();
                }
            }
        }

        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }

        public TimelineController Timeline { get { return timeline; } }

        public Region FullOccupiedRegion
        { get { return new Region(rulerRectangle); } }

        public Region FullParentRegion
        { get { return new Region(ParentRectangle); } }
        
        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;
        
        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        internal RulerController(TimelineController timeline)
        {
            this.timeline = timeline;

            rectangleProvider = timeline.RectangleProvider;
            oldRect = rectangleProvider.RulerContainerRectangle;
            oldScreenRect = GetScreenRectangle();
            oldScroll = rectangleProvider.HorizontalScroll;

            rulerRectangle = new Rectangle();
            UpdateCache();

            if (ProjectFramerate == 24)
            {
                markPattern = new int[] { 20, 35, 30, 35, 25, 35, 30, 35 };
            }
            else if (ProjectFramerate == 25)
            {
                markPattern = new int[] { 20, 35, 35, 35, 35, };
            }
            else if (ProjectFramerate == 30)
            {
                markPattern = new int[] { 20, 35, 35, 35, 35, 30, 35, 35, 35, 35 };
            }
            else
            {
                markPattern = new int[] { 25 };
            }

            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;
            timeline.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            timeline.LocationChanged += TimelineContent_LocationChanged;

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

        private void TimelineContent_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            if (rectangleProvider.HorizontalScroll != oldScroll)
            {
                oldScroll = rectangleProvider.HorizontalScroll;
                UpdateCache();
                InvokeLocationChanged(e);
                rectangleProvider.Invalidate(rectangleProvider.RulerContainerRectangle);
            }
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            if (!oldRect.Equals(rectangleProvider.RulerContainerRectangle))
            {
                UpdateCache();                
                UpdateUI();
            }
        }

        private void UpdateCache()
        {
            rulerRectangle.X = timeline.Rectangle.X;
            rulerRectangle.Y = timeline.Rectangle.Y;
            rulerRectangle.Width = timeline.Rectangle.Width;
            rulerRectangle.Height = rectangleProvider.RulerContainerRectangle.Height;
        }

        public void UpdateUI()
        {
            Rectangle screenRect = GetScreenRectangle();

            //if location or height has changed the entirety of old and new areas need redrawing
            if (screenRect.Location != oldScreenRect.Location || screenRect.Height != oldScreenRect.Height)
            {
                rectangleProvider.Invalidate(oldRect);
                rectangleProvider.Invalidate(ParentRectangle);
            }
            //if width has increased then only the new area needs redrawing
            else if (screenRect.Width != oldScreenRect.Width)
            {
                if (screenRect.Width > oldScreenRect.Width)
                {
                    rectangleProvider.Invalidate(new Rectangle(
                        oldScreenRect.Width, oldScreenRect.Y, 
                        screenRect.X - oldScreenRect.X, oldScreenRect.Height));
                }
            }
            //if nothing has changed then there's no need to redraw
            else return;

            oldRect = ParentRectangle;
            oldScreenRect = screenRect;
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(ParentRectangle))
            {
                Region graphicsClip = e.Graphics.Clip;
                Region region = graphicsClip;
                region.Exclude(Timeline.Cursor.Rectangle);
                e.Graphics.Clip = region;

                Rectangle clip = e.ClipRectangle;
                if (clip.X < ParentRectangle.X)
                {
                    int diff = ParentRectangle.X - e.ClipRectangle.X;
                    clip.X = ParentRectangle.X;
                    clip.Width = clip.Width - diff;
                }                

                Rectangle rect = ParentRectangle;
                Brush markBrush = new SolidBrush(markColor);
                Brush brush = new SolidBrush(backColor);

                e.Graphics.FillRectangle(brush, new Rectangle(
                    clip.X, ParentRectangle.Y,
                    clip.Width, ParentRectangle.Height));

                int startX = clip.X - rulerRectangle.X;
                int i = (int)(startX / TimelineZoom);
                int m = Math.Min(rect.Width + startX, rulerRectangle.Width);
                int x;
                for (; (x = (int)(i * TimelineZoom)) < m; i += 1)
                {
                    int y1 = i % ProjectFramerate == 0 ? 0 : markPattern[i % markPattern.Length];
                    Rectangle markRect = new Rectangle(rulerRectangle.X + x, y1, markWidth, 40 - y1);
                    e.Graphics.FillRectangle(markBrush, markRect);
                }

                e.Graphics.Clip = graphicsClip;
            }
        }

        private Rectangle GetScreenRectangle()
        {
            return rectangleProvider.RectangleToScreen(rectangleProvider.ClientRectangle);
        }

        public void Dispose()
        {
            
        }
    }
}
