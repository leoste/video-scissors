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
    class TimelineController : IController, IChildController
    {
        private Timeline timeline;
        private List<SliceController> slices;
        private RulerController ruler;
        private CursorController cursor;
        private int length;
        private float zoom;
        private int framerate;
        private int frameWidth;
        private int frameHeight;
        private RectangleProvider rectangleProvider;
        private Rectangle timelineRectangle;

        public int SliceCount { get { return slices.Count; } }

        public int TimelineLength
        {
            get { return length; }
            set
            {
                //add better check in the future to prevent size change when items would be left outside
                if (value > 0)
                {
                    length = value;
                    UpdateCache();
                    if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
                    InvokeSizeChanged();
                    UpdateUI();
                }
            }
        }

        public float TimelineZoom
        {
            get { return zoom; }
            set
            {
                if (value > 0)
                {
                    zoom = value;
                    UpdateCache();
                    if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
                    InvokeSizeChanged();
                    UpdateUI();
                }
            }
        }

        public int ProjectFramerate { get { return framerate; } }
        public int ProjectFrameWidth { get { return frameWidth; } }
        public int ProjectFrameHeight { get { return frameHeight; } }
        public bool IsLocked { get; }
        public bool IsVisible { get; }
        public RectangleProvider RectangleProvider { get { return rectangleProvider; } }
        public Color BackColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public TimelineController ParentTimeline { get { return this; } }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;
        public event EventHandler<DisownEventArgs> Disowning;

        public event EventHandler SizeChanged;
        private void InvokeSizeChanged()
        { if (SizeChanged != null) SizeChanged.Invoke(this, EventArgs.Empty); }

        public event EventHandler<LocationChangeEventArgs> LocationChanged;
        private void InvokeLocationChanged(bool leftChanged, bool topChanged)
        { if (LocationChanged != null) LocationChanged.Invoke(this, new LocationChangeEventArgs(leftChanged, topChanged)); }

        public Rectangle SlicesRectangle
        {
            get
            {
                Rectangle rectangle = new Rectangle();

                if (slices.Count > 0)
                {
                    SliceController firstSlice = slices.First();
                    rectangle.X = firstSlice.Rectangle.X;
                    rectangle.Y = firstSlice.Rectangle.Y;
                    rectangle.Width = firstSlice.Rectangle.Width;
                    SliceController lastSlice = slices.Last();
                    rectangle.Height = lastSlice.Rectangle.Bottom - firstSlice.Rectangle.Y;
                }

                return rectangle;
            }
        }

        public Rectangle SlicesControlRectangle
        {
            get
            {
                Rectangle rectangle = new Rectangle();

                SliceController firstSlice = slices.First();
                rectangle.X = firstSlice.ControlRectangle.X;
                rectangle.Y = firstSlice.ControlRectangle.Y;
                rectangle.Width = firstSlice.ControlRectangle.Width;
                SliceController lastSlice = slices.Last();
                rectangle.Height = lastSlice.ControlRectangle.Bottom - firstSlice.ControlRectangle.Y;

                return rectangle;
            }
        }
        
        public RulerController Ruler { get { return ruler; } }
        public CursorController Cursor { get { return cursor; } }

        public Rectangle Rectangle
        { get { return timelineRectangle; } }

        public Rectangle ParentRectangle
        { get { return RectangleProvider.HorizontalContainerRectangle; } }

        public Region FullOccupiedRegion
        {
            get
            {
                Region region = new Region();                
                region.Union(SlicesRectangle);
                region.Union(Ruler.FullOccupiedRegion);
                region.Union(SlicesControlRectangle);
                region.Union(cursor.FullOccupiedRegion);
                return region;
            }
        }

        public Region FullParentRegion
        {
            get
            {
                Region region = new Region();
                region.Union(rectangleProvider.RulerContainerRectangle);
                region.Union(rectangleProvider.ContentContainerRectangle);
                region.Union(rectangleProvider.ControlContainerRectangle);
                return region;
            }
        }

        private void Initialize(Timeline timeline, RectangleProvider rectangleProvider, int length, float zoom, int framerate, int frameWidth, int frameHeight)
        {
            this.length = length;
            this.zoom = zoom;
            this.framerate = framerate;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
                        
            this.rectangleProvider = rectangleProvider;
            rectangleProvider.VerticalScrolled += Content_VerticalScrolled;
            rectangleProvider.HorizontalScrolled += Content_HorizontalScrolled;

            timelineRectangle = new Rectangle();
            UpdateCache();

            this.timeline = timeline;
            slices = new List<SliceController>();
            CreateSlice();

            cursor = new CursorController(this);
            ruler = new RulerController(this);

            UpdateUI();
        }

        private void Content_HorizontalScrolled(object sender, ScrollEventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged(true, false);
        }

        private void Content_VerticalScrolled(object sender, ScrollEventArgs e)
        {
            UpdateCache();
            InvokeLocationChanged(false, true);
        }

        private void UpdateCache()
        {
            timelineRectangle.X = ParentRectangle.X - rectangleProvider.HorizontalScroll;
            timelineRectangle.Y = ParentRectangle.Y;
            timelineRectangle.Width = (int)(TimelineLength * TimelineZoom);
            timelineRectangle.Height = ParentRectangle.Height;
        }

        internal TimelineController(Timeline timeline, RectangleProvider rectangleProvider)
        {
            Initialize(timeline, rectangleProvider,
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom, 
                GlobalConfig.DefaultProjectFramerate, 
                GlobalConfig.DefaultProjectFrameWidth, 
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, RectangleProvider rectangleProvider, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline, rectangleProvider,
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom,
                framerate,
                frameWidth,
                frameHeight);
        }

        internal TimelineController(Timeline timeline, RectangleProvider rectangleProvider, int length)
        {
            Initialize(timeline, rectangleProvider, length,
                GlobalConfig.DefaultTimelineZoom,
                GlobalConfig.DefaultProjectFramerate,
                GlobalConfig.DefaultProjectFrameWidth,
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, RectangleProvider rectangleProvider, int length, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline, rectangleProvider, length, GlobalConfig.DefaultTimelineZoom, framerate, frameWidth, frameHeight);
        }

        internal int GetSliceId(SliceController slice)
        {
            return slices.IndexOf(slice);
        }

        internal void CreateSlice()
        {
            CreateSlice(SliceCount);
        }

        internal void CreateSlice(int id)
        {
            SliceController slice = new SliceController(this, id);
            slice.SizeChanged += Slice_SizeChanged;
            slices.Insert(id, slice);
            for (int i = SliceCount - 1; i > id; i -= 1)
            {
                slices[i].SetId(i);
            }
            InvokeLocationChanged(false, true);
            InvokeSizeChanged();
        }

        private void RemoveSlice(SliceController slice)
        {
            int id = slice.GetId();
            RemoveSlice(id);
        }

        private void RemoveSlice(int id)
        {
            SliceController slice = slices[id];
            slices.Remove(slice);
            for (int i = id; i < SliceCount; i += 1)
            {
                slices[i].SetId(i);                
            }

            if (SliceCount == 0)
            {
                slices.Add(new SliceController(this));
            }

            InvokeLocationChanged(false, true);
            InvokeSizeChanged();
        }

        private void Slice_SizeChanged(object sender, EventArgs e)
        {
            UpdateCache();
            InvokeSizeChanged();
        }

        private void CheckSliceExist(SliceController slice)
        {
            if (!slices.Exists(x => x == slice)) throw new ArgumentException("This timeline doesn't contain given slice.");
        }

        public void DeleteSlice(SliceController slice)
        {
            CheckSliceExist(slice);
            RemoveSlice(slice);
            slice.Delete();
            InvokeLocationChanged(false, true);
            rectangleProvider.Invalidate();
        }        

        internal void SwapSlices(int id1, int id2)
        {
            slices[id1].SetId(id2);
            slices[id2].SetId(id1);

            SliceController slice1 = slices[id1];
            slices[id1] = slices[id2];
            slices[id2] = slice1;

            //Bad solution but works for now
            InvokeLocationChanged(false, true);
        }
                
        internal void SwapSlices(SliceController slice1, SliceController slice2)
        {
            SwapSlices(slice1.GetId(), slice2.GetId());
        }

        internal Frame GetFrame(int position)
        {
            Frame frame = new Frame(new Bitmap(ProjectFrameWidth, ProjectFrameHeight), false);

            foreach (SliceController slice in slices)
            {                
                slice.ProcessFrame(frame, position);
            }

            return frame;
        }

        public void UpdateUI()
        {
            
        }

        public List<IController> GetChildren()
        {
            List<IController> children = new List<IController>();
            children.Add(cursor);
            children.Add(ruler);
            children.AddRange(slices);
            return children;
        }

        public List<IController> GetChildrenDeep()
        {
            List<IController> children = new List<IController>();

            children.Add(cursor);
            children.Add(ruler);

            foreach (SliceController slice in slices)
            {
                children.Add(slice);
                children.AddRange(slice.GetChildrenDeep());
            }

            return children;
        }

        public List<SliceController> GetSlices(int from = 0)
        {
            return GetSlices(from, slices.Count - from);
        }

        public List<SliceController> GetSlices(int from, int count)
        {
            List<SliceController> slices = this.slices.GetRange(from, count);
            return slices;
        }

        public void Delete()
        {
            rectangleProvider.VerticalScrolled -= Content_VerticalScrolled;
            rectangleProvider.HorizontalScrolled -= Content_HorizontalScrolled;
        }
    }
}
