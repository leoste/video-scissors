﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace Scissors.Controls
{
    public partial class FancySidemenu : UserControl
    {
        private string[] tabs;
        private int selected;
        Color highlightColor;
                
        Brush brush;
        Brush backBrush;
        Brush highlightBrush;
        float tabHeight;
        int tabWidth;
        int triangleWidth;

        public string[] Tabs
        {
            get { return tabs; }
            set
            {
                if (value.Length > 0)
                {
                    tabs = value;
                    UpdateCacheAndUI();
                }
            }
        }

        public int SelectedId
        {
            get { return selected; }
        }

        public string SelectedTab
        {
            get { return tabs[selected]; }
        }

        public Color HighlightColor
        {
            get { return highlightColor; }
            set
            {
                highlightColor = value;
                highlightBrush = new SolidBrush(highlightColor);
                UpdateUI();
            }
        }

        public event EventHandler TabSelected;

        public FancySidemenu()
        {
            tabs = new string[3];
            selected = 0;
            HighlightColor = Color.Red;
            brush = new SolidBrush(ForeColor);
            backBrush = new SolidBrush(BackColor);
            
            InitializeComponent();

            UpdateCacheAndUI();
        }

        private void FancySidemenu_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void FancySidemenu_BackColorChanged(object sender, EventArgs e)
        {
            backBrush = new SolidBrush(BackColor);
        }

        private void FancySidemenu_ForeColorChanged(object sender, EventArgs e)
        {
            brush = new SolidBrush(ForeColor);
        }

        private void UpdateCacheAndUI()
        {
            tabHeight = Height / (float)tabs.Length;
            triangleWidth = (int)tabHeight;
            tabWidth = Width - triangleWidth;

            UpdateUI();
        }

        private void UpdateUI()
        {
            Refresh();
        }

        private void FancySidemenu_Resize(object sender, EventArgs e)
        {
            UpdateCacheAndUI();
        }

        private void FancySidemenu_Paint(object sender, PaintEventArgs e)
        {
            int y = (int)(selected * tabHeight);
            int bottom = (int)(y + tabHeight);
            
            Region tab;

            Point[] points = new Point[]
            {
                new Point(0, y),
                new Point(tabWidth, y),
                new Point(Width, y + (int)(tabHeight / 2)),
                new Point(tabWidth, bottom),
                new Point(0, bottom)
            };

            using (GraphicsPath gp = new GraphicsPath())
            {
                gp.AddPolygon(points);                
                tab = new Region(gp);
            }

            e.Graphics.FillRegion(highlightBrush, tab);

            Region graphicsClip = e.Graphics.Clip;
            Region region = graphicsClip.Clone();
            region.Exclude(tab);
            e.Graphics.Clip = region;

            e.Graphics.FillRectangle(backBrush, new Rectangle(0, 0, Width, Height));

            e.Graphics.Clip = graphicsClip;
        }
    }
}