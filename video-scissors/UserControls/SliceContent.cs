﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scissors.UserControls
{
    public partial class SliceContent : UserControl
    {
        internal FlowLayoutPanel Panel { get { return flowLayoutPanel1; } }

        public SliceContent()
        {
            InitializeComponent();
        }
    }
}