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
        private Color markColor = Color.Goldenrod;

        private TimelineController timeline;
        private FlowLayoutPanel contentsPanel;

        private RulerContent content;
        private int oldLength;
        private float oldZoom;
        private int[] markPattern;

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
            contentsPanel = timeline.RulerPanel;

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

            content = new RulerContent();
            contentsPanel.Controls.Add(content);

            UpdateUI();
        }

        public void UpdateUI()
        {

            if (TimelineLength != oldLength || TimelineZoom != oldZoom)
            {
                oldLength = TimelineLength;
                oldZoom = TimelineZoom;

                int width = (int)(TimelineLength * TimelineZoom);

                content.Width = width;

                Image image = content.BackgroundImage;
                if (image != null) image.Dispose();
                Bitmap bmp = new Bitmap(width, 40);
                Pen pen = new Pen(markColor, markWidth);

                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    for (int i = 0; i < TimelineLength; i += 1)
                    {
                        int x = (int)(i * TimelineZoom + pen.Width / 2);
                        int y1 = i % ProjectFramerate == 0 ? 0 : markPattern[i % markPattern.Length];
                        int y2 = 40;
                        gfx.DrawLine(pen, x, y1, x, y2);
                    }
                }

                content.BackgroundImage = bmp;
            }
        }

        public void Dispose()
        {
            content.Dispose();
        }
    }
}
