using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IFrameController : IController
    {
        /// <summary>
        /// Processed inputted frame and returns the result.
        /// </summary>
        /// <param name="position">
        /// Position of the frame as counted from the absolute start position of the object.
        /// </param>
        Frame ProcessFrame(Frame frame, int position);
    }
}
