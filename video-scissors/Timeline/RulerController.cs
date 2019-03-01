﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class RulerController : IController
    {
        private int markWidth = 2;
        private Color markColor = Color.Black;
        private Color backColor = Color.AntiqueWhite;        

        private TimelineController timeline;
        private TimelineContent timelineContent;
        
        private int[] markPattern;
        private Rectangle oldRect;
        private Rectangle oldScreenRect;
        private Rectangle rulerRectangle;
        private int oldScroll;

        public event EventHandler SizeChanged;

        public int TimelineLength { get { return timeline.TimelineLength; } }
        public float TimelineZoom { get { return timeline.TimelineZoom; } }
        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }


        public Rectangle Rectangle
        { get { return rulerRectangle; } }

        public Rectangle ParentRectangle
        { get { return timelineContent.RulerContainerRectangle; } }

        public int MarkWidth
        {
            get { return markWidth; }
            set
            {
                if (value > 0)
                {
                    markWidth = value;
                    UpdateUI();
                }
            }
        }

        public Color BackColor
        {
            get { return backColor; }
            set
            {
                if (value != null)
                {
                    backColor = value;
                    UpdateUI();
                }
            }
        }

        public Color ForeColor
        {
            get { return markColor; }
            set
            {
                if (value != null)
                {
                    markColor = value;
                    UpdateUI();
                }
            }
        }

        public TimelineContent TimelineContent { get { return timelineContent; } }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;
        
        public event EventHandler LocationChanged;
        private void InvokeLocationChanged()
        { if (LocationChanged != null) LocationChanged.Invoke(this, EventArgs.Empty); }

        internal RulerController(TimelineController timeline)
        {
            this.timeline = timeline;

            timelineContent = timeline.TimelineContent;
            oldRect = timelineContent.RulerContainerRectangle;
            oldScreenRect = GetScreenRectangle();
            oldScroll = timelineContent.HorizontalScroll;

            rulerRectangle = new Rectangle();
            UpdateCache();

            if (ProjectFramerate == 24)
            {
                markPattern = new int[] { 20, 35, 30, 35, 25, 35, 30, 35 };
            }
            else if (ProjectFramerate == 25)
            {
                markPattern = new int[] { 20, 35, 35, 35, 35, };
            }
            else if (ProjectFramerate == 30)
            {
                markPattern = new int[] { 20, 35, 35, 35, 35, 30, 35, 35, 35, 35 };
            }
            else
            {
                markPattern = new int[] { 25 };
            }

            timelineContent.Paint += TimelineContent_Paint;
            timelineContent.Resize += TimelineContent_Resize;
            timeline.TimelineZoomChanged += Timeline_TimelineZoomChanged;
            timeline.TimelineLengthChanged += Timeline_TimelineLengthChanged;
            timeline.LocationChanged += TimelineContent_LocationChanged;

            UpdateUI();
        }

        private void Timeline_TimelineZoomChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void Timeline_TimelineLengthChanged(object sender, EventArgs e)
        {
            UpdateCache();
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }

        private void TimelineContent_LocationChanged(object sender, EventArgs e)
        {
            if (timelineContent.HorizontalScroll != oldScroll)
            {
                oldScroll = timelineContent.HorizontalScroll;
                UpdateCache();
                InvokeLocationChanged();
                timelineContent.Invalidate(timelineContent.RulerContainerRectangle);
            }
        }

        private void TimelineContent_Resize(object sender, EventArgs e)
        {
            if (!oldRect.Equals(timelineContent.RulerContainerRectangle))
            {
                UpdateCache();
                UpdateUI();
            }
        }

        private void UpdateCache()
        {
            rulerRectangle.X = timeline.Rectangle.X;
            rulerRectangle.Y = timeline.Rectangle.Y;
            rulerRectangle.Width = timeline.Rectangle.Width;
            rulerRectangle.Height = timelineContent.RulerContainerRectangle.Height;
        }

        public void UpdateUI()
        {
            Rectangle screenRect = GetScreenRectangle();

            //if location or height has changed the entirety of old and new areas need redrawing
            if (screenRect.Location != oldScreenRect.Location || screenRect.Height != oldScreenRect.Height)
            {
                timelineContent.Invalidate(oldRect);
                timelineContent.Invalidate(ParentRectangle);
            }
            //if width has increased then only the new area needs redrawing
            else if (screenRect.Width != oldScreenRect.Width)
            {
                if (screenRect.Width > oldScreenRect.Width)
                {
                    timelineContent.Invalidate(new Rectangle(
                        oldScreenRect.Width, oldScreenRect.Y, 
                        screenRect.X - oldScreenRect.X, oldScreenRect.Height));
                }
            }
            //if nothing has changed then there's no need to redraw
            else return;

            oldRect = ParentRectangle;
            oldScreenRect = screenRect;
        }

        private void TimelineContent_Paint(object sender, PaintEventArgs e)
        {
            if (e.ClipRectangle.IntersectsWith(ParentRectangle))
            {
                Rectangle rect = ParentRectangle;
                Brush markBrush = new SolidBrush(markColor);
                Brush brush = new SolidBrush(backColor);

                e.Graphics.FillRectangle(brush, new Rectangle(
                    e.ClipRectangle.X, ParentRectangle.Y,
                    e.ClipRectangle.Width, ParentRectangle.Height));

                int startX = e.ClipRectangle.X - rulerRectangle.X;
                int i = (int)(startX / TimelineZoom);
                int m = Math.Min(rect.Width + startX, rulerRectangle.Width);
                int x;
                for (; (x = (int)(i * TimelineZoom)) < m; i += 1)
                {
                    int y1 = i % ProjectFramerate == 0 ? 0 : markPattern[i % markPattern.Length];
                    Rectangle markRect = new Rectangle(rulerRectangle.X + x, y1, markWidth, 40 - y1);
                    e.Graphics.FillRectangle(markBrush, markRect);
                }

                e.Dispose();
            }
        }

        private Rectangle GetScreenRectangle()
        {
            return timelineContent.RectangleToScreen(timelineContent.ClientRectangle);
        }

        public void Dispose()
        {
            
        }
    }
}
