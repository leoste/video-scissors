using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class RulerController : IDisposable
    {
        private TimelineController timeline;
        private FlowLayoutPanel contentsPanel;

        private RulerContent content;
        private int oldLength;
        private float oldZoom;

        internal int Length { get { return timeline.Length; } }
        internal float Zoom { get { return timeline.Zoom; } }
        internal int Framerate { get { return timeline.Framerate; } }
        internal int FrameWidth { get { return timeline.FrameWidth; } }
        internal int FrameHeight { get { return timeline.FrameHeight; } }

        internal RulerController(TimelineController timeline)
        {
            this.timeline = timeline;
            contentsPanel = timeline.RulerPanel;

            content = new RulerContent();
            contentsPanel.Controls.Add(content);
        }

        internal void UpdateUI()
        {
            if (Length != oldLength || Zoom != oldZoom)
            {
                oldLength = Length;
                oldZoom = Zoom;

                content.Width = (int)(Length * Zoom);
            }
        }

        public void Dispose()
        {
            content.Dispose();
        }
    }
}
