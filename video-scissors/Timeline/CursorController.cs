using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class CursorController : IController
    {
        private CursorContent content;
        private TimelineController timeline;

        public CursorController(TimelineController timeline)
        {
            this.timeline = timeline;
            
            Panel panel = timeline.CursorPanel;

            content = new CursorContent();
            content.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;            
            content.Height = panel.Height;
            content.Visible = false;
            panel.Controls.Add(content);

            panel.MouseEnter += Panel_MouseEnter;

            panel.MouseMove += Panel_MouseMove;

            panel.MouseLeave += Panel_MouseLeave;
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            content.Visible = false;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            content.Left = e.X;
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            content.Visible = true;
        }

        public int TimelineLength { get { return timeline.TimelineLength; } }

        public float TimelineZoom { get { return timeline.TimelineZoom; } }

        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }

        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }

        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        public void Dispose()
        {
            timeline.CursorPanel.Controls.Remove(content);
            content.Dispose();
        }

        public void UpdateUI()
        {
            
        }
    }
}
