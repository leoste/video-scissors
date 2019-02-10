using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    public partial class LayerControl : UserControl, IControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        public event EventHandler RemoveClicked;
        public event EventHandler AddClicked;
        public event EventHandler MoveUpClicked;
        public event EventHandler MoveDownClicked;
        public event EventHandler ToggleLockClicked;
        public event EventHandler ToggleVisibilityClicked;

        private void removeLayer_Click(object sender, EventArgs e)
        {
            if (RemoveClicked != null) RemoveClicked.Invoke(this, EventArgs.Empty);
        }

        private void createLayer_Click(object sender, EventArgs e)
        {
            if (AddClicked != null) AddClicked.Invoke(this, EventArgs.Empty);
        }

        private void moveUp_Click(object sender, EventArgs e)
        {
            if (MoveUpClicked != null) MoveUpClicked.Invoke(this, EventArgs.Empty);
        }

        private void moveDown_Click(object sender, EventArgs e)
        {
            if (MoveDownClicked != null) MoveDownClicked.Invoke(this, EventArgs.Empty);
        }

        private void toggleLock_Click(object sender, EventArgs e)
        {
            if (ToggleLockClicked != null) ToggleLockClicked.Invoke(this, EventArgs.Empty);
        }

        private void toggleVisibility_Click(object sender, EventArgs e)
        {
            if (ToggleVisibilityClicked != null) ToggleVisibilityClicked.Invoke(this, EventArgs.Empty);
        }
    }
}
