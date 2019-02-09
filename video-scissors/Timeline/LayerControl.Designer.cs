namespace Scissors.Timeline
{
    partial class LayerControl
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
            this.toggleVisibility = new System.Windows.Forms.Button();
            this.toggleLock = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.moveUp = new System.Windows.Forms.Button();
            this.createLayer = new System.Windows.Forms.Button();
            this.removeLayer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // toggleVisibility
            // 
            this.toggleVisibility.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toggleVisibility.Location = new System.Drawing.Point(49, 23);
            this.toggleVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.toggleVisibility.Name = "toggleVisibility";
            this.toggleVisibility.Size = new System.Drawing.Size(20, 20);
            this.toggleVisibility.TabIndex = 11;
            this.toggleVisibility.Text = "v";
            this.toggleVisibility.UseVisualStyleBackColor = true;
            this.toggleVisibility.Click += new System.EventHandler(this.toggleVisibility_Click);
            // 
            // toggleLock
            // 
            this.toggleLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toggleLock.Location = new System.Drawing.Point(49, 3);
            this.toggleLock.Margin = new System.Windows.Forms.Padding(0, 3, 3, 0);
            this.toggleLock.Name = "toggleLock";
            this.toggleLock.Size = new System.Drawing.Size(20, 20);
            this.toggleLock.TabIndex = 10;
            this.toggleLock.Text = "l";
            this.toggleLock.UseVisualStyleBackColor = true;
            this.toggleLock.Click += new System.EventHandler(this.toggleLock_Click);
            // 
            // moveDown
            // 
            this.moveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.moveDown.Location = new System.Drawing.Point(29, 23);
            this.moveDown.Margin = new System.Windows.Forms.Padding(0);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(20, 20);
            this.moveDown.TabIndex = 9;
            this.moveDown.Text = "d";
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // moveUp
            // 
            this.moveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.moveUp.Location = new System.Drawing.Point(29, 3);
            this.moveUp.Margin = new System.Windows.Forms.Padding(0);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(20, 20);
            this.moveUp.TabIndex = 8;
            this.moveUp.Text = "u";
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // createLayer
            // 
            this.createLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.createLayer.Location = new System.Drawing.Point(9, 23);
            this.createLayer.Margin = new System.Windows.Forms.Padding(0);
            this.createLayer.Name = "createLayer";
            this.createLayer.Size = new System.Drawing.Size(20, 20);
            this.createLayer.TabIndex = 7;
            this.createLayer.Text = "+";
            this.createLayer.UseVisualStyleBackColor = true;
            this.createLayer.Click += new System.EventHandler(this.createLayer_Click);
            // 
            // removeLayer
            // 
            this.removeLayer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.removeLayer.Location = new System.Drawing.Point(9, 3);
            this.removeLayer.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.removeLayer.Name = "removeLayer";
            this.removeLayer.Size = new System.Drawing.Size(20, 20);
            this.removeLayer.TabIndex = 6;
            this.removeLayer.Text = "-";
            this.removeLayer.UseVisualStyleBackColor = true;
            this.removeLayer.Click += new System.EventHandler(this.removeLayer_Click);
            // 
            // LayerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toggleVisibility);
            this.Controls.Add(this.toggleLock);
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Controls.Add(this.createLayer);
            this.Controls.Add(this.removeLayer);
            this.Margin = new System.Windows.Forms.Padding(0, 0, 0, 3);
            this.Name = "LayerControl";
            this.Size = new System.Drawing.Size(72, 46);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button toggleVisibility;
        private System.Windows.Forms.Button toggleLock;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button createLayer;
        private System.Windows.Forms.Button removeLayer;
    }
}
