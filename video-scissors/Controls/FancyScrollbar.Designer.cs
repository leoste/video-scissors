namespace Scissors.Controls
{
    partial class FancyScrollbar
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
            this.SuspendLayout();
            // 
            // FancyScrollbar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FancyScrollbar";
            this.BackColorChanged += new System.EventHandler(this.FancyScrollbar_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.FancyScrollbar_ForeColorChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FancyScrollbar_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FancyScrollbar_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FancyScrollbar_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.FancyScrollbar_MouseUp);
            this.Resize += new System.EventHandler(this.FancyScrollbar_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
