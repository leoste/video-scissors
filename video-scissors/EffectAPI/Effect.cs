using System;
using System.Linq;
using System.Reflection;

namespace Scissors.EffectAPI
{
    internal class Effect
    {
        public string Path { get; }
        public EffectInfo Info { get; }
        public Type Type { get; }

        public Effect(Type effect, string path)
        {
            if (!effect.GetInterfaces().Contains(typeof(IEffect))) throw new ArgumentException("effect must implement IEffect!");

            Info = (EffectInfo)effect.GetCustomAttribute(typeof(EffectInfo), false);
            Type = effect;
            Path = path;
        }

        public IEffect CreateEffectInstance()
        {
            IEffect effectInstance = (IEffect)Activator.CreateInstance(Type);
            return effectInstance;
        }
    }
}
