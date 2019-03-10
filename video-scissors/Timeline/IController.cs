using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    interface IController
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

        RectangleProvider RectangleProvider { get; }
        Color BackColor { get; set; }
        Color ForeColor { get; set; }
        Rectangle Rectangle { get; }
        Rectangle ParentRectangle { get; }
        Region FullOccupiedRegion { get; }
        Region FullParentRegion { get; }

        event EventHandler SizeChanged;
        event EventHandler<LocationChangeEventArgs> LocationChanged;
        event EventHandler TimelineLengthChanged;
        event EventHandler TimelineZoomChanged;

        TimelineController ParentTimeline { get; }

        void UpdateUI();

        /// <summary>
        /// Disconnects all eventhandlers etc. Class shouldn't be used after this function has been called. NB! Does not get rid of references to itself, these should be removed before Deleting the class.
        /// </summary>
        void Delete();
    }
}
