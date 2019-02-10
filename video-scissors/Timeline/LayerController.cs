﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class LayerController : IFrameController
    {
        private int id;
        private SliceController slice;
        private FlowLayoutPanel controlsPanel;
        private FlowLayoutPanel contentsPanel;

        private LayerControl control;
        private LayerContent content;

        private List<ItemController> items;
        
        private int oldLength;
        private float oldZoom;

        internal Panel ItemContentsPanel { get { return content.Panel; } }

        public int TimelineLength { get { return slice.TimelineLength; } }
        public float TimelineZoom { get { return slice.TimelineZoom; } }
        public int ProjectFramerate { get { return slice.ProjectFramerate; } }
        public int ProjectFrameWidth { get { return slice.ProjectFrameWidth; } }
        public int ProjectFrameHeight { get { return slice.ProjectFrameHeight; } }

        private void Initialize(SliceController slice)
        {
            oldLength = -1;
            oldZoom = -1;

            this.slice = slice;
            controlsPanel = slice.LayerControlsPanel;
            contentsPanel = slice.LayerContentsPanel;

            Color color = ColorProvider.GetRandomLayerColor();

            control = new LayerControl();
            control.BackColor = color;
            controlsPanel.Controls.Add(control);
            control.AddClicked += Control_AddClicked;
            control.RemoveClicked += Control_RemoveClicked;
            control.MoveUpClicked += Control_MoveUpClicked;
            control.MoveDownClicked += Control_MoveDownClicked;

            content = new LayerContent();
            content.BackColor = color;
            contentsPanel.Controls.Add(content);

            SetId();

            items = new List<ItemController>();

            items.Add(new ItemController(this, 2, 10));
            items.Add(new ItemController(this, 15, 10));
            items.Add(new ItemController(this, 40, 5));

            UpdateUI();
        }

        private void Control_AddClicked(object sender, EventArgs e)
        {
            slice.CreateLayer(id + 1);
        }

        private void Control_RemoveClicked(object sender, EventArgs e)
        {
            slice.RemoveLayer(id);
        }

        private void Control_MoveUpClicked(object sender, EventArgs e)
        {
            if (id > 0) slice.SwapLayers(id, id - 1);
        }

        private void Control_MoveDownClicked(object sender, EventArgs e)
        {
            if (id < slice.LayerCount - 1) slice.SwapLayers(id, id + 1);
        }

        internal LayerController(SliceController slice)
        {
            id = slice.LayerCount;
            Initialize(slice);
        }

        internal LayerController(SliceController slice, int id)
        {
            this.id = id;
            Initialize(slice);
        }

        private void SetId()
        {
            controlsPanel.Controls.SetChildIndex(control, id);
            contentsPanel.Controls.SetChildIndex(content, id);
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

        public void UpdateUI()
        {
            if (TimelineLength != oldLength || TimelineZoom != oldZoom)
            {
                oldLength = TimelineLength;
                oldZoom = TimelineZoom;

                content.Width = (int)(TimelineLength * TimelineZoom);

                foreach (ItemController item in items)
                {
                    item.UpdateUI();
                }
            }            
        }

        public Frame ProcessFrame(Frame frame, int position)
        {
            Frame processed;

            if (frame == null)
            {
                processed = new Frame(new Bitmap(ProjectFrameWidth, ProjectFrameHeight), false);
            }
            else
            {
                processed = new Frame(frame);
            }

            //find current Item and send Frame through it get something processed and return it

            return processed;
        }

        public void Dispose()
        {
            foreach (ItemController item in items)
            {
                item.Dispose();
            }

            controlsPanel.Controls.Remove(control);
            contentsPanel.Controls.Remove(content);
            control.Dispose();
            content.Dispose();
        }
    }
}