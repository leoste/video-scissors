using Scissors.EffectAPI.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.EffectAPI
{
    internal partial class EffectLoader : Form
    {
        public EffectLoader()
        {
            InitializeComponent();
        }

        private void AddEffect(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Link Library | *.dll"
            };
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                Effect effect = EffectManager.LoadEffect(fileDialog.FileName);
                installedPlugins.Controls.Add(new EffectControl(effect));
            }
        }
    }
}
