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

namespace Scissors.UserControls
{
    public partial class Timeline : UserControl
    {
        private List<SliceController> slices;

        internal int SliceCount { get { return slices.Count; } }
        internal FlowLayoutPanel ControlsPanel { get { return optionScroll; } }
        internal FlowLayoutPanel ContentsPanel { get { return sliceScroll; } }

        public Timeline()
        {
            InitializeComponent();
            slices = new List<SliceController>();
            CreateSlice();
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
