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
                IPropertyEditor editor;
                object value = properties[i].GetValue(effect);

                if (properties[i].PropertyType == typeof(Point))
                {
                    Point point = (Point)value;
                    TwoNumberEditor twoEditor = new TwoNumberEditor();
                    twoEditor.Value1 = point.X;
                    twoEditor.Value2 = point.Y;
                    editor = twoEditor;
                    control = twoEditor;
                }
                else if (properties[i].PropertyType == typeof(Size))
                {
                    Size size = (Size)value;
                    TwoNumberEditor twoEditor = new TwoNumberEditor();
                    twoEditor.Value1 = size.Width;
                    twoEditor.Value2 = size.Height;
                    editor = twoEditor;
                    control = twoEditor;
                }
                else if (properties[i].PropertyType == typeof(string))
                {
                    string text = (string)value;
                    StringEditor stringEditor = new StringEditor();
                    stringEditor.Value = text;
                    editor = stringEditor;
                    control = stringEditor;
                }
                else
                {
                    editor = null;
                    control = new Label() { Width = 150, Height = 50, BackColor = Color.BlanchedAlmond };
                }

                if (editor != null)
                {
                    editor.Label = properties[i].Name;
                }

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
