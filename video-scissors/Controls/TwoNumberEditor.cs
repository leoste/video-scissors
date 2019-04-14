using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Controls
{
    public partial class TwoNumberEditor : UserControl
    {
        public int Value1
        {
            get
            {
                int.TryParse(textBox1.Text, out int val);
                return val;
            }
            set
            {
                textBox1.Text = value.ToString();
            }
        }

        public int Value2
        {
            get
            {
                int.TryParse(textBox2.Text, out int val);
                return val;
            }
            set
            {
                textBox2.Text = value.ToString();
            }
        }

        public string Label1 { get { return label1.Text; } set { label1.Text = value; } }
        public string Label2 { get { return label2.Text; } set { label2.Text = value; } }

        public TwoNumberEditor()
        {
            InitializeComponent();
        }
    }
}
