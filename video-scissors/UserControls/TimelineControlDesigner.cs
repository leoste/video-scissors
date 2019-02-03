using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Scissors.UserControls
{
    public class TimelineControlDesigner : ParentControlDesigner
    {
        protected override Control GetParentForComponent(IComponent component)
        {
            Timeline timeline = Control as Timeline;
            return timeline.FlowLayoutPanel;
        }
    }
}
