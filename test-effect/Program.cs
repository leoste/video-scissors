using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scissors.EffectAPI;

namespace test_effect
{
    [EffectInfo("test_effect", "TestEffect", new int[] {0, 0, 1})]
    class Program : IEffect
    {

        public static void Main()
        {

        }

        public void OnLoad()
        {
            Console.WriteLine("hello");
        }

        public void OnUnload()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
