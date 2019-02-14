using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    class CursorController : IController
    {
        private CursorContent content;
        private TimelineController timeline;

        private Dictionary<Control, int> generations;

        public CursorController(TimelineController timeline)
        {
            generations = new Dictionary<Control, int>();

            this.timeline = timeline;
            
            Panel panel = timeline.CursorPanel;
            AddEventsDeep(panel);
            
            content = new CursorContent();
            content.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;            
            content.Height = panel.Height;
            content.Visible = false;
            panel.Controls.Add(content);
            content.BringToFront();
        }
        
        private void AddEventsDeep(Control control)
        {
            if (!generations.TryGetValue(control.Parent, out int level)) level = -1;
            generations.Add(control, level + 1);            

            control.ControlAdded += Control_ControlAdded;
            control.ControlRemoved += Control_ControlRemoved;
            control.MouseEnter += Panel_MouseEnter;
            control.MouseLeave += Panel_MouseLeave;
            control.MouseMove += Panel_MouseMove;

            foreach (Control child in control.Controls)
            {
                AddEventsDeep(child);
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
            control.MouseEnter -= Panel_MouseEnter;
            control.MouseLeave -= Panel_MouseLeave;
            control.MouseMove -= Panel_MouseMove;

            foreach (Control child in control.Controls)
            {
                RemoveEventsDeep(child);
            }
        }

        private void Control_ControlRemoved(object sender, ControlEventArgs e)
        {
            RemoveEventsDeep(e.Control);
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            content.Visible = true;
        }

        private void Panel_MouseMove(object sender, MouseEventArgs e)
        {
            content.Left = GetCursorAbsoluteX((Control)sender, e.X);
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            content.Visible = false;
        }

        private int GetCursorAbsoluteX(Control control, int x)
        {
            generations.TryGetValue(control, out int level);

            if (level > 0) x += GetCursorAbsoluteX(control.Parent, control.Left);

            return x;
        }

        public int TimelineLength { get { return timeline.TimelineLength; } }

        public float TimelineZoom { get { return timeline.TimelineZoom; } }

        public int ProjectFramerate { get { return timeline.ProjectFramerate; } }

        public int ProjectFrameWidth { get { return timeline.ProjectFrameWidth; } }

        public int ProjectFrameHeight { get { return timeline.ProjectFrameHeight; } }

        public void Dispose()
        {
            timeline.CursorPanel.Controls.Remove(content);
            content.Dispose();
        }

        public void UpdateUI()
        {
            
        }
    }
}
