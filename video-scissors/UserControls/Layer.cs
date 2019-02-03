using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.UserControls
{
    public partial class Layer : UserControl
    {
        public Layer()
        {
            InitializeComponent();
        }

        private void removeLayer_Click(object sender, EventArgs e)
        {
            ParentSlice.RemoveLayer(this);
        }

        private void createLayer_Click(object sender, EventArgs e)
        {
            ParentSlice.CreateLayer(this);
        }

        private Slice ParentSlice { get { return Parent.Parent as Slice; } }
    }
}
