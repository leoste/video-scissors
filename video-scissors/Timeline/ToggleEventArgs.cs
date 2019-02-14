using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    public class ToggleEventArgs : EventArgs
    {
        public bool ToggleValue { get; private set; }
        public ToggleEventArgs(bool toggleValue)
        {
            ToggleValue = toggleValue;
        }
    }
}
