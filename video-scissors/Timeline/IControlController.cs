﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IControlController : IController
    {
        bool IsLocked { get; }
        bool IsVisible { get; }

        Rectangle ControlRectangle { get; }
        Rectangle ControlParentRectangle { get; }

        Color RealBackColor { get; }
    }
}
