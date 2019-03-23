using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    public partial class SliceContent : UserControl, IContent
    {
        internal FlowLayoutPanel Panel { get { return flowLayoutPanel1; } }

        public SliceContent()
        {
            InitializeComponent();
        }
    }
}
