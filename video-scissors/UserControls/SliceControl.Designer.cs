namespace Scissors.UserControls
{
    partial class SliceControl
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
            this.removeSlice = new System.Windows.Forms.Button();
            this.createSlice = new System.Windows.Forms.Button();
            this.moveUp = new System.Windows.Forms.Button();
            this.moveDown = new System.Windows.Forms.Button();
            this.toggleLock = new System.Windows.Forms.Button();
            this.toggleVisibility = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // removeSlice
            // 
            this.removeSlice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.removeSlice.Location = new System.Drawing.Point(9, 6);
            this.removeSlice.Margin = new System.Windows.Forms.Padding(9, 6, 0, 0);
            this.removeSlice.Name = "removeSlice";
            this.removeSlice.Size = new System.Drawing.Size(20, 20);
            this.removeSlice.TabIndex = 0;
            this.removeSlice.Text = "-";
            this.removeSlice.UseVisualStyleBackColor = true;
            this.removeSlice.Click += new System.EventHandler(this.removeSlice_Click);
            // 
            // createSlice
            // 
            this.createSlice.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.createSlice.Location = new System.Drawing.Point(9, 26);
            this.createSlice.Margin = new System.Windows.Forms.Padding(0);
            this.createSlice.Name = "createSlice";
            this.createSlice.Size = new System.Drawing.Size(20, 20);
            this.createSlice.TabIndex = 1;
            this.createSlice.Text = "+";
            this.createSlice.UseVisualStyleBackColor = true;
            this.createSlice.Click += new System.EventHandler(this.createSlice_Click);
            // 
            // moveUp
            // 
            this.moveUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.moveUp.Location = new System.Drawing.Point(29, 6);
            this.moveUp.Margin = new System.Windows.Forms.Padding(0);
            this.moveUp.Name = "moveUp";
            this.moveUp.Size = new System.Drawing.Size(20, 20);
            this.moveUp.TabIndex = 2;
            this.moveUp.Text = "u";
            this.moveUp.UseVisualStyleBackColor = true;
            this.moveUp.Click += new System.EventHandler(this.moveUp_Click);
            // 
            // moveDown
            // 
            this.moveDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.moveDown.Location = new System.Drawing.Point(29, 26);
            this.moveDown.Margin = new System.Windows.Forms.Padding(0);
            this.moveDown.Name = "moveDown";
            this.moveDown.Size = new System.Drawing.Size(20, 20);
            this.moveDown.TabIndex = 3;
            this.moveDown.Text = "d";
            this.moveDown.UseVisualStyleBackColor = true;
            this.moveDown.Click += new System.EventHandler(this.moveDown_Click);
            // 
            // toggleLock
            // 
            this.toggleLock.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toggleLock.Location = new System.Drawing.Point(49, 6);
            this.toggleLock.Margin = new System.Windows.Forms.Padding(0);
            this.toggleLock.Name = "toggleLock";
            this.toggleLock.Size = new System.Drawing.Size(20, 20);
            this.toggleLock.TabIndex = 4;
            this.toggleLock.Text = "l";
            this.toggleLock.UseVisualStyleBackColor = true;
            this.toggleLock.Click += new System.EventHandler(this.toggleLock_Click);
            // 
            // toggleVisibility
            // 
            this.toggleVisibility.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.toggleVisibility.Location = new System.Drawing.Point(49, 26);
            this.toggleVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.toggleVisibility.Name = "toggleVisibility";
            this.toggleVisibility.Size = new System.Drawing.Size(20, 20);
            this.toggleVisibility.TabIndex = 5;
            this.toggleVisibility.Text = "v";
            this.toggleVisibility.UseVisualStyleBackColor = true;
            this.toggleVisibility.Click += new System.EventHandler(this.toggleVisibility_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(72, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(72, 46);
            this.flowLayoutPanel1.TabIndex = 6;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 50);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // SliceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.toggleVisibility);
            this.Controls.Add(this.toggleLock);
            this.Controls.Add(this.moveDown);
            this.Controls.Add(this.moveUp);
            this.Controls.Add(this.createSlice);
            this.Controls.Add(this.removeSlice);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SliceControl";
            this.Size = new System.Drawing.Size(147, 52);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button removeSlice;
        private System.Windows.Forms.Button createSlice;
        private System.Windows.Forms.Button moveUp;
        private System.Windows.Forms.Button moveDown;
        private System.Windows.Forms.Button toggleLock;
        private System.Windows.Forms.Button toggleVisibility;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
    }
}
