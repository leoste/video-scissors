namespace Scissors.Timeline
{
    partial class RectangleProvider
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
            // RectangleProvider
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "RectangleProvider";
            this.Size = new System.Drawing.Size(548, 358);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.RectangleProvider_MouseDown);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.RectangleProvider_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
