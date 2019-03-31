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
            this.rectangleProvider1 = new Scissors.Timeline.RectangleProvider();
            this.verticalScrollbar = new Scissors.Controls.FancyScrollbar();
            this.horizontalScrollBar = new Scissors.Controls.FancyScrollbar();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rectangleProvider1
            // 
            this.rectangleProvider1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rectangleProvider1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.rectangleProvider1.ControlWidth = 132;
            this.rectangleProvider1.HorizontalScroll = 0;
            this.rectangleProvider1.Location = new System.Drawing.Point(27, 0);
            this.rectangleProvider1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.rectangleProvider1.Name = "rectangleProvider1";
            this.rectangleProvider1.RulerHeight = 40;
            this.rectangleProvider1.SeparatorHeight = 3;
            this.rectangleProvider1.SeparatorWidth = 3;
            this.rectangleProvider1.Size = new System.Drawing.Size(877, 524);
            this.rectangleProvider1.TabIndex = 17;
            this.rectangleProvider1.VerticalScroll = 0;
            this.rectangleProvider1.Resize += new System.EventHandler(this.rectangleProvider1_Resize);
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
            this.verticalScrollbar.ScrollDirection = Scissors.Controls.ScrollDirection.UpToDown;
            this.verticalScrollbar.ScrollWidth = 3;
            this.verticalScrollbar.Size = new System.Drawing.Size(24, 481);
            this.verticalScrollbar.TabIndex = 12;
            this.verticalScrollbar.Value = 3;
            this.verticalScrollbar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.verticalScrollbar_Scroll);
            // 
            // horizontalScrollBar
            // 
            this.horizontalScrollBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.horizontalScrollBar.BackColor = System.Drawing.Color.Gray;
            this.horizontalScrollBar.ForeColor = System.Drawing.Color.DarkGray;
            this.horizontalScrollBar.Location = new System.Drawing.Point(162, 527);
            this.horizontalScrollBar.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.horizontalScrollBar.Maximum = 10;
            this.horizontalScrollBar.Minimum = 0;
            this.horizontalScrollBar.Name = "horizontalScrollBar";
            this.horizontalScrollBar.ScrollDirection = Scissors.Controls.ScrollDirection.LeftToRight;
            this.horizontalScrollBar.ScrollWidth = 3;
            this.horizontalScrollBar.Size = new System.Drawing.Size(742, 24);
            this.horizontalScrollBar.TabIndex = 11;
            this.horizontalScrollBar.Value = 3;
            this.horizontalScrollBar.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.horizontalScrollBar_Scroll);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(41, 41);
            this.button1.TabIndex = 18;
            this.button1.Text = "Z-";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(68, 0);
            this.button2.Margin = new System.Windows.Forms.Padding(0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(41, 41);
            this.button2.TabIndex = 19;
            this.button2.Text = "Z+";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Timeline
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.rectangleProvider1);
            this.Controls.Add(this.verticalScrollbar);
            this.Controls.Add(this.horizontalScrollBar);
            this.Name = "Timeline";
            this.Size = new System.Drawing.Size(904, 551);
            this.ResumeLayout(false);

        }

        #endregion
        private Scissors.Controls.FancyScrollbar horizontalScrollBar;
        private Scissors.Controls.FancyScrollbar verticalScrollbar;
        private RectangleProvider rectangleProvider1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}
