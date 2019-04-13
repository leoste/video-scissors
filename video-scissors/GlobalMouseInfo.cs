using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors
{
    /// <summary>
    /// This class will hopefully be replaced with a better solution that doesn't reveal this info to everything but only cursorcontroller and menu.
    /// But maybe this revelation is necessary and this will stay. Either way this is a bit of an ugly solution so lets see.
    /// </summary>
    static class GlobalMouseInfo
    {
        public static MouseHolder Holder { get; set; }
        public static MouseHolder LastKnownHolder { get; set; }
        public static MouseState State { get; set; }
    }

    enum MouseState
    {
        Pressed,
        Unpressed
    }

    enum MouseHolder
    {
        None,
        RectangleProvider,
        Menu
    }
}
