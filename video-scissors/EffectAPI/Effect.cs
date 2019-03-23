using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.EffectAPI
{
    class Effect
    {
        private readonly EffectInfo info;
        private readonly IEffect effect;

        public EffectInfo Info
        {
            get
            {
                return info;
            }
        }

        public IEffect EffectInstance
        {
            get
            {
                return effect;
            }
        }

        public Effect(EffectInfo info, IEffect effect)
        {
            this.info = info;
            this.effect = effect;
        }
    }
}
