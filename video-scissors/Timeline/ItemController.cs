using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scissors.Config;

namespace Scissors.Timeline
{
    internal class ItemController : IFrameController
    {
        private LayerController layer;
        private RectangleProvider rectangleProvider;

        private Rectangle itemRectangle;
        private Rectangle oldRectangle;
        private Color backColor;      

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
                    if (value + itemLength <= layer.TimelineLength)
                    {
                        startPosition = value;
                        endPosition = startPosition + itemLength;
                        UpdateCache();
                        InvokeLocationChanged(new LocationChangeEventArgs(true, false));
                        UpdateUI();
                    }
                }
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
                        UpdateCache();
                        InvokeSizeChanged();
                        UpdateUI();
                    }
                }
            }
        }

        internal int ItemLength
        {
            get { return endPosition - startPosition; }
            set
            {
                if (value > 0)
                {
                    if (startPosition + value <= layer.TimelineLength)
                    {
                        itemLength = value;
                        endPosition = startPosition + value;
                        UpdateCache();
                        InvokeSizeChanged();
                        UpdateUI();
                    }
                }
            }
        }

        public int TimelineLength { get { return layer.TimelineLength; } }
        public int ProjectFramerate { get { return layer.ProjectFramerate; } }
        public float TimelineZoom { get { return layer.TimelineZoom; } }
        public int ProjectFrameWidth { get { return layer.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return layer.ProjectFrameHeight; } }

        public RectangleProvider RectangleProvider => throw new NotImplementedException();

        public Color BackColor { get { return backColor; } set { backColor = value; } }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Rectangle Rectangle
        { get { return itemRectangle; } }

        public Rectangle ParentRectangle
        { get { return rectangleProvider.ContentContainerRectangle; } }

        public TimelineController ParentTimeline { get { return layer.ParentTimeline; } }
        public LayerController ParentLayer { get { return layer; } }

        public Region FullOccupiedRegion
        { get { return new Region(itemRectangle); } }

        public Region FullParentRegion
        { get { return new Region(ParentRectangle); } }

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(LocationChangeEventArgs e)
        { if (LocationChanged != null) LocationChanged.Invoke(this, e); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        internal ItemController(LayerController layer, int startPosition, int length)
        {
            backColor = Color.DarkGray;

            this.layer = layer;
            rectangleProvider = layer.RectangleProvider;
            UpdateCache();
            oldRectangle = itemRectangle;

            rectangleProvider.Paint += TimelineContent_Paint;
            rectangleProvider.Resize += TimelineContent_Resize;

            AddLayerEvents();

            this.startPosition = startPosition;
            ItemLength = length;

            UpdateUI();
        }

        private void AddLayerEvents()
        {
            layer.TimelineZoomChanged += Layer_TimelineZoomChanged;
            layer.TimelineLengthChanged += Layer_TimelineLengthChanged;
            layer.LocationChanged += Layer_LocationChanged;
            layer.Disowning += Layer_Disowning;
        }

        private void RemoveLayerEvents()
        {
            layer.TimelineZoomChanged -= Layer_TimelineZoomChanged;
            layer.TimelineLengthChanged -= Layer_TimelineLengthChanged;
            layer.LocationChanged -= Layer_LocationChanged;
            layer.Disowning -= Layer_Disowning;
        }

        private void Layer_LocationChanged(object sender, LocationChangeEventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged(e);
            UpdateUI();
        }

        private void Layer_TimelineZoomChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void Layer_TimelineLengthChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            UpdateCache();
            UpdateUI();
        }
        
        private void UpdateCache()
        {
            itemRectangle.X = (int)(startPosition * TimelineZoom) + layer.Rectangle.X;
            itemRectangle.Y = layer.Rectangle.Y;
            itemRectangle.Width = (int)(itemLength * TimelineZoom);
            itemRectangle.Height = layer.Rectangle.Height;
        }

        public void UpdateUI()
        {
            rectangleProvider.InvalidateContentContainerRectangle(oldRectangle);
            rectangleProvider.InvalidateContentContainerRectangle(itemRectangle);
            oldRectangle = itemRectangle;
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(itemRectangle))
            {
                Region graphicsClip = e.Graphics.Clip;
                Region region = new Region(ParentRectangle);
                region.Exclude(ParentTimeline.Cursor.FullOccupiedRegion);
                e.Graphics.Clip = region;

                Brush brush = new SolidBrush(backColor);
                e.Graphics.FillRectangle(brush, itemRectangle);

                e.Graphics.Clip = graphicsClip;
            }
        }

        public void ProcessFrame(Frame frame, int position)
        {
            
        }
        
        public int GetRelativePosition(int timelinePosition)
        {
            return timelinePosition - startPosition;
        }

        public bool IsOverlapping(int timelinePosition)
        {
            return timelinePosition >= startPosition && timelinePosition < endPosition;
        }

        private void Layer_Disowning(object sender, DisownEventArgs e)
        {
            if (e.DisownedChild == this)
            {
                RemoveLayerEvents();
                layer = e.NewParent as LayerController;
                AddLayerEvents();
                UpdateCache();
                UpdateUI();
            }
        }
    }
}
