using System.Windows.Forms;

namespace Scissors.EffectAPI.UserControls
{
    internal partial class EffectControl : UserControl
    {
        private Effect effect;

        public EffectControl(Effect effect)
        {
            InitializeComponent();
            this.effect = effect;
        }

        private void Uninstall(object sender, System.EventArgs e)
        {
            EffectManager.UnloadEffect(effect);
            Dispose();
        }

        private void Loaded(object sender, System.EventArgs e)
        {
            effectName.Text = effect.Info.name;
            effectDescription.Text = effect.Info.description;
            effectVersion.Text = $"{effect.Info.version[0].ToString()}.{effect.Info.version[1].ToString()}.{effect.Info.version[2].ToString()}";
        }
    }
}
