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
            this.timelineHorizontalScroll = new System.Windows.Forms.Panel();
            this.horizontalScrollBar = new Scissors.Timeline.FancyScrollbar();
            this.cursorPanel.SuspendLayout();
            this.timelineHorizontalScroll.SuspendLayout();
            this.SuspendLayout();
            // 
            // optionScroll
            // 
            this.optionScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.optionScroll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.optionScroll.Location = new System.Drawing.Point(17, 43);
            this.optionScroll.Margin = new System.Windows.Forms.Padding(0);
            this.optionScroll.Name = "optionScroll";
            this.optionScroll.Size = new System.Drawing.Size(144, 463);
            this.optionScroll.TabIndex = 7;
            this.optionScroll.WrapContents = false;
            // 
            // sliceScroll
            // 
            this.sliceScroll.AutoSize = true;
            this.sliceScroll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.sliceScroll.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.sliceScroll.Location = new System.Drawing.Point(0, 43);
            this.sliceScroll.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.sliceScroll.Name = "sliceScroll";
            this.sliceScroll.Size = new System.Drawing.Size(0, 0);
            this.sliceScroll.TabIndex = 8;
            this.sliceScroll.WrapContents = false;
            // 
            // rulerScroll
            // 
            this.rulerScroll.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rulerScroll.Location = new System.Drawing.Point(0, 0);
            this.rulerScroll.Margin = new System.Windows.Forms.Padding(0);
            this.rulerScroll.Name = "rulerScroll";
            this.rulerScroll.Size = new System.Drawing.Size(744, 40);
            this.rulerScroll.TabIndex = 9;
            // 
            // cursorPanel
            // 
            this.cursorPanel.AutoSize = true;
            this.cursorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cursorPanel.Controls.Add(this.rulerScroll);
            this.cursorPanel.Controls.Add(this.sliceScroll);
            this.cursorPanel.Location = new System.Drawing.Point(0, 0);
            this.cursorPanel.Margin = new System.Windows.Forms.Padding(0);
            this.cursorPanel.Name = "cursorPanel";
            this.cursorPanel.Size = new System.Drawing.Size(744, 43);
            this.cursorPanel.TabIndex = 10;
            // 
            // timelineHorizontalScroll
            // 
            this.timelineHorizontalScroll.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timelineHorizontalScroll.Controls.Add(this.cursorPanel);
            this.timelineHorizontalScroll.Location = new System.Drawing.Point(164, 0);
            this.timelineHorizontalScroll.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.timelineHorizontalScroll.Name = "timelineHorizontalScroll";
            this.timelineHorizontalScroll.Size = new System.Drawing.Size(744, 527);
            this.timelineHorizontalScroll.TabIndex = 11;
            // 
            // horizontalScrollBar
            // 
            this.horizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalScrollBar.BackColor = System.Drawing.SystemColors.HotTrack;
            this.horizontalScrollBar.ForeColor = System.Drawing.SystemColors.Highlight;
            this.horizontalScrollBar.Location = new System.Drawing.Point(164, 527);
            this.horizontalScrollBar.Margin = new System.Windows.Forms.Padding(0);
            this.horizontalScrollBar.Maximum = 10;
            this.horizontalScrollBar.Minimum = 0;
            this.horizontalScrollBar.Name = "horizontalScrollBar";
            this.horizontalScrollBar.ScrollDirection = Scissors.Timeline.ScrollDirection.LeftToRight;
            this.horizontalScrollBar.ScrollWidth = 3;
            this.horizontalScrollBar.Size = new System.Drawing.Size(744, 24);
            this.horizontalScrollBar.TabIndex = 11;
            this.horizontalScrollBar.Value = 3;
            this.horizontalScrollBar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.horizontalScrollBar_Scroll);
            this.horizontalScrollBar.Resize += new System.EventHandler(this.horizontalScrollBar_Resize);
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.horizontalScrollBar);
            this.Controls.Add(this.timelineHorizontalScroll);
            this.Controls.Add(this.optionScroll);
            this.Name = "Timeline";
            this.Size = new System.Drawing.Size(908, 551);
            this.cursorPanel.ResumeLayout(false);
            this.cursorPanel.PerformLayout();
            this.timelineHorizontalScroll.ResumeLayout(false);
            this.timelineHorizontalScroll.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel optionScroll;
        private System.Windows.Forms.FlowLayoutPanel sliceScroll;
        private System.Windows.Forms.FlowLayoutPanel rulerScroll;
        private System.Windows.Forms.Panel cursorPanel;
        private System.Windows.Forms.Panel timelineHorizontalScroll;
        private FancyScrollbar horizontalScrollBar;
    }
}
