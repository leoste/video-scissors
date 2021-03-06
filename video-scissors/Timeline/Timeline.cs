﻿using System;
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

        public Timeline()
        {
            InitializeComponent();
            controller = new TimelineController(this, rectangleProvider1, 540);
            rectangleProvider1.TimelineController = controller;

            controller.SizeChanged += Timeline_SizeChanged;                
            horizontalScrollBar.Minimum = 0;
            UpdateHorizontalScrollbar();            
        }

        private int oldWidth;
        private int oldHeight;

        private void rectangleProvider1_Resize(object sender, EventArgs e)
        {
            if (rectangleProvider1.Width != oldWidth)
            {
                UpdateHorizontalScrollbar();
                oldWidth = rectangleProvider1.Width;
            }
            if (rectangleProvider1.Height != oldHeight)
            {
                UpdateVerticalScrollbar();
                oldHeight = rectangleProvider1.Height;
            }
        }

        private void horizontalScrollBar_Scroll(object sender, ScrollEventArgs e)
        {
           rectangleProvider1.HorizontalScroll = horizontalScrollBar.Value;
        }

        private void UpdateHorizontalScrollbar()
        {
            horizontalScrollBar.Maximum = controller.Ruler.Rectangle.Width;
            horizontalScrollBar.ScrollWidth = rectangleProvider1.HorizontalContainerRectangle.Width;
        }

        private void Timeline_SizeChanged(object sender, EventArgs e)
        {
            UpdateHorizontalScrollbar();
            UpdateVerticalScrollbar();
        }

        private void verticalScrollbar_Scroll(object sender, ScrollEventArgs e)
        {
            rectangleProvider1.VerticalScroll = verticalScrollbar.Value;
        }

        private void UpdateVerticalScrollbar()
        {
            verticalScrollbar.Maximum = controller.SlicesRectangle.Height;
            verticalScrollbar.ScrollWidth = rectangleProvider1.VerticalContainerRectangle.Height;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            controller.TimelineZoom *= 0.8f;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.TimelineZoom *= 1.25f;
        }
    }
}
