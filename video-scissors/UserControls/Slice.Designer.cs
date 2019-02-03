namespace Scissors.UserControls
{
    partial class Slice
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
            this.removeSlice = new System.Windows.Forms.Button();
            this.addSlice = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.layer1 = new Scissors.UserControls.Layer();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.toggleLock);
            this.panel1.Controls.Add(this.toggleVisibility);
            this.panel1.Controls.Add(this.removeSlice);
            this.panel1.Controls.Add(this.addSlice);
            this.panel1.Location = new System.Drawing.Point(3, 9);
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
            this.toggleLock.TabIndex = 7;
            this.toggleLock.Text = "lock";
            this.toggleLock.UseVisualStyleBackColor = true;
            // 
            // toggleVisibility
            // 
            this.toggleVisibility.Location = new System.Drawing.Point(20, 20);
            this.toggleVisibility.Margin = new System.Windows.Forms.Padding(0);
            this.toggleVisibility.Name = "toggleVisibility";
            this.toggleVisibility.Size = new System.Drawing.Size(20, 20);
            this.toggleVisibility.TabIndex = 6;
            this.toggleVisibility.Text = "visiblity";
            this.toggleVisibility.UseVisualStyleBackColor = true;
            // 
            // removeSlice
            // 
            this.removeSlice.Location = new System.Drawing.Point(0, 0);
            this.removeSlice.Margin = new System.Windows.Forms.Padding(0);
            this.removeSlice.Name = "removeSlice";
            this.removeSlice.Size = new System.Drawing.Size(20, 20);
            this.removeSlice.TabIndex = 5;
            this.removeSlice.Text = "-";
            this.removeSlice.UseVisualStyleBackColor = true;
            this.removeSlice.Click += new System.EventHandler(this.removeSlice_Click);
            // 
            // addSlice
            // 
            this.addSlice.Location = new System.Drawing.Point(0, 20);
            this.addSlice.Margin = new System.Windows.Forms.Padding(0);
            this.addSlice.Name = "addSlice";
            this.addSlice.Size = new System.Drawing.Size(20, 20);
            this.addSlice.TabIndex = 4;
            this.addSlice.Text = "+";
            this.addSlice.UseVisualStyleBackColor = true;
            this.addSlice.Click += new System.EventHandler(this.addSlice_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.layer1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(49, 6);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(351, 46);
            this.flowLayoutPanel1.TabIndex = 5;
            this.flowLayoutPanel1.SizeChanged += new System.EventHandler(this.flowLayoutPanel1_SizeChanged);
            // 
            // layer1
            // 
            this.layer1.BackColor = System.Drawing.Color.Crimson;
            this.layer1.Location = new System.Drawing.Point(0, 0);
            this.layer1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.layer1.Name = "layer1";
            this.layer1.Size = new System.Drawing.Size(351, 46);
            this.layer1.TabIndex = 0;
            // 
            // Slice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "Slice";
            this.Size = new System.Drawing.Size(400, 58);
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button toggleLock;
        private System.Windows.Forms.Button toggleVisibility;
        private System.Windows.Forms.Button removeSlice;
        private System.Windows.Forms.Button addSlice;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Layer layer1;
    }
}
