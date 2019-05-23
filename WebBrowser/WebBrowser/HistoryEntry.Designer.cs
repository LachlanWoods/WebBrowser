namespace WebBrowser {
    partial class HistoryEntry {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.historyPanel = new System.Windows.Forms.Panel();
            this.historyDate = new System.Windows.Forms.Label();
            this.removeHistroyButton = new System.Windows.Forms.Button();
            this.historyURL = new System.Windows.Forms.Label();
            this.historyPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // historyPanel
            // 
            this.historyPanel.AutoSize = true;
            this.historyPanel.BackColor = System.Drawing.SystemColors.Window;
            this.historyPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.historyPanel.Controls.Add(this.historyDate);
            this.historyPanel.Controls.Add(this.removeHistroyButton);
            this.historyPanel.Controls.Add(this.historyURL);
            this.historyPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.historyPanel.Location = new System.Drawing.Point(0, 0);
            this.historyPanel.Name = "historyPanel";
            this.historyPanel.Size = new System.Drawing.Size(300, 55);
            this.historyPanel.TabIndex = 2;
            this.historyPanel.Click += new System.EventHandler(this.historyPanel_Click);
            // 
            // historyDate
            // 
            this.historyDate.AutoSize = true;
            this.historyDate.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.historyDate.Location = new System.Drawing.Point(5, 29);
            this.historyDate.Name = "historyDate";
            this.historyDate.Size = new System.Drawing.Size(132, 13);
            this.historyDate.TabIndex = 2;
            this.historyDate.Text = "Access Date: 14/10/2018";
            this.historyDate.Click += new System.EventHandler(this.historyPanel_Click);
            // 
            // removeHistroyButton
            // 
            this.removeHistroyButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.removeHistroyButton.FlatAppearance.BorderSize = 0;
            this.removeHistroyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeHistroyButton.Image = global::WebBrowser.Properties.Resources.CloseButton;
            this.removeHistroyButton.Location = new System.Drawing.Point(268, 0);
            this.removeHistroyButton.Name = "removeHistroyButton";
            this.removeHistroyButton.Size = new System.Drawing.Size(30, 53);
            this.removeHistroyButton.TabIndex = 1;
            this.removeHistroyButton.UseVisualStyleBackColor = true;
            this.removeHistroyButton.Click += new System.EventHandler(this.removeHistroyButton_Click);
            // 
            // historyURL
            // 
            this.historyURL.AutoEllipsis = true;
            this.historyURL.AutoSize = true;
            this.historyURL.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.historyURL.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.historyURL.Location = new System.Drawing.Point(3, 0);
            this.historyURL.Name = "historyURL";
            this.historyURL.Size = new System.Drawing.Size(148, 17);
            this.historyURL.TabIndex = 0;
            this.historyURL.Text = "http://www.google.com";
            this.historyURL.Click += new System.EventHandler(this.historyPanel_Click);
            // 
            // HistoryEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.historyPanel);
            this.Name = "HistoryEntry";
            this.Size = new System.Drawing.Size(300, 55);
            this.historyPanel.ResumeLayout(false);
            this.historyPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel historyPanel;
        private System.Windows.Forms.Label historyDate;
        private System.Windows.Forms.Button removeHistroyButton;
        private System.Windows.Forms.Label historyURL;
    }
}
