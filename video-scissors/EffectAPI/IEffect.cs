using Scissors.Objects;

namespace Scissors.EffectAPI
{
    public interface IEffect
    {

        void OnLoad();
        void ProcessFrame(Frame originalFrame);
        void OnUnload();
    }
}
