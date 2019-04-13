namespace Scissors.Timeline
{
    partial class Menu
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.fancySidemenu1 = new Scissors.Controls.FancySidemenu();
            this.verticalScrollbar = new Scissors.Controls.FancyScrollbar();
            this.openMediaDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(112, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(261, 200);
            this.flowLayoutPanel1.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Gainsboro;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.label1.Location = new System.Drawing.Point(3, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 75);
            this.label1.TabIndex = 0;
            this.label1.Text = "+";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // fancySidemenu1
            // 
            this.fancySidemenu1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.fancySidemenu1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.fancySidemenu1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(186)));
            this.fancySidemenu1.ForeColor = System.Drawing.Color.Black;
            this.fancySidemenu1.HighlightColor = System.Drawing.Color.Gainsboro;
            this.fancySidemenu1.Location = new System.Drawing.Point(0, 0);
            this.fancySidemenu1.Margin = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.fancySidemenu1.Name = "fancySidemenu1";
            this.fancySidemenu1.SelectedId = 0;
            this.fancySidemenu1.Size = new System.Drawing.Size(100, 200);
            this.fancySidemenu1.TabIndex = 14;
            this.fancySidemenu1.Tabs = new string[] {
        "Media",
        "Effects",
        "Presets"};
            this.fancySidemenu1.TabClicked += new System.EventHandler<Scissors.SelectionEventArgs>(this.fancySidemenu1_TabClicked);
            // 
            // verticalScrollbar
            // 
            this.verticalScrollbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.verticalScrollbar.BackColor = System.Drawing.Color.Silver;
            this.verticalScrollbar.ForeColor = System.Drawing.Color.Gainsboro;
            this.verticalScrollbar.Location = new System.Drawing.Point(376, 0);
            this.verticalScrollbar.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.verticalScrollbar.Maximum = 10;
            this.verticalScrollbar.Minimum = 0;
            this.verticalScrollbar.Name = "verticalScrollbar";
            this.verticalScrollbar.ScrollDirection = Scissors.Controls.ScrollDirection.UpToDown;
            this.verticalScrollbar.ScrollWidth = 3;
            this.verticalScrollbar.Size = new System.Drawing.Size(24, 200);
            this.verticalScrollbar.TabIndex = 13;
            this.verticalScrollbar.Value = 3;
            // 
            // openMediaDialog
            // 
            this.openMediaDialog.FileName = "openFileDialog1";
            this.openMediaDialog.Multiselect = true;
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.fancySidemenu1);
            this.Controls.Add(this.verticalScrollbar);
            this.Name = "Menu";
            this.Size = new System.Drawing.Size(400, 200);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Scissors.Controls.FancyScrollbar verticalScrollbar;
        private Controls.FancySidemenu fancySidemenu1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openMediaDialog;
    }
}
