using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Config
{
    public static partial class GlobalConfig
    {
        private static float defaultTimelineZoom = 10;
        private static int defaultTimelineLength = 1800;

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
    }
}
