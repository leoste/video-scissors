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
            timelineContent.Invalidate(oldRect);
            timelineContent.Invalidate(timelineContent.RulerRectangle);
            timelineContent.Update();
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(timelineContent.RulerRectangle))
            {
                oldLength = TimelineLength;
                oldZoom = TimelineZoom;
                oldRect = timelineContent.RulerRectangle;

                Rectangle rect = timelineContent.RulerRectangle;
                int width = (int)(TimelineLength * TimelineZoom);
                Pen pen = new Pen(markColor, markWidth);
                Brush brush = new SolidBrush(backColor);          

                e.Graphics.FillRectangle(brush, timelineContent.RulerRectangle);                
                
                int x;
                int i = (int)(timelineContent.HorizontalScroll / TimelineZoom);
                int offset = (int)(timelineContent.HorizontalScroll - i * TimelineZoom + pen.Width / 2);
                int m = rect.Width + timelineContent.HorizontalScroll;

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

        public void Dispose()
        {
            
        }
    }
}
