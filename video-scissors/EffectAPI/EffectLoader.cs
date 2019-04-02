using Scissors.EffectAPI.UserControls;
using System;
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
                Effect effect = EffectManager.LoadEffectFile(fileDialog.FileName);
                if (effect != null)
                {
                    installedPlugins.Controls.Add(new EffectControl(effect));
                }
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

        private void FormLoaded(object sender, EventArgs e)
        {
            foreach (Effect effect in EffectManager.Effects)
            {
                installedPlugins.Controls.Add(new EffectControl(effect));
            }
        }
    }
}
