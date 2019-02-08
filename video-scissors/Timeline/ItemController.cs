using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    internal class ItemController : IDisposable
    {
        private static ArgumentException EndTooBigException { get { return new ArgumentException("End position must be smaller than timeline length."); } }
        private static ArgumentException EndTooSmallException { get { return new ArgumentException("End position must be larger than start position."); } }
        private static ArgumentException LengthTooSmallException { get { return new ArgumentException("Length must be at least 1."); } }
        private static ArgumentException StartTooSmallException { get { return new ArgumentException("Start position can't be below 0."); } }

        private LayerController layer;
        private Panel contentsPanel;

        private ItemContent content;

        private int startPosition;
        private int endPosition;
        private int length;
        
        internal int StartPosition
        {
            get { return startPosition; }
            set
            {
                if (value >= 0)
                {
                    if (value + length < layer.Length)
                    {
                        startPosition = value;
                        endPosition = startPosition + length;
                        UpdateUI();
                    }
                    else throw EndTooBigException;
                }
                else throw StartTooSmallException;
            }
        }

        internal int EndPosition
        {
            get { return endPosition; }
            set
            {
                if (value < layer.Length)
                {
                    if (value > startPosition)
                    {
                        endPosition = value;
                        length = endPosition - startPosition;
                        UpdateUI();
                    }
                    else throw EndTooSmallException;
                }
                else throw EndTooBigException;
            }
        }

        internal int Length
        {
            get { return endPosition - startPosition; }
            set
            {
                if (value > 0)
                {
                    if (startPosition + value < layer.Length)
                    {
                        length = value;
                        endPosition = startPosition + value;
                        UpdateUI();
                    }
                    else throw EndTooBigException;
                }
                else throw LengthTooSmallException;
            }
        }

        internal ItemController(LayerController layer)
        {
            this.layer = layer;
            contentsPanel = layer.ItemContentsPanel;
            content = new ItemContent();
        }

        internal void UpdateUI()
        {

        }

        public void Dispose()
        {
            contentsPanel.Controls.Remove(content);
            content.Dispose();
        }
    }
}
