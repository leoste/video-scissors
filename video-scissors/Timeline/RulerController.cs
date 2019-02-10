using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class RulerController : IController
    {
        private TimelineController timeline;
        private FlowLayoutPanel contentsPanel;

        private RulerContent content;
        private int oldLength;
        private float oldZoom;

        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        internal RulerController(TimelineController timeline)
        {
            this.timeline = timeline;
            contentsPanel = timeline.RulerPanel;

            content = new RulerContent();
            contentsPanel.Controls.Add(content);
        }

        public void UpdateUI()
        {
            if (TimelineLength != oldLength || TimelineZoom != oldZoom)
            {
                oldLength = TimelineLength;
                oldZoom = TimelineZoom;

                content.Width = (int)(TimelineLength * TimelineZoom);
            }
        }

        public void Dispose()
        {
            content.Dispose();
        }
    }
}
