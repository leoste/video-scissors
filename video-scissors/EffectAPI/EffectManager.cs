using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows.Forms;

namespace Scissors.EffectAPI
{
    internal class EffectManager
    {
        public static List<Effect> Effects { get; } = new List<Effect>();

        public static Effect LoadEffectFile(string path)
        {
            string copyDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Video Scissors\\effects\\");
            string fileName = Path.GetFileName(path);
            if (!File.Exists(Path.Combine(copyDirectory, fileName)))
            {
                try
                {
                    File.Copy(path, Path.Combine(copyDirectory, fileName));
                    File.SetAttributes(Path.Combine(copyDirectory, fileName), FileAttributes.Normal);
                    return LoadEffect(path);
                }
                catch (ReflectionTypeLoadException e)
                {
                    MessageBox.Show(e.StackTrace, "Error!", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Duplicate effect detected!", "Duplicate Error!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }

        public static void LoadEffects()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Video Scissors\\effects\\");
            if (Directory.Exists(path))
            {
                foreach (string effectFile in Directory.GetFiles(path))
                {
                    if (Path.GetExtension(effectFile) == ".dll")
                    {
                        LoadEffect(effectFile);
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(path);
            }
        }

        public static void UnloadEffect(Effect effect)
        {
            effect.EffectInstance.OnUnload();
            Effects.Remove(effect);
            File.Delete(effect.Path);
        }

        private static Effect LoadEffect(string path)
        {
            string copyDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Video Scissors\\effects\\");
            try
            {
                Assembly effectDll = Assembly.LoadFile(path);
                Type type = effectDll.GetTypes()[0];
                IEffect effectInstance = (IEffect)Activator.CreateInstance(type);
                EffectInfo effectInfo = (EffectInfo)type.GetCustomAttribute(typeof(EffectInfo), false);
                Effect effect = new Effect(effectInfo, effectInstance, Path.Combine(copyDirectory, Path.GetFileName(path)));

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
    }
}
