using Scissors.Objects;

namespace Scissors.Timeline
{
    interface IFrameController : IController
    {
        /// <summary>
        /// Processes inputted frame, modifying the input variable.
        /// </summary>
        /// <param name="position">
        /// Position of the frame as counted from the absolute start position of the object.
        /// </param>
        void ProcessFrame(Frame frame, int position);
    }
}
