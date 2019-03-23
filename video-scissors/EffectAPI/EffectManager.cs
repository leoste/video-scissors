using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Scissors.EffectAPI
{
    internal class EffectManager
    {
        public static List<Effect> Effects { get; } = new List<Effect>();

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
                    Effects.Add(effect);
                }

            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.Fail(e.StackTrace);
            }
        }

        public static void UnloadEffect(Effect effect)
        {
            effect.EffectInstance.OnUnload();
            Effects.Remove(effect);
        }
    }
}
