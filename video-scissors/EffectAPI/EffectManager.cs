using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Scissors.EffectAPI
{
    internal class EffectManager
    {
        public static List<Effect> Effects { get; } = new List<Effect>();

        public static Effect LoadEffect(string path)
        {
            try
            {
                Assembly effectDll = Assembly.LoadFile(path);
                Type type = effectDll.GetTypes()[0];
                IEffect effectInstance = (IEffect)Activator.CreateInstance(type);
                EffectInfo effectInfo = (EffectInfo)type.GetCustomAttribute(typeof(EffectInfo), false);
                Effect effect = new Effect(effectInfo, effectInstance);

                effect.EffectInstance.OnLoad();
                Effects.Add(effect);
                return effect;
            }
            catch (ReflectionTypeLoadException e)
            {
                Debug.Fail(e.StackTrace);
                return null;
            }
        }

        public static void UnloadEffect(Effect effect)
        {
            effect.EffectInstance.OnUnload();
            Effects.Remove(effect);
        }
    }
}
