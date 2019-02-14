using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class TimelineController : IController
    {
        private Timeline timeline;
        private List<SliceController> slices;
        private RulerController ruler;
        private int length;
        private float zoom;
        private int framerate;
        private int frameWidth;
        private int frameHeight;

        internal int SliceCount { get { return slices.Count; } }
        internal FlowLayoutPanel ControlsPanel { get { return timeline.ControlsPanel; } }
        internal FlowLayoutPanel ContentsPanel { get { return timeline.ContentsPanel; } }
        internal FlowLayoutPanel RulerPanel { get { return timeline.RulerPanel; } }
        
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
        
        private void Initialize(Timeline timeline, int length, float zoom, int framerate, int frameWidth, int frameHeight)
        {
            this.length = length;
            this.zoom = zoom;
            this.framerate = framerate;
            this.frameWidth = frameWidth;
            this.frameHeight = frameHeight;

            this.timeline = timeline;
            slices = new List<SliceController>();
            CreateSlice();

            ruler = new RulerController(this);

            UpdateUI();
        }

        internal TimelineController(Timeline timeline)
        {
            Initialize(timeline, 
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom, 
                GlobalConfig.DefaultProjectFramerate, 
                GlobalConfig.DefaultProjectFrameWidth, 
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline,
                GlobalConfig.DefaultTimelineLength,
                GlobalConfig.DefaultTimelineZoom,
                framerate,
                frameWidth,
                frameHeight);
        }

        internal TimelineController(Timeline timeline, int length)
        {
            Initialize(timeline,
                length,
                GlobalConfig.DefaultTimelineZoom,
                GlobalConfig.DefaultProjectFramerate,
                GlobalConfig.DefaultProjectFrameWidth,
                GlobalConfig.DefaultProjectFrameHeight);
        }

        internal TimelineController(Timeline timeline, int length, int framerate, int frameWidth, int frameHeight)
        {
            Initialize(timeline, length, GlobalConfig.DefaultTimelineZoom, framerate, frameWidth, frameHeight);
        }

        private void SetLength(int length)
        {
            //check if length can be changed or not

            this.length = length;
            UpdateUI();
        }
        
        private void SetZoom(float zoom)
        {
            this.zoom = zoom;
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
            Frame processed = new Frame();

            foreach (SliceController slice in slices)
            {
                Frame temp = slice.ProcessFrame(processed, position);
                processed.Dispose();
                processed = temp;
            }

            return processed;
        }

        public void UpdateUI()
        {
            foreach (SliceController slice in slices)
            {
                slice.UpdateUI();
            }

            ruler.UpdateUI();
        }

        public void Dispose()
        {
            foreach (SliceController slice in slices)
            {
                slice.Dispose();
            }
            ruler.Dispose();
            timeline.Dispose();
        }
    }
}
