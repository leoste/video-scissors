using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.EffectAPI
{
    interface IEffect
    {
        string Name { get; set; }
        int[] Version { get; set; }
        
        void Load();
        void Update();
        void Shutdown();
    }
}
