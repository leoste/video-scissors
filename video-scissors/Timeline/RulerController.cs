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
        private TimelineContent timelineContent;
        
        private int oldLength;
        private float oldZoom;
        private int[] markPattern;
        private Rectangle oldRect;
        private Rectangle oldScreenRect;

        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

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

        public Color MarkColor
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

        internal RulerController(TimelineController timeline)
        {
            this.timeline = timeline;

            timelineContent = timeline.Content;
            oldRect = timelineContent.RulerRectangle;
            oldScreenRect = GetScreenRectangle();

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

            timelineContent.Paint += TimelineContent_Paint;
            timelineContent.Resize += TimelineContent_Resize;
                        
            UpdateUI();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            if (!oldRect.Equals(timelineContent.RulerRectangle))
            {
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            Rectangle screenRect = GetScreenRectangle();

            //if location or height has changed the entirety of old and new areas need redrawing
            if (screenRect.Location != oldScreenRect.Location || screenRect.Height != oldScreenRect.Height)
            {
                timelineContent.Invalidate(oldRect);
                timelineContent.Invalidate(timelineContent.RulerRectangle);
            }
            //if width has increased then only the new area needs redrawing
            else if (screenRect.Width != oldScreenRect.Width)
            {
                if (screenRect.Width > oldScreenRect.Width)
                {
                    timelineContent.Invalidate(new Rectangle(
                        oldScreenRect.Width, oldScreenRect.Y, 
                        screenRect.X - oldScreenRect.X, oldScreenRect.Height));
                }
            }
            //if nothing has changed then there's no need to redraw
            else return;
                        
            oldRect = timelineContent.RulerRectangle;
            oldScreenRect = screenRect;
            timelineContent.Update();
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(timelineContent.RulerRectangle))
            {
                oldLength = TimelineLength;
                oldZoom = TimelineZoom;

                Rectangle rect = timelineContent.RulerRectangle;
                int width = (int)(TimelineLength * TimelineZoom);
                Pen pen = new Pen(markColor, markWidth);
                Brush brush = new SolidBrush(backColor);          

                e.Graphics.FillRectangle(brush, new Rectangle(
                    e.ClipRectangle.X,
                    timelineContent.RulerRectangle.Y,
                    e.ClipRectangle.Width,
                    timelineContent.RulerRectangle.Height));

                int startX = timelineContent.HorizontalScroll + e.ClipRectangle.X;
                int i = (int)(startX / TimelineZoom);
                int offset = (int)(pen.Width / 2);
                int m = Math.Min(rect.Width + startX, (int)(TimelineLength * TimelineZoom));
                int x;
                for (; (x = (int)(i * TimelineZoom + offset)) < m; i += 1)
                {
                    int y1 = i % ProjectFramerate == 0 ? 0 : markPattern[i % markPattern.Length];
                    int y2 = 40;
                    int corrected_x = x - timelineContent.HorizontalScroll;
                    e.Graphics.DrawLine(pen, corrected_x, y1, corrected_x, y2);
                }

                e.Dispose();
            }
        }

        private Rectangle GetScreenRectangle()
        {
            return timelineContent.RectangleToScreen(timelineContent.ClientRectangle);
        }

        public void Dispose()
        {
            
        }
    }
}
