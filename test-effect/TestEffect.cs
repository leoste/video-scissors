using System;
using Scissors.EffectAPI;
using Scissors.Timeline;

namespace test_effect
{
    [EffectInfo("test_effect", "Meme Deep Frier", "Makes your videos have the deep frier meme effect :joy: :ok_hand:", new int[] {5, 35, 2})]
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
    }
}
