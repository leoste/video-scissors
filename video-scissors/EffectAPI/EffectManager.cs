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
    class EffectManager
    {
        public static List<Assembly> effects = new List<Assembly>();
        static void LoadEffect(string path)
        {
            try
            {
                Assembly effect = Assembly.LoadFile(path);
                effects.Add(effect);
            }
            catch (Exception e)
            {
                Debug.Fail(e.StackTrace);
            }
        }
    }
}
