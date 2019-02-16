using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class CursorController : IController
    {        
        private static readonly Brush brush = new SolidBrush(Color.Crimson);
        private static readonly int width = 2;
        
        private int oldLeft;
        private bool lockToControl;
        private TimelineController timeline;
        private Dictionary<Control, ControlInfo> generations;
        private Panel panel;
        
        public CursorController(TimelineController timeline)
        {
            generations = new Dictionary<Control, ControlInfo>();
            oldLeft = 0;

            this.timeline = timeline;            
            panel = timeline.CursorPanel;            

            AddEventsDeep(panel);            
        }
        
        private void AddEventsDeep(Control control)
        {
            int level;
            if (generations.TryGetValue(control.Parent, out ControlInfo info)) level = info.Level;
            else level = -1;
            
            generations.Add(control, new ControlInfo(level + 1, Graphics.FromHwnd(control.Handle)));

            control.ControlAdded += Control_ControlAdded;
            control.ControlRemoved += Control_ControlRemoved;
            control.MouseMove += Control_MouseMove;
            control.Resize += Control_Resize;

            if (control is ItemContent)
            {
                ItemContent item = control as ItemContent;
                item.MouseDown += Item_MouseDown;
                item.MouseUp += Item_MouseUp;
            }

            foreach (Control child in control.Controls)
            {
                AddEventsDeep(child);
            }
        }

        private void Item_MouseDown(object sender, MouseEventArgs e)
        {
            lockToControl = true;
        }

        private void Item_MouseUp(object sender, MouseEventArgs e)
        {
            lockToControl = false;
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            int cx;
            if (lockToControl) cx = 0;
            else cx = e.X;

            int left = GetCursorAbsoluteX((Control)sender, cx);
            int diff = left - oldLeft;
            int diffw;
            int abs = Math.Abs(diff);

            if (abs < width)
            {
                diffw = abs;
                if (diff < 0) diff = -width;
            }
            else diffw = width;

            foreach (KeyValuePair<Control, ControlInfo> pair in generations)
            {
                int x = GetCursorRelativeX(pair.Key, left);
                if (abs > 0) pair.Key.Invalidate(new Rectangle(x - diff, 0, diffw, pair.Key.Height));
                pair.Key.Update();
                pair.Value.Graphics.FillRectangle(brush, x, 0, width, pair.Key.Height);
            }

            oldLeft = left;
        }

        private void Control_Resize(object sender, EventArgs e)
        {
            Control control = sender as Control;
            if (generations.TryGetValue(control, out ControlInfo info))
            {                
                generations[control] = new ControlInfo(info.Level, Graphics.FromHwnd(control.Handle));
                info.Dispose();
            }
        }

        private void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            AddEventsDeep(e.Control);
        }

        private void RemoveEventsDeep(Control control)
        {
            generations.Remove(control);
            control.ControlAdded -= Control_ControlAdded;
            control.ControlRemoved -= Control_ControlRemoved;
            control.MouseMove -= Control_MouseMove;
            control.Resize -= Control_Resize;

            if (control is ItemContent)
            {
                ItemContent item = control as ItemContent;
                item.MouseDown -= Item_MouseDown;
                item.MouseUp -= Item_MouseUp;
            }

            foreach (Control child in control.Controls)
            {
                RemoveEventsDeep(child);
            }
        }

        private void Control_ControlRemoved(object sender, ControlEventArgs e)
        {
            RemoveEventsDeep(e.Control);
        }

        private int GetCursorAbsoluteX(Control control, int x)
        {
            if (generations.TryGetValue(control, out ControlInfo info))
            {

                if (info.Level > 0) x += GetCursorAbsoluteX(control.Parent, control.Left);

                return x;
            }
            else throw new ArgumentException();
        }

        private int GetCursorRelativeX(Control control, int x)
        {
            return x - GetCursorAbsoluteX(control, 0);
        }

        public int TimelineLength { get { return timeline.TimelineLength; } }

        public float TimelineZoom { get { return timeline.TimelineZoom; } }

        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }

        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }

        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        public void Dispose()
        {
            foreach (KeyValuePair<Control, ControlInfo> pair in generations)
            {
                pair.Value.Dispose();
            }
        }

        public void UpdateUI()
        {
            
        }

        class ControlInfo : IDisposable
        {
            public int Level { get; private set; }
            public Graphics Graphics { get; private set; }

            public ControlInfo(int level, Graphics graphics)
            {
                Level = level;
                Graphics = graphics;
            }

            public void Dispose()
            {
                Graphics.Dispose();
            }
        }
    }
}
