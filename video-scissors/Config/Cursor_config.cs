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
        public static Brush CursorMainBrush { get; set; } = new SolidBrush(Color.Crimson);
        public static Brush CursorItemEdgeBrush { get; set; } = new SolidBrush(Color.DodgerBlue);
        public static Brush CursorItemResizeBrush { get; set; } = new SolidBrush(Color.DeepSkyBlue);
        public static Brush CursorItemAnchorBrush { get; set; } = new SolidBrush(Color.DimGray);
    }
}
