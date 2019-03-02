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
        private int oldScroll = 0;

        public Timeline()
        {
            InitializeComponent();
            controller = new TimelineController(this, timelineContent1, 540);
            controller.SizeChanged += Timeline_SizeChanged;                
            horizontalScrollBar.Minimum = 0;
            UpdateHorizontalScrollbar();
        }

        private int oldWidth;
        private int oldHeight;

        private void timelineContent1_Resize(object sender, EventArgs e)
        {
            if (timelineContent1.Width != oldWidth)
            {
                UpdateHorizontalScrollbar();
                oldWidth = timelineContent1.Width;
            }
            if (timelineContent1.Height != oldHeight)
            {
                UpdateVerticalScrollbar();
                oldHeight = timelineContent1.Height;
            }
        }

        private void horizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
           timelineContent1.HorizontalScroll = horizontalScrollBar.Value;
        }

        private void UpdateHorizontalScrollbar()
        {
            horizontalScrollBar.Maximum = controller.RulerRectangle.Width;
            horizontalScrollBar.ScrollWidth = timelineContent1.HorizontalContainerRectangle.Width;
        }

        private void Timeline_SizeChanged(object sender, EventArgs e)
        {
            UpdateHorizontalScrollbar();
            UpdateVerticalScrollbar();
        }

        private void verticalScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            timelineContent1.VerticalScroll = verticalScrollbar.Value;
        }

        private void UpdateVerticalScrollbar()
        {
            verticalScrollbar.Maximum = controller.SlicesRectangle.Height;
            verticalScrollbar.ScrollWidth = timelineContent1.VerticalContainerRectangle.Height;
        }
    }
}
