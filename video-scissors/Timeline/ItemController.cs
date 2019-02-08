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
        
        private int oldItemLength;
        private int oldStartPosition;
        private float oldZoom;        

        private int startPosition;
        private int endPosition;
        private int itemLength;
        
        internal int StartPosition
        {
            get { return startPosition; }
            set
            {
                if (value >= 0)
                {
                    if (value + itemLength < layer.Length)
                    {
                        startPosition = value;
                        endPosition = startPosition + itemLength;
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
                        itemLength = endPosition - startPosition;
                        UpdateUI();
                    }
                    else throw EndTooSmallException;
                }
                else throw EndTooBigException;
            }
        }

        internal int ItemLength
        {
            get { return endPosition - startPosition; }
            set
            {
                if (value > 0)
                {
                    if (startPosition + value < layer.Length)
                    {
                        itemLength = value;
                        endPosition = startPosition + value;
                        UpdateUI();
                    }
                    else throw EndTooBigException;
                }
                else throw LengthTooSmallException;
            }
        }

        internal int Length { get { return layer.Length; } }
        internal int Framerate { get { return layer.Framerate; } }
        internal float Zoom { get { return layer.Zoom; } }

        internal ItemController(LayerController layer)
        {
            oldZoom = -1;
            oldItemLength = -1;
            oldStartPosition = -1;

            this.layer = layer;
            contentsPanel = layer.ItemContentsPanel;
            content = new ItemContent();
        }

        internal void UpdateUI()
        {
            bool updatePos = false;
            bool updateWidth = false;

            if (startPosition != oldStartPosition)
            {
                oldStartPosition = startPosition;
                updatePos = true;
            }
            if (itemLength != oldItemLength)
            {
                oldItemLength = ItemLength;
                updateWidth = true;
            }
            if (Zoom != oldZoom)
            {
                oldZoom = Zoom;
                updatePos = true;
                updateWidth = true;
            }

            if (updatePos)
            {
                content.Left = (int)(startPosition * Zoom);
            }

            if (updateWidth)
            {
                content.Width = (int)(itemLength * Zoom);
            }
        }

        public void Dispose()
        {
            contentsPanel.Controls.Remove(content);
            content.Dispose();
        }
    }
}
