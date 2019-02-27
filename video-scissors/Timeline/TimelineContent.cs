using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.ComponentModel.Design;
using System.Collections;

namespace Scissors.Timeline
{
    [Designer(typeof(TimelineContentDesigner))]
    public partial class TimelineContent : UserControl
    {
        private int rulerHeight = 40;
        private int separatorHeight = 3;

        private int slicesHeight;
        private int slicesBegin;

        public int RulerHeight
        {
            get { return rulerHeight; }
            set
            {
                if (value > 0)
                {
                    int protoSlicesBegin = CalculateSlicesBegin(value, separatorHeight);
                    int protoSlicesHeight = CalculateSlicesHeight(Height, protoSlicesBegin);

                    if (protoSlicesHeight > 0)
                    {
                        rulerHeight = value;
                        slicesBegin = protoSlicesBegin;
                        slicesHeight = protoSlicesHeight;
                        InvokeInternalHeightsChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public int SeparatorHeight
        {
            get { return separatorHeight; }
            set
            {
                if (value >= 0)
                {
                    int protoSlicesBegin = CalculateSlicesBegin(rulerHeight, value);
                    int protoSlicesHeight = CalculateSlicesHeight(Height, protoSlicesBegin);

                    if (protoSlicesHeight > 0)
                    {
                        separatorHeight = value;
                        slicesBegin = protoSlicesBegin;
                        slicesHeight = protoSlicesHeight;
                        InvokeInternalHeightsChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public Rectangle RulerRectangle
        { get { return new Rectangle(0, 0, Width, rulerHeight); } }

        public Rectangle SlicesRectangle
        { get { return new Rectangle(slicesBegin, 0, Width, slicesHeight); } }

        public event EventHandler InternalHeightsChanged;
        private void InvokeInternalHeightsChanged(object sender, EventArgs e)
        { if (InternalHeightsChanged != null) InternalHeightsChanged.Invoke(sender, e); }

        public TimelineContent()
        {
            InitializeComponent();
            UpdateSlicesCache();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            
        }

        private int CalculateSlicesBegin(int rulerHeight, int separatorHeight)
        {
            return rulerHeight + separatorHeight;
        }

        private int CalculateSlicesHeight(int height, int slicesBegin)
        {
            return height - slicesBegin;
        }

        private void UpdateSlicesCache()
        {
            slicesBegin = CalculateSlicesBegin(rulerHeight, separatorHeight);
            slicesHeight = CalculateSlicesHeight(Height, slicesBegin);
        }       
    }
}
