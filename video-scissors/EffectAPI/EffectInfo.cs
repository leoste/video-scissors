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
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int[] Version = new int[3];

        public EffectInfo(string id, string name, string description, int[] version)
        {
            Id = id;
            Name = name;
            Description = description;
            Version = version;
        }
    }
}
