namespace Scissors.EffectAPI.UserControls
{
    partial class EffectControl
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
            this.effectName = new System.Windows.Forms.Label();
            this.effectDescription = new System.Windows.Forms.Label();
            this.uninstallButton = new System.Windows.Forms.Button();
            this.effectVersionLabel = new System.Windows.Forms.Label();
            this.effectVersion = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // effectName
            // 
            this.effectName.AutoSize = true;
            this.effectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.effectName.Location = new System.Drawing.Point(3, 3);
            this.effectName.Name = "effectName";
            this.effectName.Size = new System.Drawing.Size(61, 24);
            this.effectName.TabIndex = 0;
            this.effectName.Text = "Name";
            // 
            // effectDescription
            // 
            this.effectDescription.AutoSize = true;
            this.effectDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.effectDescription.Location = new System.Drawing.Point(3, 28);
            this.effectDescription.Name = "effectDescription";
            this.effectDescription.Size = new System.Drawing.Size(79, 17);
            this.effectDescription.TabIndex = 1;
            this.effectDescription.Text = "Description";
            // 
            // uninstallButton
            // 
            this.uninstallButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.uninstallButton.Location = new System.Drawing.Point(677, 64);
            this.uninstallButton.Name = "uninstallButton";
            this.uninstallButton.Size = new System.Drawing.Size(75, 23);
            this.uninstallButton.TabIndex = 2;
            this.uninstallButton.Text = "Uninstall";
            this.uninstallButton.UseVisualStyleBackColor = true;
            this.uninstallButton.Click += new System.EventHandler(this.Uninstall);
            // 
            // effectVersionLabel
            // 
            this.effectVersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effectVersionLabel.AutoSize = true;
            this.effectVersionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.effectVersionLabel.Location = new System.Drawing.Point(695, -1);
            this.effectVersionLabel.Name = "effectVersionLabel";
            this.effectVersionLabel.Size = new System.Drawing.Size(57, 16);
            this.effectVersionLabel.TabIndex = 3;
            this.effectVersionLabel.Text = "Version:";
            // 
            // effectVersion
            // 
            this.effectVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.effectVersion.AutoSize = true;
            this.effectVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.effectVersion.Location = new System.Drawing.Point(717, 15);
            this.effectVersion.Name = "effectVersion";
            this.effectVersion.Size = new System.Drawing.Size(35, 16);
            this.effectVersion.TabIndex = 4;
            this.effectVersion.Text = "0.0.1";
            // 
            // EffectControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.effectName);
            this.Controls.Add(this.effectVersion);
            this.Controls.Add(this.effectVersionLabel);
            this.Controls.Add(this.uninstallButton);
            this.Controls.Add(this.effectDescription);
            this.Name = "EffectControl";
            this.Size = new System.Drawing.Size(755, 90);
            this.Load += new System.EventHandler(this.Loaded);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label effectName;
        private System.Windows.Forms.Label effectDescription;
        private System.Windows.Forms.Button uninstallButton;
        private System.Windows.Forms.Label effectVersionLabel;
        private System.Windows.Forms.Label effectVersion;
    }
}
