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
        
        public static void SetDefaultProjectFramerate(Framerate framerate)
        {
            defaultProjectFrameHeight = (int)framerate;
        }

        public static void SetDefaultProjectResolution(Resolution resolution)
        {
            if (resolution == Resolution.XGA)
            {
                defaultProjectFrameWidth = 1024;
                defaultProjectFrameHeight = 768;
            }
            else if (resolution == Resolution.HD)
            {
                defaultProjectFrameWidth = 1280;
                defaultProjectFrameHeight = 720;
            }
            else if (resolution == Resolution.FullHD)
            {
                defaultProjectFrameWidth = 1920;
                defaultProjectFrameHeight = 1080;
            }
        }

        public static void SetDefaultProjectResolution(int width, int height)
        {
            defaultProjectFrameWidth = width;
            defaultProjectFrameHeight = height;
        }
    }
}
