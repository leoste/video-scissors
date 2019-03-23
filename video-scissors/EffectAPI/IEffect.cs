using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.EffectAPI
{
    public interface IEffect
    {

        void OnLoad();
        void OnUpdate();
        void OnUnload();
    }
}
