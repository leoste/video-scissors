namespace Scissors.EffectAPI
{
    public interface IEffect
    {

        void OnLoad();
        void OnUpdate();
        void OnUnload();
    }
}
