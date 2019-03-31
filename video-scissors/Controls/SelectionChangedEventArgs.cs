using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Controls
{
    public class SelectionEventArgs : EventArgs
    {
        public int SelectedId { get; }
        public bool SelectionChanged { get; }

        public SelectionEventArgs(int id, bool changed)
        {
            SelectedId = id;
            SelectionChanged = changed;
        }
    }
}
