using System;
using Scissors.EffectAPI;

namespace test_effect
{
    [EffectInfo("test_effect", "Meme Deep Frier", "Makes your videos have the deep frier meme effect :joy: :ok_hand:", new int[] {5, 35, 2})]
    class TestEffect : IEffect
    {
        public void OnLoad()
        {
            Console.WriteLine("test_effect loaded!");
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
