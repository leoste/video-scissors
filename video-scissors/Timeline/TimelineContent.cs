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
        private int horizontalScroll = 0;
        private int verticalScroll = 0;

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
                        InvokeInternalHeightsChanged(EventArgs.Empty);
                    }

                    Refresh();
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
                        InvokeInternalHeightsChanged(EventArgs.Empty);
                    }

                    Refresh();
                }
            }
        }

        public new int Height
        {
            get { return base.Height; }
            set
            {
                base.Height = value;
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
            }
        }

        public new int HorizontalScroll
        {
            get { return horizontalScroll; }
            set
            {
                if (value >= 0)
                {
                    if (horizontalScroll != value)
                    {
                        horizontalScroll = value;
                        InvokeHorizontalScrolled(new ScrollEventArgs(ScrollEventType.SmallDecrement, horizontalScroll));
                    }
                }
            }
        }

        public new int VerticalScroll
        {
            get { return verticalScroll; }
            set
            {
                if (value >= 0)
                {
                    if (verticalScroll != value)
                    {
                        verticalScroll = value;
                        InvokeVerticalScrolled(new ScrollEventArgs(ScrollEventType.SmallDecrement, verticalScroll));
                    }
                }
            }
        }

        public Rectangle RulerContainerRectangle
        { get { return new Rectangle(0, 0, Width, rulerHeight); } }

        public Rectangle SlicesContainerRectangle
        { get { return new Rectangle(0, slicesBegin, Width, slicesHeight); } }

        public event EventHandler InternalHeightsChanged;
        private void InvokeInternalHeightsChanged(EventArgs e)
        { if (InternalHeightsChanged != null) InternalHeightsChanged.Invoke(this, e); }

        public event EventHandler<ScrollEventArgs> HorizontalScrolled;
        private void InvokeHorizontalScrolled(ScrollEventArgs e)
        { if (HorizontalScrolled != null) HorizontalScrolled.Invoke(this, e); }

        public event EventHandler<ScrollEventArgs> VerticalScrolled;
        private void InvokeVerticalScrolled(ScrollEventArgs e)
        { if (VerticalScrolled != null) VerticalScrolled.Invoke(this, e); }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do not call base.OnPaintBackground(), it will cause flickering with controls drawing onto here

            Rectangle rect = new Rectangle(e.ClipRectangle.X, rulerHeight, e.ClipRectangle.Width, separatorHeight);

            if (rect.IntersectsWith(e.ClipRectangle))
            {
                Brush brush = new SolidBrush(BackColor);
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            int protoSlicesHeight = CalculateSlicesHeight(Size.Height, slicesBegin);            
            if (protoSlicesHeight <= 0) protoSlicesHeight = 1;
            slicesHeight = protoSlicesHeight;

            base.OnResize(e);
        }

        public TimelineContent()
        {
            InitializeComponent();
            UpdateSlicesCache();
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
