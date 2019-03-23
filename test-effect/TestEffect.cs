using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scissors.EffectAPI;

namespace test_effect
{
    [EffectInfo("test_effect", "Meme Deep Fryer", "Makes your videos have the deep fryer meme effect :joy: :ok_hand:", new int[] {0, 0, 1})]
    class TestEffect : IEffect
    {
        public void OnLoad()
        {
            Console.WriteLine($"{Math.Sin(9)}");
        }

        public void OnUpdate()
        {
            Console.WriteLine("test_effect updated!");
        }

        public void OnUnload()
        {
            Console.WriteLine("test_effect unloaded!");
        }
    }
}
