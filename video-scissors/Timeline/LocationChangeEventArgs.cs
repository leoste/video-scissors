using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    public class LocationChangeEventArgs : EventArgs
    {
        public bool LeftChanged { get; private set; }
        public bool TopChanged { get; private set; }

        internal LocationChangeEventArgs(bool leftChanged, bool topChanged)
        {
            LeftChanged = leftChanged;
            TopChanged = topChanged;
        }
    }
}
