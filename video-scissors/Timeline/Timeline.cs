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

        public Timeline()
        {
            InitializeComponent();
            controller = new TimelineController(this);
        }        
    }
}
