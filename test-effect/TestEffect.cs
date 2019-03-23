using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scissors.EffectAPI;

namespace test_effect
{
    [EffectInfo("test_effect", "TestEffect", new int[] {0, 0, 1})]
    class TestEffect : IEffect
    {
        public void OnLoad()
        {
            Console.WriteLine("test_effect loaded!");
        }

        public void OnUnload()
        {
            Console.WriteLine("test_effect unloaded!");
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
