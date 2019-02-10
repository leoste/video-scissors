using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class TimelineController
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

        /// <summary>
        /// Timeline length in frames.
        /// </summary>
        public int Length
        {
            get { return length; }
            set { SetLength(value); }
        }
                
        /// <summary>
        /// Timeline horizontal zoom.
        /// </summary>
        public float Zoom
        {
            get { return zoom; }
            set { if (value > 0) SetZoom(value); }
        }

        public int Framerate { get { return framerate; } }
        public int FrameWidth { get { return frameWidth; } }
        public int FrameHeight { get { return frameHeight; } }

        internal TimelineController(Timeline timeline)
        {
            //length = 1800;
            length = 60;
            zoom = 10;
            framerate = 30;

            this.timeline = timeline;
            slices = new List<SliceController>();
            CreateSlice();

            ruler = new RulerController(this);

            UpdateUI();
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

        internal void UpdateUI()
        {
            foreach (SliceController slice in slices)
            {
                slice.UpdateUI();
            }

            ruler.UpdateUI();
        }
    }
}
