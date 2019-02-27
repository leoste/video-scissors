using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
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
