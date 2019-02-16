using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Config
{
    public static partial class GlobalConfig
    {
        private static int defaultProjectFramerate = 30;
        private static int defaultProjectFrameWidth = 1920;
        private static int defaultProjectFrameHeight = 1080;

        public static int DefaultProjectFramerate { get { return defaultProjectFramerate; } }
        public static int DefaultProjectFrameWidth { get { return defaultProjectFrameWidth; } }
        public static int DefaultProjectFrameHeight { get { return defaultProjectFrameHeight; } }
    }
}
