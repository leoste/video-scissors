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
        
        internal int LayerCount { get { return layers.Count; } }
        internal FlowLayoutPanel ControlsPanel { get { return control.Panel; } }
        internal FlowLayoutPanel ContentsPanel { get { return content.Panel; } }

        private void Initialize(Timeline timeline)
        {
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
            content.BackColor = color;
            contentsPanel.Controls.Add(content);

            SetId();

            layers = new List<LayerController>();
            CreateLayer();
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
        }

        private void UpdateHeight()
        {
            int height = 46 * LayerCount + (LayerCount - 1) * 3 + 6;
            control.Height = height;
            content.Height = height;
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

        internal int GetLayerId(LayerController layer)
        {
            return layers.IndexOf(layer);
        }

        internal void CreateLayer()
        {
            CreateLayer(LayerCount);
        }

        internal void CreateLayer(int id)
        {
            layers.Insert(id, new LayerController(this, id));
            for (int i = LayerCount - 1; i > id; i -= 1)
            {
                layers[i].SetId(i);
            }

            UpdateHeight();
        }

        internal void RemoveLayer(int id)
        {
            LayerController layer = layers[id];
            layers.Remove(layer);
            layer.Dispose();
            for (int i = id; i < LayerCount; i += 1)
            {
                layers[i].SetId(i);
            }

            if (LayerCount == 0)
            {
                layers.Add(new LayerController(this));
            }

            UpdateHeight();
        }

        internal void SwapLayers(int id1, int id2)
        {
            layers[id1].SetId(id2);
            layers[id2].SetId(id1);

            LayerController layer1 = layers[id1];
            layers[id1] = layers[id2];
            layers[id2] = layer1;
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
    }
}
