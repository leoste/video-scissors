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
                List<Effect> currentEffects = EffectManager.Effects;
                Effect effect = EffectManager.LoadEffect(fileDialog.FileName);
                installedPlugins.Controls.Add(new EffectControl(effect));
            }
        }

        private void SearchUpdated(object sender, EventArgs e)
        {
            foreach (EffectControl effect in installedPlugins.Controls)
            {
                if (!effect.Effect.Info.Name.ToLower().Contains(effectSearch.Text.ToLower()))
                {
                    effect.Visible = false;
                }
                else if (effect.Effect.Info.Name.ToLower().Contains(effectSearch.Text.ToLower()))
                {
                    effect.Visible = true;
                }
                else if (effectSearch.Text == string.Empty)
                {
                    effect.Visible = true;
                }
            }
        }
    }
}
