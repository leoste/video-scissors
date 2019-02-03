namespace Scissors.UserControls
{
    partial class Layer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.toggleLock = new System.Windows.Forms.Button();
            this.toggleVisibility = new System.Windows.Forms.Button();
            this.removeLayer = new System.Windows.Forms.Button();
            this.createLayer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.toggleLock);
            this.panel1.Controls.Add(this.toggleVisibility);
            this.panel1.Controls.Add(this.removeLayer);
            this.panel1.Controls.Add(this.createLayer);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(46, 40);
            this.panel1.TabIndex = 4;
            // 
            // toggleLock
            // 
            this.toggleLock.Location = new System.Drawing.Point(20, 0);
            this.toggleLock.Margin = new System.Windows.Forms.Padding(0);
            this.toggleLock.Name = "toggleLock";
            this.toggleLock.Size = new System.Drawing.Size(20, 20);
            this.toggleLock.TabIndex = 3;
            this.toggleLock.Text = "lock";
            this.toggleLock.UseVisualStyleBackColor = true;
            // 
            // toggleVisibility
            // 
            this.toggleVisibility.Location = new System.Drawing.Point(20, 20);
            this.toggleVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.toggleVisibility.Name = "toggleVisibility";
            this.toggleVisibility.Size = new System.Drawing.Size(20, 20);
            this.toggleVisibility.TabIndex = 2;
            this.toggleVisibility.Text = "visiblity";
            this.toggleVisibility.UseVisualStyleBackColor = true;
            // 
            // removeLayer
            // 
            this.removeLayer.Location = new System.Drawing.Point(0, 0);
            this.removeLayer.Margin = new System.Windows.Forms.Padding(0);
            this.removeLayer.Name = "removeLayer";
            this.removeLayer.Size = new System.Drawing.Size(20, 20);
            this.removeLayer.TabIndex = 1;
            this.removeLayer.Text = "-";
            this.removeLayer.UseVisualStyleBackColor = true;
            this.removeLayer.Click += new System.EventHandler(this.removeLayer_Click);
            // 
            // createLayer
            // 
            this.createLayer.Location = new System.Drawing.Point(0, 20);
            this.createLayer.Margin = new System.Windows.Forms.Padding(0);
            this.createLayer.Name = "createLayer";
            this.createLayer.Size = new System.Drawing.Size(20, 20);
            this.createLayer.TabIndex = 0;
            this.createLayer.Text = "+";
            this.createLayer.UseVisualStyleBackColor = true;
            this.createLayer.Click += new System.EventHandler(this.createLayer_Click);
            // 
            // Layer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.Name = "Layer";
            this.Size = new System.Drawing.Size(400, 46);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button toggleLock;
        private System.Windows.Forms.Button toggleVisibility;
        private System.Windows.Forms.Button removeLayer;
        private System.Windows.Forms.Button createLayer;
    }
}
