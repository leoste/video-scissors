using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Scissors.Timeline
{
    public partial class Timeline : UserControl
    {
        private TimelineController controller;

        internal FlowLayoutPanel ControlsPanel { get { return optionScroll; } }
        internal FlowLayoutPanel ContentsPanel { get { return sliceScroll; } }
        internal FlowLayoutPanel RulerPanel { get { return rulerScroll; } }
        internal Panel CursorPanel { get { return cursorPanel; } }

        public Timeline()
        {
            InitializeComponent();
            controller = new TimelineController(this, 90);
            horizontalScrollBar.Minimum = 0;
        }

        private void horizontalScrollBar_Resize(object sender, EventArgs e)
        {
            horizontalScrollBar.Maximum = (int)(controller.TimelineLength * controller.TimelineZoom);
            horizontalScrollBar.ScrollWidth = timelineHorizontalScroll.Width;
        }

        private void horizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            cursorPanel.Left = -horizontalScrollBar.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            cursorPanel.Left = -horizontalScrollBar.Value;
        }
    }
}
