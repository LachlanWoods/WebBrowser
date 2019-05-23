namespace WebBrowser {
    partial class FavouriteEntry {
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
            this.favouritePanel = new System.Windows.Forms.Panel();
            this.editFavouriteButton = new System.Windows.Forms.Button();
            this.removeFavouriteButton = new System.Windows.Forms.Button();
            this.favouriteUrl = new System.Windows.Forms.Label();
            this.favouriteName = new System.Windows.Forms.Label();
            this.favouritePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // favouritePanel
            // 
            this.favouritePanel.BackColor = System.Drawing.SystemColors.Window;
            this.favouritePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.favouritePanel.Controls.Add(this.editFavouriteButton);
            this.favouritePanel.Controls.Add(this.removeFavouriteButton);
            this.favouritePanel.Controls.Add(this.favouriteUrl);
            this.favouritePanel.Controls.Add(this.favouriteName);
            this.favouritePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favouritePanel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.favouritePanel.Location = new System.Drawing.Point(0, 0);
            this.favouritePanel.Name = "favouritePanel";
            this.favouritePanel.Size = new System.Drawing.Size(300, 55);
            this.favouritePanel.TabIndex = 0;
            this.favouritePanel.Click += new System.EventHandler(this.favouritePanel_Click);
            // 
            // editFavouriteButton
            // 
            this.editFavouriteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.editFavouriteButton.FlatAppearance.BorderSize = 0;
            this.editFavouriteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editFavouriteButton.Location = new System.Drawing.Point(193, 0);
            this.editFavouriteButton.Name = "editFavouriteButton";
            this.editFavouriteButton.Size = new System.Drawing.Size(75, 53);
            this.editFavouriteButton.TabIndex = 3;
            this.editFavouriteButton.Text = "Edit";
            this.editFavouriteButton.UseVisualStyleBackColor = true;
            this.editFavouriteButton.Click += new System.EventHandler(this.editFavouriteButton_Click);
            // 
            // removeFavouriteButton
            // 
            this.removeFavouriteButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.removeFavouriteButton.FlatAppearance.BorderSize = 0;
            this.removeFavouriteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeFavouriteButton.Image = global::WebBrowser.Properties.Resources.CloseButton;
            this.removeFavouriteButton.Location = new System.Drawing.Point(268, 0);
            this.removeFavouriteButton.Name = "removeFavouriteButton";
            this.removeFavouriteButton.Size = new System.Drawing.Size(30, 53);
            this.removeFavouriteButton.TabIndex = 2;
            this.removeFavouriteButton.UseVisualStyleBackColor = true;
            this.removeFavouriteButton.Click += new System.EventHandler(this.removeFavouriteButton_Click);
            // 
            // favouriteUrl
            // 
            this.favouriteUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.favouriteUrl.AutoEllipsis = true;
            this.favouriteUrl.AutoSize = true;
            this.favouriteUrl.Location = new System.Drawing.Point(0, 39);
            this.favouriteUrl.Name = "favouriteUrl";
            this.favouriteUrl.Size = new System.Drawing.Size(120, 13);
            this.favouriteUrl.TabIndex = 1;
            this.favouriteUrl.Text = "http://www.google.com";
            // 
            // favouriteName
            // 
            this.favouriteName.AutoEllipsis = true;
            this.favouriteName.AutoSize = true;
            this.favouriteName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.favouriteName.Location = new System.Drawing.Point(3, 0);
            this.favouriteName.Name = "favouriteName";
            this.favouriteName.Size = new System.Drawing.Size(54, 17);
            this.favouriteName.TabIndex = 0;
            this.favouriteName.Text = "Google";
            // 
            // FavouriteEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.favouritePanel);
            this.Name = "FavouriteEntry";
            this.Size = new System.Drawing.Size(300, 55);
            this.favouritePanel.ResumeLayout(false);
            this.favouritePanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel favouritePanel;
        private System.Windows.Forms.Label favouriteName;
        private System.Windows.Forms.Button editFavouriteButton;
        private System.Windows.Forms.Button removeFavouriteButton;
        private System.Windows.Forms.Label favouriteUrl;
    }
}
