using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
