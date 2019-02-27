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
    public partial class FancyScrollbar : UserControl
    {
        private ScrollDirection scrollDirection = ScrollDirection.LeftToRight;
        private int minimum = 0;
        private int maximum = 10;
        private int value = 3;
        private int scrollWidth = 3;

        Brush brush;
        private float relation;
        private int scrollerLeftX;
        private int scrollerRightX;
        private int scrollerWidthX;
        Rectangle oldRectangle = new Rectangle(0, 0, 1, 1);

        private bool dragging = false;
        private int dragOffset = 0;

        public int Minimum {
            get { return minimum; }
            set
            {
                if (value <= maximum - scrollWidth)
                {
                    bool changeValue = this.value < value;
                    if (changeValue) this.value = value;
                    minimum = value;
                    UpdateCacheAndUI();
                    if (changeValue) InvokeScroll();
                }
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value >= minimum + scrollWidth)
                {
                    bool changeValue = this.value + scrollWidth > value;
                    if (changeValue) this.value = maximum - scrollWidth;
                    maximum = value;
                    UpdateCacheAndUI();
                    if (changeValue) InvokeScroll();
                }
            }
        }

        public int Value
        {
            get { return value; }
            set
            {
                if (value >= minimum && value <= maximum - scrollWidth)
                {                    
                    this.value = value;
                    UpdateScroller();
                }
            }
        }

        public ScrollDirection ScrollDirection
        {
            get { return scrollDirection; }
            set
            {
                scrollDirection = value;
                UpdateCacheAndUI();
            }
        }

        public int ScrollWidth
        {
            get { return scrollWidth; }
            set
            {
                int diff = maximum - minimum;
                if (value <= diff) scrollWidth = value;
                else scrollWidth = diff;
                UpdateCacheAndUI();
            }
        }

        public event EventHandler<ScrollEventArgs> Scroll;

        public FancyScrollbar()
        {
            InitializeComponent();
            
            brush = new SolidBrush(ForeColor);
            pictureBox1.Image = new Bitmap(Width, Height);

            UpdateCacheAndUI();
        }

        private void FancyScrollbar_UpdateUI(object sender, EventArgs e)
        {
            UpdateCacheAndUI();
        }

        private void FancyScrollbar_Resize(object sender, EventArgs e)
        {
            pictureBox1.Image.Dispose();
            pictureBox1.Image = new Bitmap(Width, Height);
            UpdateCacheAndUI();
        }

        private void FancyScrollbar_ForeColorChanged(object sender, EventArgs e)
        {
            brush = new SolidBrush(ForeColor);
            UpdateCacheAndUI();
        }

        private void FancyScrollbar_MouseDown(object sender, MouseEventArgs e)
        {
            int x = scrollDirection == ScrollDirection.LeftToRight ? e.X : e.Y;

            if (x >= scrollerLeftX && x <= scrollerRightX)
            {
                dragging = true;
                dragOffset = x - scrollerLeftX;
            }
        }

        private void FancyScrollbar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!dragging) return;

            int x = scrollDirection == ScrollDirection.LeftToRight ? e.X : e.Y;

            int proposedValue = (int)((x - dragOffset) / relation);

            if (proposedValue != value)
            {
                if (proposedValue >= 0)
                    proposedValue = Math.Min(proposedValue, maximum - scrollWidth);
                else proposedValue = 0;

                if (proposedValue != value)
                {
                    Value = proposedValue;
                    InvokeScroll();
                }
            }
        }

        private void InvokeScroll()
        {
            if (Scroll != null) Scroll.Invoke(this, new ScrollEventArgs(ScrollEventType.SmallIncrement, value));
        }

        private void FancyScrollbar_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void UpdateCacheAndUI()
        {
            int width = scrollDirection == ScrollDirection.LeftToRight ? Width : Height;
            relation = width / (float)(maximum - minimum);            
            UpdateScrollerCache();
            UpdateUI();
        }

        private void UpdateScrollerCache()
        {
            int left = (value - minimum);
            scrollerLeftX = (int)(left * relation);
            scrollerWidthX = (int)Math.Ceiling(scrollWidth * relation);
            scrollerRightX = (int)((left + scrollWidth) * relation);
        }

        private void UpdateScroller()
        {
            UpdateScrollerCache();
            UpdateUI();
        }        

        private void UpdateUI()
        {
            Bitmap bitmap = (Bitmap)pictureBox1.Image;

            Rectangle scrollRectangle = scrollDirection == ScrollDirection.LeftToRight
                ? new Rectangle(scrollerLeftX, 0, scrollerWidthX, Height)
                : new Rectangle(0, scrollerLeftX, Width, scrollerWidthX);

            bitmap.MakeTransparent(ForeColor);

            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.FillRectangle(brush, scrollRectangle);
            }

            pictureBox1.Invalidate(oldRectangle);
            pictureBox1.Invalidate(scrollRectangle);

            oldRectangle = scrollRectangle;
        }
    }
}
