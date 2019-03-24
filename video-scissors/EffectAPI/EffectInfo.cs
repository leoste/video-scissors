using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.EffectAPI
{
    [AttributeUsage(AttributeTargets.Class)]
    public class EffectInfo : Attribute
    {
        public readonly string id;
        public readonly string name;
        public readonly string description;
        public readonly int[] version = new int[3];

        public EffectInfo(string id, string name, string description, int[] version)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.version = version;
        }
    }
}
