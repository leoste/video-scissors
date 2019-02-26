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

        internal FlowLayoutPanel ControlsPanel { get { return new FlowLayoutPanel(); } }
        internal FlowLayoutPanel ContentsPanel { get { return new FlowLayoutPanel(); } }
        internal FlowLayoutPanel RulerPanel { get { return new FlowLayoutPanel(); } }
        internal Panel CursorPanel { get { return new Panel(); } }

        public Timeline()
        {
            InitializeComponent();
            controller = new TimelineController(this, timelineControl1, timelineContent1, 900);
            horizontalScrollBar.Minimum = 0;
        }

        private void horizontalScrollBar_Resize(object sender, EventArgs e)
        {
            horizontalScrollBar.Maximum = (int)(controller.TimelineLength * controller.TimelineZoom);
            //horizontalScrollBar.ScrollWidth = timelineHorizontalScroll.Width;
        }

        private void horizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
            //cursorPanel.Left = -horizontalScrollBar.Value;
        }

        private void verticalScrollbar_Resize(object sender, EventArgs e)
        {
            UpdateVerticalScrollbar();
        }

        private void verticalScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            //optionScroll.Top = -verticalScrollbar.Value;
            //sliceScroll.Top = -verticalScrollbar.Value;
        }

        private void optionScroll_Resize(object sender, EventArgs e)
        {
            UpdateVerticalScrollbar();
        }

        private void UpdateVerticalScrollbar()
        {
            //verticalScrollbar.Maximum = optionScroll.Height;
            //verticalScrollbar.ScrollWidth = panel1.Height;
        }

        private void sliceScroll_Resize(object sender, EventArgs e)
        {
            //panel2.Width = sliceScroll.Width;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            //panel2.Height = panel1.Height;
        }
    }
}
