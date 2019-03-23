using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.EffectAPI
{
    internal class EffectManager
    {
        public static List<Assembly> effects = new List<Assembly>();

        public static void LoadEffect(string path)
        {
            try
            {
                Assembly effect = Assembly.LoadFile(path);
                effects.Add(effect);
                foreach (Type type in effect.GetTypes())
                {
                    EffectInfo attribute = (EffectInfo)type.GetCustomAttribute(typeof(EffectInfo), false);
                    Console.WriteLine(attribute.id);
                }
            }
            catch (Exception e)
            {
                Debug.Fail(e.StackTrace);
            }
        }

        public static void RemoveEffect(Assembly assembly)
        {
            effects.Remove(assembly);
        }
    }
}
