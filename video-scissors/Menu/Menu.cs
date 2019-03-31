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
        private List<MediaItem> mediaItems;
        private List<EffectItem> effectItems;

        public Menu()
        {
            InitializeComponent();
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
                        Label label = new Label();
                        label.Size = label1.Size;
                        label.BackColor = label1.BackColor;
                        label.ForeColor = label1.ForeColor;
                        label.Margin = label1.Margin;
                        
                        label.Text = openMediaDialog.SafeFileNames[i];

                        flowLayoutPanel1.Controls.Add(label);
                        flowLayoutPanel1.Controls.SetChildIndex(label1, flowLayoutPanel1.Controls.Count);

                        mediaItems.Add(new MediaItem()
                        {
                            label = label,
                            filename = openMediaDialog.FileNames[i]
                        });
                    }
                }
            }
        }

        private struct MediaItem
        {
            public Label label;
            public string filename;
        }

        private struct EffectItem
        {
            public Label label;
            public string filename;
            public int duwu;
        }
    }
}
