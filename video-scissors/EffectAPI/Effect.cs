namespace Scissors.EffectAPI
{
    internal class Effect
    {
        public string Path { get; }
        public EffectInfo Info { get; }
        public IEffect EffectInstance { get; }

        public Effect(EffectInfo info, IEffect effect, string path)
        {
            Info = info;
            EffectInstance = effect;
            Path = path;
        }
    }
}
