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
    public partial class StringEditor : UserControl
    {
        public string Value { get { return textBox1.Text; } set { textBox1.Text = value; } }
        public string Label { get { return label1.Text; } set { label1.Text = value; } }

        public StringEditor()
        {
            InitializeComponent();
        }
    }
}
