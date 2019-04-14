using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scissors.EffectAPI;
using Scissors.Objects;

namespace Scissors.Effects
{
    class ImageRenderer : IEffect
    {
        private Image image;

        public string FilePath { get; set; }
        public Point Location { get; set; }
        public Size Size { get; set; }

        public void OnLoad()
        {
            image = Image.FromFile(FilePath);
        }

        public void OnUnload()
        {
            image.Dispose();
        }

        public void ProcessFrame(Frame originalFrame)
        {
            using (Graphics gfx = Graphics.FromImage(originalFrame.Bitmap))
            {
                gfx.DrawImageUnscaled(image, new Rectangle(Location, Size));
            }
        }
    }
}
