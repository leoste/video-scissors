using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    interface IControl
    {
        event EventHandler RemoveClicked;
        event EventHandler AddClicked;
        event EventHandler MoveUpClicked;
        event EventHandler MoveDownClicked;
        event EventHandler<ToggleEventArgs> ToggleLockClicked;
        event EventHandler<ToggleEventArgs> ToggleVisibilityClicked;

        bool IsLockToggled { get; }
        bool IsVisibilityToggled { get; }
    }
}
