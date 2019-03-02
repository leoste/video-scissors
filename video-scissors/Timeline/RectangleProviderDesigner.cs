using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;

namespace Scissors.Timeline
{
    public partial class RectangleProvider : UserControl
    {
        private class RectangleProviderDesigner : ControlDesigner
        {
            protected override void OnPaintAdornments(PaintEventArgs pe)
            {                
                if (!(Control is RectangleProvider control)) return;
                
                pe.Graphics.FillRectangle(new SolidBrush(control.BackColor), control.ContainerRectangle);

                Color textColor = SelectHighestContrast(control.BackColor, Color.DimGray, Color.LightGray);

                Brush textBrush = new SolidBrush(textColor);
                Point offset = new Point(10, 10);

                Pen textPen = new Pen(textBrush, 2);                
                textPen.DashStyle = DashStyle.Dash;
                textPen.Alignment = PenAlignment.Inset;

                pe.Graphics.DrawString("RulerContainer", SystemFonts.DefaultFont, textBrush, 
                    control.RulerContainerRectangle.X + offset.X, control.RulerContainerRectangle.Y + offset.Y);
                pe.Graphics.DrawString("ContentContainer", SystemFonts.DefaultFont, textBrush, 
                    control.ContentContainerRectangle.X + offset.X, control.ContentContainerRectangle.Y + offset.Y);
                pe.Graphics.DrawString("ControlContainer", SystemFonts.DefaultFont, textBrush,
                    control.ControlContainerRectangle.X + offset.X, control.ControlContainerRectangle.Y + offset.Y);

                pe.Graphics.DrawRectangle(textPen, control.RulerContainerRectangle);
                pe.Graphics.DrawRectangle(textPen, control.ContentContainerRectangle);
                pe.Graphics.DrawRectangle(textPen, control.ControlContainerRectangle);
            }            

            public override IList SnapLines
            {
                get
                {
                    IList snapLines = base.SnapLines;

                    if (!(Control is RectangleProvider control)) { return snapLines; }
                    
                    SnapLinePriority priority = SnapLinePriority.Always;
                    
                    snapLines.Add(new SnapLine(SnapLineType.Bottom, control.rulerHeight, priority));
                    snapLines.Add(new SnapLine(SnapLineType.Top, control.contentBegin, priority));
                    snapLines.Add(new SnapLine(SnapLineType.Right, control.controlWidth, priority));
                    snapLines.Add(new SnapLine(SnapLineType.Left, control.rulerBegin, priority));

                    return snapLines;
                }
            }

            public Color SelectHighestContrast(Color compared, Color color1, Color color2)
            {
                float L1 = compared.GetBrightness();
                float L2 = color1.GetBrightness();
                float L3 = color2.GetBrightness();

                return (L1 + 0.05) / (L2 + 0.05) > (L3 + 0.05) / (L1 + 0.05) ? color1 : color2;
            }
        }
    }
}
