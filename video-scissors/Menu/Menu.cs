using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.Timeline
{
    public partial class Menu : UserControl
    {
        private List<Item> allItems;
        private List<MediaItem> mediaItems;
        private List<EffectItem> effectItems;

        public Menu()
        {
            InitializeComponent();
            allItems = new List<Item>();
            mediaItems = new List<MediaItem>();
            effectItems = new List<EffectItem>();
        }

        private void fancySidemenu1_TabClicked(object sender, SelectionEventArgs e)
        {
            if (e.SelectionChanged)
            {
                foreach (MediaItem item in mediaItems)
                {
                    item.label.Visible = e.SelectedId == 0;
                }

                foreach (EffectItem item in effectItems)
                {
                    item.label.Visible = e.SelectedId == 1;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (fancySidemenu1.SelectedId == 0)
            {
                if (openMediaDialog.ShowDialog() == DialogResult.OK)
                {
                    for (int i = 0; i < openMediaDialog.FileNames.Length; i += 1)
                    {
                        MediaItem item = new MediaItem();
                        SetupItem(item, openMediaDialog.FileNames[i], openMediaDialog.SafeFileNames[i]);
                        
                        mediaItems.Add(item);
                    }
                    flowLayoutPanel1.Controls.SetChildIndex(label1, flowLayoutPanel1.Controls.Count);
                }
            }
        }

        private void SetupItem(Item item, string filename, string name)
        {
            Label label = new Label();

            label.Size = label1.Size;
            label.BackColor = label1.BackColor;
            label.ForeColor = label1.ForeColor;
            label.Margin = label1.Margin;

            label.MouseDown += Label_MouseDown;
            label.MouseMove += Label_MouseMove;
            label.MouseUp += Label_MouseUp;

            flowLayoutPanel1.Controls.Add(label);

            label.Text = name;
            item.label = label;
            item.filename = filename;

            allItems.Add(item);
        }

        private void Label_MouseDown(object sender, MouseEventArgs e)
        {
            GlobalMouseInfo.Holder = MouseHolder.Menu;
            GlobalMouseInfo.LastKnownHolder = MouseHolder.Menu;
            GlobalMouseInfo.State = MouseState.Pressed;

            Item item = FindItemByLabel(sender as Label);
            
            if (item is MediaItem media)
            {
                GlobalMouseInfo.MenuItemEffect = new EffectAPI.Effect(null, typeof(Effects.ImageRenderer), null);
            }
        }

        private void Label_MouseMove(object sender, MouseEventArgs e)
        {
            
        }

        private void Label_MouseUp(object sender, MouseEventArgs e)
        {
            GlobalMouseInfo.State = MouseState.Unpressed;
            GlobalMouseInfo.Holder = MouseHolder.None;
        }

        private Item FindItemByLabel(Label label)
        {
            return allItems.Find(x => x.label == label);
        }

        private class Item
        {
            public Label label;
            public string filename;
        }

        private class MediaItem : Item
        {
            
        }

        private class EffectItem : Item
        {
            public int duwu;
        }
    }
}
