namespace Scissors.EffectAPI
{
    partial class EffectLoader
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
            this.effectSearch = new System.Windows.Forms.TextBox();
            this.searchLabel = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.installedEffects = new System.Windows.Forms.TabPage();
            this.installedPlugins = new System.Windows.Forms.FlowLayoutPanel();
            this.availableEffects = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.installedEffects.SuspendLayout();
            this.SuspendLayout();
            // 
            // effectSearch
            // 
            this.effectSearch.Location = new System.Drawing.Point(12, 41);
            this.effectSearch.Name = "effectSearch";
            this.effectSearch.Size = new System.Drawing.Size(776, 20);
            this.effectSearch.TabIndex = 0;
            this.effectSearch.TextChanged += new System.EventHandler(this.SearchUpdated);
            // 
            // searchLabel
            // 
            this.searchLabel.AutoSize = true;
            this.searchLabel.Location = new System.Drawing.Point(13, 22);
            this.searchLabel.Name = "searchLabel";
            this.searchLabel.Size = new System.Drawing.Size(79, 13);
            this.searchLabel.TabIndex = 1;
            this.searchLabel.Text = "Search effects:";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.installedEffects);
            this.tabControl1.Controls.Add(this.availableEffects);
            this.tabControl1.Location = new System.Drawing.Point(12, 67);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(776, 371);
            this.tabControl1.TabIndex = 3;
            // 
            // installedEffects
            // 
            this.installedEffects.Controls.Add(this.installedPlugins);
            this.installedEffects.Location = new System.Drawing.Point(4, 22);
            this.installedEffects.Name = "installedEffects";
            this.installedEffects.Padding = new System.Windows.Forms.Padding(3);
            this.installedEffects.Size = new System.Drawing.Size(768, 345);
            this.installedEffects.TabIndex = 0;
            this.installedEffects.Text = "Installed";
            this.installedEffects.UseVisualStyleBackColor = true;
            // 
            // installedPlugins
            // 
            this.installedPlugins.AutoScroll = true;
            this.installedPlugins.Dock = System.Windows.Forms.DockStyle.Fill;
            this.installedPlugins.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.installedPlugins.Location = new System.Drawing.Point(3, 3);
            this.installedPlugins.Name = "installedPlugins";
            this.installedPlugins.Size = new System.Drawing.Size(762, 339);
            this.installedPlugins.TabIndex = 0;
            this.installedPlugins.WrapContents = false;
            // 
            // availableEffects
            // 
            this.availableEffects.Location = new System.Drawing.Point(4, 22);
            this.availableEffects.Name = "availableEffects";
            this.availableEffects.Padding = new System.Windows.Forms.Padding(3);
            this.availableEffects.Size = new System.Drawing.Size(768, 345);
            this.availableEffects.TabIndex = 1;
            this.availableEffects.Text = "Available";
            this.availableEffects.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(713, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add Effect";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddEffect);
            // 
            // EffectLoader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.searchLabel);
            this.Controls.Add(this.effectSearch);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EffectLoader";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Effects...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormLoaded);
            this.tabControl1.ResumeLayout(false);
            this.installedEffects.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox effectSearch;
        private System.Windows.Forms.Label searchLabel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage installedEffects;
        private System.Windows.Forms.TabPage availableEffects;
        private System.Windows.Forms.FlowLayoutPanel installedPlugins;
        private System.Windows.Forms.Button button1;
    }
}