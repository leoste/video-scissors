using System.Windows.Forms;

namespace Scissors.EffectAPI.UserControls
{
    internal partial class EffectControl : UserControl
    {
        public Effect Effect { get; }

        public EffectControl(Effect effect)
        {
            InitializeComponent();
            this.Effect = effect;
        }

        private void Uninstall(object sender, System.EventArgs e)
        {
            EffectManager.UnloadEffect(Effect);
            Dispose();
        }

        private void Loaded(object sender, System.EventArgs e)
        {
            effectName.Text = Effect.Info.name;
            effectDescription.Text = Effect.Info.description;
            effectVersion.Text = $"{Effect.Info.version[0].ToString()}.{Effect.Info.version[1].ToString()}.{Effect.Info.version[2].ToString()}";
        }
    }
}
