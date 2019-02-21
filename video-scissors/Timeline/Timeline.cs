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
            trackBar1.Minimum = 0;
        }

        private void timelineHorizontalScroll_Resize(object sender, EventArgs e)
        {
            int maximum = Math.Max(1, (int)(controller.TimelineLength * controller.TimelineZoom) - timelineHorizontalScroll.Width);
            float relation = (float)maximum / trackBar1.Maximum;            
            trackBar1.Maximum = maximum;
            trackBar1.Value = (int)(trackBar1.Value * relation);
            cursorPanel.Left = -trackBar1.Value;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            cursorPanel.Left = -trackBar1.Value;
        }
    }
}
