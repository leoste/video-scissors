using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IControlController : IController
    {
        bool IsLocked { get; }
    }
}
