using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Scissors.EffectAPI;

namespace Scissors.Timeline
{
    public partial class ItemEditor : Form
    {
        public ItemEditor(IEffect effectInstance)
        {
            InitializeComponent();
        }
    }
}
