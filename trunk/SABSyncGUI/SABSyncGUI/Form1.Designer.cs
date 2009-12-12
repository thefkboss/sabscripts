namespace SABSyncGUI
{
    partial class SABSyncGUI
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
            this.saveButton = new System.Windows.Forms.Button();
            this.tvRootText = new System.Windows.Forms.TextBox();
            this.tvRoot = new System.Windows.Forms.Label();
            this.tvRootBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(497, 227);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 0;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // tvRootText
            // 
            this.tvRootText.Location = new System.Drawing.Point(57, 18);
            this.tvRootText.Name = "tvRootText";
            this.tvRootText.Size = new System.Drawing.Size(311, 20);
            this.tvRootText.TabIndex = 1;
            this.tvRootText.Text = "Hello";
            this.tvRootText.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // tvRoot
            // 
            this.tvRoot.AutoSize = true;
            this.tvRoot.Location = new System.Drawing.Point(12, 25);
            this.tvRoot.Name = "tvRoot";
            this.tvRoot.Size = new System.Drawing.Size(39, 13);
            this.tvRoot.TabIndex = 2;
            this.tvRoot.Text = "tvRoot";
            this.tvRoot.Click += new System.EventHandler(this.label1_Click);
            // 
            // tvRootBrowse
            // 
            this.tvRootBrowse.Location = new System.Drawing.Point(374, 15);
            this.tvRootBrowse.Name = "tvRootBrowse";
            this.tvRootBrowse.Size = new System.Drawing.Size(75, 23);
            this.tvRootBrowse.TabIndex = 3;
            this.tvRootBrowse.Text = "Browse";
            this.tvRootBrowse.UseVisualStyleBackColor = true;
            this.tvRootBrowse.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // SABSyncGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 262);
            this.Controls.Add(this.tvRootBrowse);
            this.Controls.Add(this.tvRoot);
            this.Controls.Add(this.tvRootText);
            this.Controls.Add(this.saveButton);
            this.Name = "SABSyncGUI";
            this.Text = "SABSync GUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.TextBox tvRootText;
        private System.Windows.Forms.Label tvRoot;
        private System.Windows.Forms.Button tvRootBrowse;

    }
}

