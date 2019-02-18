using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Config
{
    public static partial class GlobalConfig
    {
        private static int resizeHandleWidth = 3;

        public static int ItemResizeHandleWidth {
            get { return resizeHandleWidth; }
            set { if (value > 0 && value < 20)
                {
                    resizeHandleWidth = value;
                }
            }
        }
    }
}
