using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors
{
    public static class GlobalConfig
    {
        private static float defaultTimelineZoom = 10;
        private static int defaultTimelineLength = 1800;
        private static int defaultProjectFramerate = 30;
        private static int defaultProjectFrameWidth = 1920;
        private static int defaultProjectFrameHeight = 1080;

        public static float DefaultTimelineZoom
        {
            get { return defaultTimelineZoom; }
            set
            {
                if (value > 0.1 && value < 20)
                {
                    defaultTimelineZoom = value;
                }
            }
        }

        public static int DefaultTimelineLength
        {
            get { return defaultTimelineLength; }
            set
            {
                if (value > 60 || value < 3600)
                {
                    defaultTimelineLength = value;
                }
            }
        }

        public static int DefaultProjectFramerate { get { return defaultProjectFramerate; } }
        public static int DefaultProjectFrameWidth { get { return defaultProjectFrameWidth; } }
        public static int DefaultProjectFrameHeight { get { return defaultProjectFrameHeight; } }
    }
}
