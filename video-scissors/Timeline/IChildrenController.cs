using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IChildController : IController
    {
        /// <summary>
        /// Returns list of controller's immediate children.
        /// </summary>
        List<IController> GetChildren();

        /// <summary>
        /// Returns list of controller's children, grand-children, grand-grand-children etc.
        /// </summary>
        List<IController> GetChildrenDeep();
    }
}
