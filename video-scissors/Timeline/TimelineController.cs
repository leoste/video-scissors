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
    class TimelineController : IControlController, IChildController
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
        private TimelineControl control;
        private TimelineContent content;

        internal int SliceCount { get { return slices.Count; } }
        internal FlowLayoutPanel ControlsPanel { get { return timeline.ControlsPanel; } }
        internal FlowLayoutPanel ContentsPanel { get { return timeline.ContentsPanel; } }
        internal FlowLayoutPanel RulerPanel { get { return timeline.RulerPanel; } }
        internal Panel CursorPanel { get { return timeline.CursorPanel; } }
        
        public int TimelineLength
        {
            get { return length; }
            set { SetLength(value); }
        }

        public float TimelineZoom
        {
            get { return zoom; }
            set { if (value > 0) SetZoom(value); }
        }

        public int ProjectFramerate { get { return framerate; } }
        public int ProjectFrameWidth { get { return frameWidth; } }
        public int ProjectFrameHeight { get { return frameHeight; } }
        public bool IsLocked { get; }
        public bool IsVisible { get; }
        public TimelineContent TimelineContent { get { return content; } }
        public TimelineControl TimelineControl { get { return control; } }

        public Color BackColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event EventHandler TimelineLengthChanged;
        public event EventHandler TimelineZoomChanged;

        private void Initialize(Timeline timeline, TimelineControl control, TimelineContent content, int length, float zoom, int framerate, int frameWidth, int frameHeight)
        {
            this.length = length;
            this.zoom = zoom;
            this.framerate = framerate;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;
            this.control = control;
            this.content = content;

            this.timeline = timeline;
            slices = new List<SliceController>();
            CreateSlice();
            CreateSlice();
            CreateSlice();
            CreateSlice();

            ruler = new RulerController(this);

            //cursor = new CursorController(this);

            UpdateUI();
        }

        internal TimelineController(Timeline timeline, TimelineControl control, TimelineContent content)
        {
            Initialize(timeline, control, content,
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom, 
                GlobalConfig.DefaultProjectFramerate, 
                GlobalConfig.DefaultProjectFrameWidth, 
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, TimelineControl control, TimelineContent content, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline, control, content,
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom,
                framerate,
                frameWidth,
                frameHeight);
        }

        internal TimelineController(Timeline timeline, TimelineControl control, TimelineContent content, int length)
        {
            Initialize(timeline, control, content, length,
                GlobalConfig.DefaultTimelineZoom,
                GlobalConfig.DefaultProjectFramerate,
                GlobalConfig.DefaultProjectFrameWidth,
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, TimelineControl control, TimelineContent content, int length, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline, control, content, length, GlobalConfig.DefaultTimelineZoom, framerate, frameWidth, frameHeight);
        }

        private void SetLength(int length)
        {
            //check if length can be changed or not

            this.length = length;
            if (TimelineLengthChanged != null) TimelineLengthChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
        }
        
        private void SetZoom(float zoom)
        {
            this.zoom = zoom;
            if (TimelineZoomChanged != null) TimelineZoomChanged.Invoke(this, EventArgs.Empty);
            UpdateUI();
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
            slices.Insert(id, new SliceController(this, id));
            for (int i = SliceCount - 1; i > id; i -= 1)
            {
                slices[i].SetId(i);
            }
        }

        internal void RemoveSlice(int id)
        {
            SliceController slice = slices[id];
            slices.Remove(slice);
            slice.Dispose();
            for (int i = id; i < SliceCount; i += 1)
            {
                slices[i].SetId(i);
            }

            if (SliceCount == 0)
            {
                slices.Add(new SliceController(this));
            }
        }

        internal void SwapSlices(int id1, int id2)
        {
            slices[id1].SetId(id2);
            slices[id2].SetId(id1);

            SliceController slice1 = slices[id1];
            slices[id1] = slices[id2];
            slices[id2] = slice1;
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
            /*foreach (SliceController slice in slices)
            {
                slice.UpdateUI();
            }*/

            ruler.UpdateUI();
            //cursor.UpdateUI();
        }

        public void Dispose()
        {
            foreach (SliceController slice in slices)
            {
                slice.Dispose();
            }
            ruler.Dispose();
            cursor.Dispose();
            timeline.Dispose();
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
    }
}
