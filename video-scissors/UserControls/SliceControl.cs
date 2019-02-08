using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.UserControls
{
    public partial class SliceControl : UserControl
    {
        public SliceControl()
        {
            InitializeComponent();
        }

        public event EventHandler RemoveClicked;
        public event EventHandler AddClicked;
        public event EventHandler MoveUpClicked;
        public event EventHandler MoveDownClicked;
        public event EventHandler ToggleLockClicked;
        public event EventHandler ToggleVisibilityClicked;

        internal FlowLayoutPanel Panel { get { return flowLayoutPanel1; } }

        private void removeSlice_Click(object sender, EventArgs e)
        {
            if (RemoveClicked != null) RemoveClicked.Invoke(this, EventArgs.Empty);
        }

        private void createSlice_Click(object sender, EventArgs e)
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
