using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    class DisownEventArgs : EventArgs
    {
        internal DisownEventArgs(IController disownedChild, IChildController newParent)
        {
            DisownedChild = disownedChild;
            NewParent = newParent;
        }

        public IController DisownedChild { get; private set; }
        public IChildController NewParent { get; private set; }
    }
}
