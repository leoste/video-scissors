using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scissors.EffectAPI;
using Scissors.UserControls;

namespace Scissors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timeline1_Load(object sender, EventArgs e)
        {
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EffectLoader effectLoader = new EffectLoader();
            effectLoader.Show();
        }

        private void timeline2_Load(object sender, EventArgs e)
        {

        }
    }
}
