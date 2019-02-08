using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Design;

namespace Scissors.Timeline
{
    public partial class Timeline : UserControl
    {
        private List<SliceController> slices;
        private int length = 1800;
        private int framerate = 30;
        private float zoom = 10;

        internal int SliceCount { get { return slices.Count; } }
        internal FlowLayoutPanel ControlsPanel { get { return optionScroll; } }
        internal FlowLayoutPanel ContentsPanel { get { return sliceScroll; } }
        
        /// <summary>
        /// Timeline length in frames.
        /// </summary>
        public int Length {
            get { return length; }
            set { SetLength(value); }
        }
        
        /// <summary>
        /// Timeline framerate. Changing this with an existing project will screw up its speed.
        /// </summary>
        public int Framerate {
            get { return framerate; }
            set { SetFramerate(value); }
        }
        
        /// <summary>
        /// Timeline horizontal zoom.
        /// </summary>
        public float Zoom {
            get { return zoom; }
            set { SetZoom(value); }
        }

        public Timeline()
        {
            InitializeComponent();
            slices = new List<SliceController>();
            CreateSlice();
        }

        private void SetLength(int length)
        {
            //check if length can be changed or not

            this.length = length;
        }

        private void SetFramerate(int framerate)
        {
            this.framerate = framerate;

            // update ruler
        }

        private void SetZoom(float zoom)
        {
            this.zoom = zoom;

            // resize slices, layers
            // resize and reposition items
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
    }
}
