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
    [Designer(typeof(TimelineRectangleProviderDesigner))]
    public partial class RectangleProvider : UserControl
    {
        private int rulerHeight = 40;
        private int separatorHeight = 3;
        private int controlWidth = 144;
        private int separatorWidth = 3;
        private int horizontalScroll = 0;
        private int verticalScroll = 0;        

        private int contentHeight;
        private int contentBegin;
        private int rulerBegin;
        private int rulerWidth;

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
                        contentBegin = protoSlicesBegin;
                        contentHeight = protoSlicesHeight;
                        InvokeInternalSizesChanged(EventArgs.Empty);
                        Refresh();
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
                        contentBegin = protoSlicesBegin;
                        contentHeight = protoSlicesHeight;
                        InvokeInternalSizesChanged(EventArgs.Empty);
                        Refresh();
                    }

                }
            }
        }

        public int ControlWidth
        {
            get { return controlWidth; }
            set
            {
                if (value > 0)
                {
                    int protoRulerBegin = CalculateRulerBegin(value, separatorWidth);
                    int protoRulerWidth = CalculateRulerWidth(Width, protoRulerBegin);

                    if (protoRulerWidth > 0)
                    {
                        controlWidth = value;
                        rulerBegin = protoRulerBegin;
                        rulerWidth = protoRulerWidth;
                        InvokeInternalSizesChanged(EventArgs.Empty);
                        Refresh();
                    }
                }
            }
        }

        public int SeparatorWidth
        {
            get { return separatorWidth; }
            set
            {
                if (value >= 0)
                {
                    int protoRulerBegin = CalculateRulerBegin(controlWidth, value);
                    int protoRulerWidth = CalculateRulerWidth(Width, protoRulerBegin);

                    if (protoRulerWidth > 0)
                    {
                        separatorWidth = value;
                        rulerBegin = protoRulerBegin;
                        rulerWidth = protoRulerWidth;
                        InvokeInternalSizesChanged(EventArgs.Empty);
                        Refresh();
                    }
                }
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
        { get { return new Rectangle(rulerBegin, 0, rulerWidth, rulerHeight); } }

        public Rectangle ContentContainerRectangle
        { get { return new Rectangle(rulerBegin, contentBegin, rulerWidth, contentHeight); } }

        public Rectangle ControlContainerRectangle
        { get { return new Rectangle(0, contentBegin, controlWidth, contentHeight); } }

        public Rectangle ContainerRectangle
        { get { return new Rectangle(0, 0, Width, Height); } }

        public Rectangle HorizontalContainerRectangle
        { get { return new Rectangle(rulerBegin, 0, rulerWidth, Height); } }

        public Rectangle VerticalContainerRectangle
        { get { return new Rectangle(0, contentBegin, Width, contentHeight); } }

        public event EventHandler InternalSizesChanged;
        private void InvokeInternalSizesChanged(EventArgs e)
        { if (InternalSizesChanged != null) InternalSizesChanged.Invoke(this, e); }

        public event EventHandler<ScrollEventArgs> HorizontalScrolled;
        private void InvokeHorizontalScrolled(ScrollEventArgs e)
        { if (HorizontalScrolled != null) HorizontalScrolled.Invoke(this, e); }

        public event EventHandler<ScrollEventArgs> VerticalScrolled;
        private void InvokeVerticalScrolled(ScrollEventArgs e)
        { if (VerticalScrolled != null) VerticalScrolled.Invoke(this, e); }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do not call base.OnPaintBackground(), it will cause flickering with controls drawing onto here

            Rectangle horizontal = new Rectangle(e.ClipRectangle.X, rulerHeight, e.ClipRectangle.Width, separatorHeight);
            Rectangle vertical = new Rectangle(ControlWidth, e.ClipRectangle.Y, SeparatorWidth, e.ClipRectangle.Height);

            Brush brush = new SolidBrush(BackColor);

            if (horizontal.IntersectsWith(e.ClipRectangle)) e.Graphics.FillRectangle(brush, horizontal);

            if (vertical.IntersectsWith(e.ClipRectangle)) e.Graphics.FillRectangle(brush, vertical);
        }

        protected override void OnResize(EventArgs e)
        {
            int protoSlicesHeight = CalculateSlicesHeight(Size.Height, contentBegin);            
            if (protoSlicesHeight <= 0) protoSlicesHeight = 1;
            contentHeight = protoSlicesHeight;

            int protoRulerWidth = CalculateRulerWidth(Size.Width, rulerBegin);
            if (protoRulerWidth <= 0) protoRulerWidth = 1;
            rulerWidth = protoRulerWidth;

            base.OnResize(e);
        }

        public RectangleProvider()
        {
            InitializeComponent();

            contentBegin = CalculateSlicesBegin(rulerHeight, separatorHeight);
            contentHeight = CalculateSlicesHeight(Height, contentBegin);
            rulerBegin = CalculateRulerBegin(controlWidth, separatorWidth);
            rulerWidth = CalculateRulerWidth(Width, rulerBegin);
        }

        private int CalculateSlicesBegin(int rulerHeight, int separatorHeight)
        { return rulerHeight + separatorHeight; }

        private int CalculateSlicesHeight(int height, int slicesBegin)
        { return height - slicesBegin; }

        private int CalculateRulerBegin(int controlWidth, int separatorWidth)
        { return controlWidth + separatorWidth; }

        private int CalculateRulerWidth(int width, int rulerBegin)
        { return width - rulerBegin; }

        public void InvalidateSlicesContainerRectangle(Rectangle rect)
        {
            if (rect.Y < contentBegin + contentHeight && rect.Bottom > contentBegin)
            {
                if (rect.Y < contentBegin)
                {
                    rect.Height = rect.Height - (contentBegin - rect.Y);
                    rect.Y = contentBegin;
                }
                Invalidate(rect);
            }
        }
    }
}
