using Scissors.Objects;

namespace Scissors.EffectAPI
{
    public interface IEffect
    {

        void OnLoad();
        Frame ProcessFrame(Frame originalFrame);
        void OnUnload();
    }
}
