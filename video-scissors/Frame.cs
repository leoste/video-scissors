using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors
{
    class Frame : IDisposable
    {
        private Bitmap bitmap;
        private bool sameAsLastFrame;

        internal Bitmap Bitmap { get { return bitmap; } }
        public bool SameAsLastFrame { get { return sameAsLastFrame; } set { sameAsLastFrame = value; } }

        private void Initialize(Bitmap bitmap, bool sameAsLastFrame)
        {
            this.bitmap = bitmap;
            this.sameAsLastFrame = sameAsLastFrame;
        }

        internal Frame(Bitmap bitmap, bool sameAsLastFrame = false)
        {
            Initialize(bitmap, sameAsLastFrame);
        }

        internal Frame(Frame frame)
        {
            Initialize(new Bitmap(frame.Bitmap), frame.SameAsLastFrame);
        }

        public void Dispose()
        {
            bitmap.Dispose();
        }

        public void CombineWith(Frame frame)
        {
            using (Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.DrawImage(frame.bitmap, 0, 0, frame.bitmap.Width, frame.bitmap.Height);                
            }
        }
    }
}
