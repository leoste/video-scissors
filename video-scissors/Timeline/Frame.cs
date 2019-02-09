using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scissors.Timeline
{
    class Frame: IDisposable
    {
        private Bitmap bitmap;
        private bool sameAsLastFrame;

        internal Bitmap Bitmap { get { return bitmap; } }
        internal bool SameAsLastFrame { get { return sameAsLastFrame; } }

        private void Initialize(Bitmap bitmap, bool sameAsLastFrame)
        {
            this.bitmap = new Bitmap(bitmap);
            this.sameAsLastFrame = sameAsLastFrame;
        }

        internal Frame()
        {
            Initialize(null, false);
        }

        internal Frame(Bitmap bitmap, bool sameAsLastFrame = false)
        {
            Initialize(bitmap, sameAsLastFrame);
        }

        internal Frame(Frame frame)
        {
            Initialize(frame.Bitmap, frame.SameAsLastFrame);
        }

        public void Dispose()
        {
            bitmap.Dispose();
        }
    }
}
