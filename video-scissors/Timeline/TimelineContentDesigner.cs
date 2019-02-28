using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace Scissors.Timeline
{
    public partial class TimelineContent : UserControl
    {
        private class TimelineContentDesigner : ControlDesigner
        {
            protected override void OnPaintAdornments(PaintEventArgs pe)
            {
                if (!(Control is TimelineContent control)) return;

                Brush rulerBrush = new SolidBrush(Color.FromArgb(128, Color.CornflowerBlue));
                pe.Graphics.FillRectangle(rulerBrush, control.RulerContainerRectangle);

                Brush slicesBrush = new SolidBrush(Color.FromArgb(128, Color.LightGoldenrodYellow));
                pe.Graphics.FillRectangle(slicesBrush, control.SlicesContainerRectangle);
            }            

            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    if (!(Control is TimelineContent control)) { return snapLines; }
                    
                    SnapLinePriority priority = SnapLinePriority.Always;
                    
                    snapLines.Add(new SnapLine(SnapLineType.Bottom, control.rulerHeight, priority));
                    snapLines.Add(new SnapLine(SnapLineType.Top, control.slicesBegin, priority));

                    return snapLines;
                }
            }
        }
    }
}
