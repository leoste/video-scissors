using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scissors.Timeline;
using Scissors.EffectAPI;
using System.IO;

namespace Scissors
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            EffectManager.LoadEffect(Path.GetFullPath("test-effect.dll"));
            pluginThread.RunWorkerAsync();
        }

        private void timeline1_Load(object sender, EventArgs e)
        {
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EffectLoader effectLoader = new EffectLoader();
            effectLoader.Show();
            EffectManager.UnloadEffect(EffectManager.Effects[0]);

        }

        private void timeline2_Load(object sender, EventArgs e)
        {

        }

        private void Update(object sender, DoWorkEventArgs e)
        {
            //TODO: Implement performance friendly update loop (tried with while(true) but that's trash and slow)
        }
    }
}
