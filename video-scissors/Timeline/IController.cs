using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IController : IDisposable
    {
        /// <summary>
        /// Timeline length in frames. Can be changed.
        /// </summary>
        int TimelineLength { get; }
        
        /// <summary>
        /// Timeline horizontal zoom. Can be changed.
        /// </summary>
        float TimelineZoom { get; }

        /// <summary>
        /// Project framerate in fps. Can't be changed.
        /// </summary>
        int ProjectFramerate { get; }

        /// <summary>
        /// Project frame width in pixels. Can't be changed.
        /// </summary>
        int ProjectFrameWidth { get; }

        /// <summary>
        /// Project frame height in pixels. Can't be changed.
        /// </summary>
        int ProjectFrameHeight { get; }        

        void UpdateUI();        
    }
}
