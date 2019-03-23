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
        public static List<Effect> effects = new List<Effect>();

        public static void LoadEffect(string path)
        {
            try
            {
                Assembly effectDll = Assembly.LoadFile(path);
                foreach (Type type in effectDll.GetTypes())
                {
                    IEffect effectInstance = (IEffect)Activator.CreateInstance(type);
                    EffectInfo effectInfo = (EffectInfo)type.GetCustomAttribute(typeof(EffectInfo), false);
                    Effect effect = new Effect(effectInfo, effectInstance);

                    effect.EffectInstance.OnLoad();
                    effects.Add(effect);
                }
            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.Fail(e.StackTrace);
            }
        }

        public static void RemoveEffect(Effect effect)
        {
            effects.Remove(effect);
        }
    }
}
