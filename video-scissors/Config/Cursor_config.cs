using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Config
{
    public static partial class GlobalConfig
    {        
        public static Brush CursorRegularBrush { get; set; } = new SolidBrush(Color.Crimson);
        public static Brush CursorMoveItemBrush { get; set; } = new SolidBrush(Color.Orchid);
    }
}
