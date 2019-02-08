using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.UserControls
{
    class SliceController: IDisposable
    {
        private int id;
        private Timeline timeline;
        private FlowLayoutPanel controlsPanel;
        private FlowLayoutPanel contentsPanel;

        private SliceControl control;
        private SliceContent content;

        private List<LayerController> layers;

        private void Initialize(Timeline timeline)
        {
            layers = new List<LayerController>();

            this.timeline = timeline;
            controlsPanel = timeline.ControlsPanel;
            contentsPanel = timeline.ContentsPanel;

            Color color = ColorProvider.GetRandomSliceColor();

            control = new SliceControl();
            control.BackColor = color;
            controlsPanel.Controls.Add(control);
            control.AddClicked += Control_AddClicked;
            control.RemoveClicked += Control_RemoveClicked;
            control.MoveUpClicked += Control_MoveUpClicked;
            control.MoveDownClicked += Control_MoveDownClicked;

            content = new SliceContent();
            control.BackColor = color;
            contentsPanel.Controls.Add(content);

            SetId();
        } 

        private void Control_AddClicked(object sender, EventArgs e)
        {
            timeline.CreateSlice(id + 1);
        }

        private void Control_RemoveClicked(object sender, EventArgs e)
        {
            timeline.RemoveSlice(id);
        }

        private void Control_MoveUpClicked(object sender, EventArgs e)
        {
            if (id > 0) timeline.SwapSlices(id, id - 1);
        }

        private void Control_MoveDownClicked(object sender, EventArgs e)
        {
            if (id < timeline.SliceCount - 1) timeline.SwapSlices(id, id + 1);
        }

        public void Dispose()
        {
            foreach (LayerController layer in layers)
            {
                layer.Dispose();
            }

            controlsPanel.Controls.Remove(control);
            contentsPanel.Controls.Remove(content);
            control.Dispose();
            content.Dispose();
        }

        internal SliceController(Timeline timeline)
        {
            id = timeline.SliceCount;
            Initialize(timeline);
        }

        internal SliceController(Timeline timeline, int id)
        {
            this.id = id;
            Initialize(timeline);
        }

        private void SetId()
        {
            controlsPanel.Controls.SetChildIndex(control, id);
            contentsPanel.Controls.SetChildIndex(content, id);
            control.Label.Text = id.ToString();
            content.Label.Text = id.ToString();
        }        

        internal int GetId()
        {
            return id;
        }

        internal void SetId(int id)
        {
            this.id = id;
            SetId();
        }
    }
}
