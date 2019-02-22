namespace Scissors
{
    partial class Form1
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timeline1 = new Scissors.Timeline.Timeline();
            this.fancyScrollbar1 = new Scissors.Timeline.FancyScrollbar();
            this.SuspendLayout();
            // 
            // timeline1
            // 
            this.timeline1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.timeline1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.timeline1.Location = new System.Drawing.Point(12, 12);
            this.timeline1.Name = "timeline1";
            this.timeline1.Size = new System.Drawing.Size(727, 477);
            this.timeline1.TabIndex = 0;
            // 
            // fancyScrollbar1
            // 
            this.fancyScrollbar1.BackColor = System.Drawing.Color.Coral;
            this.fancyScrollbar1.ForeColor = System.Drawing.Color.Cornsilk;
            this.fancyScrollbar1.Location = new System.Drawing.Point(745, 12);
            this.fancyScrollbar1.Maximum = 100;
            this.fancyScrollbar1.Minimum = 0;
            this.fancyScrollbar1.Name = "fancyScrollbar1";
            this.fancyScrollbar1.ScrollDirection = Scissors.Timeline.ScrollDirection.UpToDown;
            this.fancyScrollbar1.ScrollWidth = 30;
            this.fancyScrollbar1.Size = new System.Drawing.Size(316, 477);
            this.fancyScrollbar1.TabIndex = 1;
            this.fancyScrollbar1.Value = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 501);
            this.Controls.Add(this.fancyScrollbar1);
            this.Controls.Add(this.timeline1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Timeline.Timeline timeline1;
        private Timeline.FancyScrollbar fancyScrollbar1;
    }
}

