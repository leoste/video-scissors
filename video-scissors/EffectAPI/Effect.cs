namespace Scissors.EffectAPI
{
    internal class Effect
    {
        public EffectInfo Info { get; }
        public IEffect EffectInstance { get; }

        public Effect(EffectInfo info, IEffect effect)
        {
            Info = info;
            EffectInstance = effect;
        }
    }
}
