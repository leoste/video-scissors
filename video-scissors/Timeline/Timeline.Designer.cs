namespace Scissors.Timeline
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
            this.optionScroll = new System.Windows.Forms.FlowLayoutPanel();
            this.sliceScroll = new System.Windows.Forms.FlowLayoutPanel();
            this.rulerScroll = new System.Windows.Forms.FlowLayoutPanel();
            this.cursorPanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.timelineHorizontalScroll = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.verticalScrollbar = new Scissors.Timeline.FancyScrollbar();
            this.horizontalScrollBar = new Scissors.Timeline.FancyScrollbar();
            this.cursorPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.timelineHorizontalScroll.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionScroll
            // 
            this.optionScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.optionScroll.AutoSize = true;
            this.optionScroll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.optionScroll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionScroll.Location = new System.Drawing.Point(0, 0);
            this.optionScroll.Margin = new System.Windows.Forms.Padding(0);
            this.optionScroll.Name = "optionScroll";
            this.optionScroll.Size = new System.Drawing.Size(0, 0);
            this.optionScroll.TabIndex = 7;
            this.optionScroll.WrapContents = false;
            this.optionScroll.Resize += new System.EventHandler(this.optionScroll_Resize);
            // 
            // sliceScroll
            // 
            this.sliceScroll.AutoSize = true;
            this.sliceScroll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sliceScroll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sliceScroll.Location = new System.Drawing.Point(0, 0);
            this.sliceScroll.Margin = new System.Windows.Forms.Padding(0);
            this.sliceScroll.Name = "sliceScroll";
            this.sliceScroll.Size = new System.Drawing.Size(0, 0);
            this.sliceScroll.TabIndex = 8;
            this.sliceScroll.WrapContents = false;
            this.sliceScroll.Resize += new System.EventHandler(this.sliceScroll_Resize);
            // 
            // rulerScroll
            // 
            this.rulerScroll.AutoSize = true;
            this.rulerScroll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.rulerScroll.Location = new System.Drawing.Point(0, 0);
            this.rulerScroll.Margin = new System.Windows.Forms.Padding(0);
            this.rulerScroll.Name = "rulerScroll";
            this.rulerScroll.Size = new System.Drawing.Size(0, 0);
            this.rulerScroll.TabIndex = 9;
            // 
            // cursorPanel
            // 
            this.cursorPanel.AutoSize = true;
            this.cursorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cursorPanel.Controls.Add(this.panel2);
            this.cursorPanel.Controls.Add(this.rulerScroll);
            this.cursorPanel.Location = new System.Drawing.Point(0, 0);
            this.cursorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.cursorPanel.Name = "cursorPanel";
            this.cursorPanel.Size = new System.Drawing.Size(525, 436);
            this.cursorPanel.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.sliceScroll);
            this.panel2.Location = new System.Drawing.Point(0, 43);
            this.panel2.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(525, 393);
            this.panel2.TabIndex = 10;
            // 
            // timelineHorizontalScroll
            // 
            this.timelineHorizontalScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timelineHorizontalScroll.Controls.Add(this.cursorPanel);
            this.timelineHorizontalScroll.Location = new System.Drawing.Point(174, 0);
            this.timelineHorizontalScroll.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.timelineHorizontalScroll.Name = "timelineHorizontalScroll";
            this.timelineHorizontalScroll.Size = new System.Drawing.Size(734, 524);
            this.timelineHorizontalScroll.TabIndex = 11;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.optionScroll);
            this.panel1.Location = new System.Drawing.Point(27, 43);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(144, 481);
            this.panel1.TabIndex = 13;
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // verticalScrollbar
            // 
            this.verticalScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.verticalScrollbar.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.verticalScrollbar.ForeColor = System.Drawing.Color.SlateBlue;
            this.verticalScrollbar.Location = new System.Drawing.Point(0, 42);
            this.verticalScrollbar.Margin = new System.Windows.Forms.Padding(0);
            this.verticalScrollbar.Maximum = 10;
            this.verticalScrollbar.Minimum = 0;
            this.verticalScrollbar.Name = "verticalScrollbar";
            this.verticalScrollbar.ScrollDirection = Scissors.Timeline.ScrollDirection.UpToDown;
            this.verticalScrollbar.ScrollWidth = 3;
            this.verticalScrollbar.Size = new System.Drawing.Size(24, 482);
            this.verticalScrollbar.TabIndex = 12;
            this.verticalScrollbar.Value = 3;
            this.verticalScrollbar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.verticalScrollbar_Scroll);
            this.verticalScrollbar.Resize += new System.EventHandler(this.verticalScrollbar_Resize);
            // 
            // horizontalScrollBar
            // 
            this.horizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalScrollBar.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.horizontalScrollBar.ForeColor = System.Drawing.Color.SlateBlue;
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
            this.horizontalScrollBar.Resize += new System.EventHandler(this.horizontalScrollBar_Resize);
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.verticalScrollbar);
            this.Controls.Add(this.horizontalScrollBar);
            this.Controls.Add(this.timelineHorizontalScroll);
            this.Name = "Timeline";
            this.Size = new System.Drawing.Size(908, 551);
            this.cursorPanel.ResumeLayout(false);
            this.cursorPanel.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.timelineHorizontalScroll.ResumeLayout(false);
            this.timelineHorizontalScroll.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel optionScroll;
        private System.Windows.Forms.FlowLayoutPanel sliceScroll;
        private System.Windows.Forms.FlowLayoutPanel rulerScroll;
        private System.Windows.Forms.Panel cursorPanel;
        private System.Windows.Forms.Panel timelineHorizontalScroll;
        private FancyScrollbar horizontalScrollBar;
        private FancyScrollbar verticalScrollbar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}
