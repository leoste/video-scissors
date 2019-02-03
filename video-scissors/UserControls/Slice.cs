using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.UserControls
{
    public partial class Slice : UserControl
    {
        public Slice()
        {
            InitializeComponent();
        }

        public void CreateLayer(Layer bottomLayer)
        {
            int index = flowLayoutPanel1.Controls.GetChildIndex(bottomLayer) + 1;

            Layer layer = new Layer();
            layer.Width = flowLayoutPanel1.Width;
            Random rnd = new Random();
            layer.BackColor = Color.FromArgb(rnd.Next(96, 192), rnd.Next(96, 192), rnd.Next(96, 192));
            flowLayoutPanel1.Controls.Add(layer);

            int c = flowLayoutPanel1.Controls.Count - 1;

            for (int i = c - 1; i >= index; i -= 1)
            {
                Control control = flowLayoutPanel1.Controls[i];
                flowLayoutPanel1.Controls.SetChildIndex(control, i + 1);
            }

            flowLayoutPanel1.Controls.SetChildIndex(layer, index);
            
            AddHeight(52);
        }

        public void RemoveLayer(Layer layer)
        {
            if (flowLayoutPanel1.Controls.Count == 1)
            {
                CreateLayer(layer);
            }

            layer.Dispose();
            AddHeight(-52);
        }

        public void AddHeight(int amount)
        {
            Height += amount;
        }        

        private void removeSlice_Click(object sender, EventArgs e)
        {
            ParentTimeline.RemoveSlice(this);
        }

        private void addSlice_Click(object sender, EventArgs e)
        {
            ParentTimeline.CreateSlice(this);
        }

        private Timeline ParentTimeline { get { return Parent.Parent as Timeline; } }

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
        }
    }
}
