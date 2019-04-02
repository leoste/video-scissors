﻿namespace Scissors.Timeline
{
    partial class Menu
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fancySidemenu1 = new Scissors.Controls.FancySidemenu();
            this.verticalScrollbar = new Scissors.Controls.FancyScrollbar();
            this.SuspendLayout();
            // 
            // fancySidemenu1
            // 
            this.fancySidemenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fancySidemenu1.BackColor = System.Drawing.Color.Gray;
            this.fancySidemenu1.ForeColor = System.Drawing.Color.Black;
            this.fancySidemenu1.HighlightColor = System.Drawing.Color.DarkGray;
            this.fancySidemenu1.Location = new System.Drawing.Point(0, 0);
            this.fancySidemenu1.Margin = new System.Windows.Forms.Padding(0);
            this.fancySidemenu1.Name = "fancySidemenu1";
            this.fancySidemenu1.Size = new System.Drawing.Size(100, 200);
            this.fancySidemenu1.TabIndex = 14;
            this.fancySidemenu1.Tabs = new string[] {
        "b",
        "c",
        "d",
        "a"};
            // 
            // verticalScrollbar
            // 
            this.verticalScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalScrollbar.BackColor = System.Drawing.Color.Gray;
            this.verticalScrollbar.ForeColor = System.Drawing.Color.DarkGray;
            this.verticalScrollbar.Location = new System.Drawing.Point(103, 0);
            this.verticalScrollbar.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.verticalScrollbar.Maximum = 10;
            this.verticalScrollbar.Minimum = 0;
            this.verticalScrollbar.Name = "verticalScrollbar";
            this.verticalScrollbar.ScrollDirection = Scissors.Controls.ScrollDirection.UpToDown;
            this.verticalScrollbar.ScrollWidth = 3;
            this.verticalScrollbar.Size = new System.Drawing.Size(24, 200);
            this.verticalScrollbar.TabIndex = 13;
            this.verticalScrollbar.Value = 3;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fancySidemenu1);
            this.Controls.Add(this.verticalScrollbar);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(400, 200);
            this.ResumeLayout(false);

        }

        #endregion

        private Scissors.Controls.FancyScrollbar verticalScrollbar;
        private Controls.FancySidemenu fancySidemenu1;
    }
}