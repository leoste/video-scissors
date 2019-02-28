﻿namespace Scissors.Timeline
{
    partial class Timeline
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
            this.timelineContent1 = new Scissors.Timeline.TimelineContent();
            this.timelineControl1 = new Scissors.Timeline.TimelineControl();
            this.verticalScrollbar = new Scissors.Timeline.FancyScrollbar();
            this.horizontalScrollBar = new Scissors.Timeline.FancyScrollbar();
            this.SuspendLayout();
            // 
            // timelineContent1
            // 
            this.timelineContent1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timelineContent1.BackColor = System.Drawing.Color.LightSalmon;
            this.timelineContent1.HorizontalScroll = 0;
            this.timelineContent1.Location = new System.Drawing.Point(174, 0);
            this.timelineContent1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.timelineContent1.Name = "timelineContent1";
            this.timelineContent1.RulerHeight = 40;
            this.timelineContent1.SeparatorHeight = 3;
            this.timelineContent1.Size = new System.Drawing.Size(734, 524);
            this.timelineContent1.TabIndex = 17;
            this.timelineContent1.Resize += new System.EventHandler(this.timelineContent1_Resize);
            // 
            // timelineControl1
            // 
            this.timelineControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.timelineControl1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.timelineControl1.Location = new System.Drawing.Point(27, 43);
            this.timelineControl1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.timelineControl1.Name = "timelineControl1";
            this.timelineControl1.Size = new System.Drawing.Size(144, 481);
            this.timelineControl1.TabIndex = 15;
            // 
            // verticalScrollbar
            // 
            this.verticalScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalScrollbar.BackColor = System.Drawing.Color.Gray;
            this.verticalScrollbar.ForeColor = System.Drawing.Color.DarkGray;
            this.verticalScrollbar.Location = new System.Drawing.Point(0, 43);
            this.verticalScrollbar.Margin = new System.Windows.Forms.Padding(0);
            this.verticalScrollbar.Maximum = 10;
            this.verticalScrollbar.Minimum = 0;
            this.verticalScrollbar.Name = "verticalScrollbar";
            this.verticalScrollbar.ScrollDirection = Scissors.Timeline.ScrollDirection.UpToDown;
            this.verticalScrollbar.ScrollWidth = 3;
            this.verticalScrollbar.Size = new System.Drawing.Size(24, 481);
            this.verticalScrollbar.TabIndex = 12;
            this.verticalScrollbar.Value = 3;
            this.verticalScrollbar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.verticalScrollbar_Scroll);
            this.verticalScrollbar.Resize += new System.EventHandler(this.verticalScrollbar_Resize);
            // 
            // horizontalScrollBar
            // 
            this.horizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalScrollBar.BackColor = System.Drawing.Color.Gray;
            this.horizontalScrollBar.ForeColor = System.Drawing.Color.DarkGray;
            this.horizontalScrollBar.Location = new System.Drawing.Point(174, 527);
            this.horizontalScrollBar.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.horizontalScrollBar.Maximum = 10;
            this.horizontalScrollBar.Minimum = 0;
            this.horizontalScrollBar.Name = "horizontalScrollBar";
            this.horizontalScrollBar.ScrollDirection = Scissors.Timeline.ScrollDirection.LeftToRight;
            this.horizontalScrollBar.ScrollWidth = 3;
            this.horizontalScrollBar.Size = new System.Drawing.Size(734, 24);
            this.horizontalScrollBar.TabIndex = 11;
            this.horizontalScrollBar.Value = 3;
            this.horizontalScrollBar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.horizontalScrollBar_Scroll);
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.timelineContent1);
            this.Controls.Add(this.timelineControl1);
            this.Controls.Add(this.verticalScrollbar);
            this.Controls.Add(this.horizontalScrollBar);
            this.Name = "Timeline";
            this.Size = new System.Drawing.Size(908, 551);
            this.ResumeLayout(false);

        }

        #endregion
        private FancyScrollbar horizontalScrollBar;
        private FancyScrollbar verticalScrollbar;
        private TimelineControl timelineControl1;
        private TimelineContent timelineContent1;
    }
}
