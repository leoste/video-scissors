namespace Scissors.Controls
{
    partial class FancySidemenu
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
            // FancySidemenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkGray;
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FancySidemenu";
            this.Size = new System.Drawing.Size(100, 200);
            this.BackColorChanged += new System.EventHandler(this.FancySidemenu_BackColorChanged);
            this.ForeColorChanged += new System.EventHandler(this.FancySidemenu_ForeColorChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FancySidemenu_Paint);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.FancySidemenu_MouseDown);
            this.Resize += new System.EventHandler(this.FancySidemenu_Resize);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
