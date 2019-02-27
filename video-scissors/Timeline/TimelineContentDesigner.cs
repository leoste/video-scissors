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

                    SnapLineType type1 = SnapLineType.Top;
                    SnapLineType type2 = SnapLineType.Bottom;
                    int offset1 = control.rulerHeight;
                    int offset2 = control.slicesBegin;
                    string filter = null;
                    SnapLinePriority priority = SnapLinePriority.Always;
                    
                    snapLines.Add(new SnapLine(type1, offset1, filter, priority));
                    snapLines.Add(new SnapLine(type1, offset2, filter, priority));
                    snapLines.Add(new SnapLine(type2, offset1, filter, priority));
                    snapLines.Add(new SnapLine(type2, offset2, filter, priority));

                    return snapLines;
                }
            }
        }
    }
}
