﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    internal class ItemController : IFrameController
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
                    if (value + itemLength < layer.TimelineLength)
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
                if (value < layer.TimelineLength)
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
                    if (startPosition + value < layer.TimelineLength)
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

        public int TimelineLength { get { return layer.TimelineLength; } }
        public int ProjectFramerate { get { return layer.ProjectFramerate; } }
        public float TimelineZoom { get { return layer.TimelineZoom; } }
        public int ProjectFrameWidth { get { return layer.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return layer.ProjectFrameHeight; } }

        internal ItemController(LayerController layer, int startPosition, int length)
        {
            oldZoom = -1;
            oldItemLength = -1;
            oldStartPosition = -1;

            this.layer = layer;
            contentsPanel = layer.ItemContentsPanel;
            content = new ItemContent();
            contentsPanel.Controls.Add(content);

            this.startPosition = startPosition;
            ItemLength = length;

            UpdateUI();
        }

        public void UpdateUI()
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
            if (TimelineZoom != oldZoom)
            {
                oldZoom = TimelineZoom;
                updatePos = true;
                updateWidth = true;
            }

            if (updatePos)
            {
                content.Left = (int)(startPosition * TimelineZoom);
            }

            if (updateWidth)
            {
                content.Width = (int)(itemLength * TimelineZoom);
            }
        }

        public void Dispose()
        {
            contentsPanel.Controls.Remove(content);
            content.Dispose();
        }

        public Frame ProcessFrame(Frame frame, int position)
        {
            throw new NotImplementedException();
        }
    }
}