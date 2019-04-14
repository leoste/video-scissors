using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Windows.Forms;
using Scissors.EffectAPI;
using Scissors.Controls;

namespace Scissors.Timeline
{
    public partial class ItemEditor : Form
    {
        IEffect effect;        
        PropertyInfo[] properties;        

        public ItemEditor(IEffect effectInstance)
        {            
            InitializeComponent();
            effect = effectInstance;

            Type type = effect.GetType();

            EffectInfo info = (EffectInfo)type.GetCustomAttribute(typeof(EffectInfo), false);
            nameLabel.Text = info.Name;

            properties = type.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                Control control;
                object value = properties[i].GetValue(effect);

                if (properties[i].PropertyType == typeof(Point))
                {
                    Point point = (Point)value;
                    TwoNumberEditor editor = new TwoNumberEditor();
                    editor.Value1 = point.X;
                    editor.Value2 = point.Y;
                    editor.Label1 = "X";
                    editor.Label2 = "Y";
                    control = editor;
                }
                else if (properties[i].PropertyType == typeof(Size))
                {
                    Size size = (Size)value;
                    TwoNumberEditor editor = new TwoNumberEditor();
                    editor.Value1 = size.Width;
                    editor.Value2 = size.Height;
                    editor.Label1 = "Width";
                    editor.Label2 = "Height";
                    control = editor;
                }
                else if (properties[i].PropertyType == typeof(string))
                {
                    string text = (string)value;
                    StringEditor editor = new StringEditor();
                    editor.Value = text;
                    editor.Label = "String";
                    control = editor;
                }
                else control = new Label() { Width = 150, Height = 50, BackColor = Color.BlanchedAlmond };

                flowLayoutPanel1.Controls.Add(control);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < properties.Length; i++)
            {
                Control control = flowLayoutPanel1.Controls[i];

                if (properties[i].PropertyType == typeof(Point))
                {
                    TwoNumberEditor editor = control as TwoNumberEditor;
                    properties[i].SetValue(effect, new Point(editor.Value1, editor.Value2));
                }
                else if (properties[i].PropertyType == typeof(Size))
                {
                    TwoNumberEditor editor = control as TwoNumberEditor;
                    properties[i].SetValue(effect, new Size(editor.Value1, editor.Value2));
                }
                else if (properties[i].PropertyType == typeof(string))
                {
                    StringEditor editor = control as StringEditor;
                    properties[i].SetValue(effect, editor.Value);
                }
            }

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
