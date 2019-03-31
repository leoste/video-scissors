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
            effectName.Text = Effect.Info.Name;
            effectDescription.Text = Effect.Info.Description;
            effectVersion.Text = $"{Effect.Info.Version[0].ToString()}.{Effect.Info.Version[1].ToString()}.{Effect.Info.Version[2].ToString()}";
        }
    }
}
