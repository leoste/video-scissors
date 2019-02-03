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
    public partial class LayerObject : UserControl
    {
        private int startPosition;
        private int endPosition;
        private int length;

        public int StartPosition
        {
            get { return startPosition; }
            set { startPosition = value; }
        }

        public int EndPosition
        {
            get { return endPosition; }
            set
            {
                if (value > startPosition)
                {
                    endPosition = value;
                    length = endPosition - startPosition;
                }
                else throw new ArgumentException("End position must be larger than start position.");
            }
        }

        public int Length
        {
            get { return endPosition - startPosition; }
            set
            {
                if (value > 0)
                {
                    length = value;
                    endPosition = startPosition + value;
                }                
                else throw new ArgumentException("Length must be at least 1.");
            }
        }

        public LayerObject()
        {
            InitializeComponent();
        }
    }
}
