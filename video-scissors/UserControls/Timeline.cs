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

namespace Scissors.UserControls
{
    public partial class Timeline : UserControl
    {
        private List<SliceController> slices;

        internal FlowLayoutPanel ControlsPanel { get { return optionScroll; } }
        internal FlowLayoutPanel ContentsPanel { get { return sliceScroll; } }
        internal int SliceCount { get { return slices.Count; } }

        public Timeline()
        {
            InitializeComponent();
            slices = new List<SliceController>();
            slices.Add(new SliceController(this));
            slices.Add(new SliceController(this));
        }

        internal int GetSliceId(SliceController slice)
        {
            return slices.IndexOf(slice);
        }

        internal void CreateSlice()
        {
            CreateSlice(SliceCount);
        }

        internal void CreateSlice(int id)
        {
            for (int i = SliceCount - 1; i >= id; i -= 1)
            {
                slices[i].SetId(i + 1);                
            }
            slices.Insert(id, new SliceController(this, id));
        }

        /*public void CreateSlice(Slice bottomSlice)
        {
            int index = flowLayoutPanel1.Controls.GetChildIndex(bottomSlice) + 1;

            Slice slice = CreateSlice();

            int c = flowLayoutPanel1.Controls.Count - 1;

            for (int i = c - 1; i >= index; i -= 1)
            {
                Control control = flowLayoutPanel1.Controls[i];
                flowLayoutPanel1.Controls.SetChildIndex(control, i + 1);
            }

            flowLayoutPanel1.Controls.SetChildIndex(slice, index);
        }

        private Slice CreateSlice()
        {
            Slice slice = new Slice();
            slice.Width = flowLayoutPanel1.Width;
            Random rnd = new Random();
            slice.BackColor = Color.FromArgb(rnd.Next(32, 72), rnd.Next(32, 72), rnd.Next(32, 72));
            flowLayoutPanel1.Controls.Add(slice);
            return slice;
        }
        
        public void RemoveSlice(Slice slice)
        {
            if (flowLayoutPanel1.Controls.Count == 1)
            {
                CreateSlice(slice);
            }
            slice.Dispose();
        }

        int oldwidth = 0;

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            if (flowLayoutPanel1.Width != oldwidth)
            {
                oldwidth = flowLayoutPanel1.Width;

                int c = flowLayoutPanel1.Controls.Count;

                for (int i = 0; i < c; i += 1)
                {
                    flowLayoutPanel1.Controls[i].Width = flowLayoutPanel1.Width;
                }
            }
        }*/
    }
}
