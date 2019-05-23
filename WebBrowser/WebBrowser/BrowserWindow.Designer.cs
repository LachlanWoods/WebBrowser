namespace WebBrowser {
    partial class BrowserWindow {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BrowserWindow));
            this.tabBar = new System.Windows.Forms.TabControl();
            this.newTabButton = new System.Windows.Forms.TabPage();
            this.headerPanel = new System.Windows.Forms.Panel();
            this.tooltip = new System.Windows.Forms.ToolTip(this.components);
            this.refreshButton = new System.Windows.Forms.Button();
            this.favouritesButton = new System.Windows.Forms.Button();
            this.homeButton = new System.Windows.Forms.Button();
            this.historyButton = new System.Windows.Forms.Button();
            this.forwardButton = new System.Windows.Forms.Button();
            this.backButton = new System.Windows.Forms.Button();
            this.CodeButton = new System.Windows.Forms.Button();
            this.tabBar.SuspendLayout();
            this.headerPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabBar
            // 
            this.tabBar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabBar.Controls.Add(this.newTabButton);
            this.tabBar.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabBar.ItemSize = new System.Drawing.Size(150, 18);
            this.tabBar.Location = new System.Drawing.Point(0, 36);
            this.tabBar.Name = "tabBar";
            this.tabBar.Padding = new System.Drawing.Point(20, 3);
            this.tabBar.SelectedIndex = 0;
            this.tabBar.Size = new System.Drawing.Size(1135, 572);
            this.tabBar.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabBar.TabIndex = 0;
            this.tabBar.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabBar_DrawItem);
            this.tabBar.SelectedIndexChanged += new System.EventHandler(this.tabBar_SelectedIndexChanged);
            this.tabBar.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabBar_Selecting);
            this.tabBar.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabBar_MouseClick);
            // 
            // newTabButton
            // 
            this.newTabButton.ForeColor = System.Drawing.SystemColors.Control;
            this.newTabButton.Location = new System.Drawing.Point(4, 22);
            this.newTabButton.Name = "newTabButton";
            this.newTabButton.Padding = new System.Windows.Forms.Padding(3);
            this.newTabButton.Size = new System.Drawing.Size(1127, 546);
            this.newTabButton.TabIndex = 1;
            this.newTabButton.Text = "+";
            this.newTabButton.UseVisualStyleBackColor = true;
            // 
            // headerPanel
            // 
            this.headerPanel.Controls.Add(this.CodeButton);
            this.headerPanel.Controls.Add(this.refreshButton);
            this.headerPanel.Controls.Add(this.favouritesButton);
            this.headerPanel.Controls.Add(this.homeButton);
            this.headerPanel.Controls.Add(this.historyButton);
            this.headerPanel.Controls.Add(this.forwardButton);
            this.headerPanel.Controls.Add(this.backButton);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.MaximumSize = new System.Drawing.Size(0, 30);
            this.headerPanel.MinimumSize = new System.Drawing.Size(0, 30);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1134, 30);
            this.headerPanel.TabIndex = 1;
            // 
            // refreshButton
            // 
            this.refreshButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.refreshButton.FlatAppearance.BorderSize = 0;
            this.refreshButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.refreshButton.Image = global::WebBrowser.Properties.Resources.RefreshButton;
            this.refreshButton.Location = new System.Drawing.Point(60, 0);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(30, 30);
            this.refreshButton.TabIndex = 5;
            this.tooltip.SetToolTip(this.refreshButton, "Refresh (control r)");
            this.refreshButton.UseVisualStyleBackColor = true;
            this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
            // 
            // favouritesButton
            // 
            this.favouritesButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.favouritesButton.FlatAppearance.BorderSize = 0;
            this.favouritesButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.favouritesButton.Image = global::WebBrowser.Properties.Resources.FavouritesButton;
            this.favouritesButton.Location = new System.Drawing.Point(1044, 0);
            this.favouritesButton.Name = "favouritesButton";
            this.favouritesButton.Size = new System.Drawing.Size(30, 30);
            this.favouritesButton.TabIndex = 4;
            this.tooltip.SetToolTip(this.favouritesButton, "View Favourites");
            this.favouritesButton.UseVisualStyleBackColor = true;
            this.favouritesButton.Click += new System.EventHandler(this.favouritesButton_Click);
            // 
            // homeButton
            // 
            this.homeButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.homeButton.FlatAppearance.BorderSize = 0;
            this.homeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeButton.Image = global::WebBrowser.Properties.Resources.HomeIcon;
            this.homeButton.Location = new System.Drawing.Point(1074, 0);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(30, 30);
            this.homeButton.TabIndex = 3;
            this.tooltip.SetToolTip(this.homeButton, "Go to homepage");
            this.homeButton.UseVisualStyleBackColor = true;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // historyButton
            // 
            this.historyButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.historyButton.FlatAppearance.BorderSize = 0;
            this.historyButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.historyButton.Image = global::WebBrowser.Properties.Resources.HistoryIcon;
            this.historyButton.Location = new System.Drawing.Point(1104, 0);
            this.historyButton.Name = "historyButton";
            this.historyButton.Size = new System.Drawing.Size(30, 30);
            this.historyButton.TabIndex = 2;
            this.tooltip.SetToolTip(this.historyButton, "View history");
            this.historyButton.UseVisualStyleBackColor = true;
            this.historyButton.Click += new System.EventHandler(this.historyButton_Click);
            // 
            // forwardButton
            // 
            this.forwardButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.forwardButton.Enabled = false;
            this.forwardButton.FlatAppearance.BorderSize = 0;
            this.forwardButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.forwardButton.Image = global::WebBrowser.Properties.Resources.NavArrowForward;
            this.forwardButton.Location = new System.Drawing.Point(30, 0);
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(30, 30);
            this.forwardButton.TabIndex = 1;
            this.tooltip.SetToolTip(this.forwardButton, "Go Forward (control x)");
            this.forwardButton.UseVisualStyleBackColor = true;
            this.forwardButton.Click += new System.EventHandler(this.forwardButton_Click);
            // 
            // backButton
            // 
            this.backButton.Dock = System.Windows.Forms.DockStyle.Left;
            this.backButton.Enabled = false;
            this.backButton.FlatAppearance.BorderSize = 0;
            this.backButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backButton.Image = ((System.Drawing.Image)(resources.GetObject("backButton.Image")));
            this.backButton.Location = new System.Drawing.Point(0, 0);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(30, 30);
            this.backButton.TabIndex = 0;
            this.tooltip.SetToolTip(this.backButton, "Go Back (control z)");
            this.backButton.UseVisualStyleBackColor = true;
            this.backButton.Click += new System.EventHandler(this.backButton_Click);
            // 
            // CodeButton
            // 
            this.CodeButton.FlatAppearance.BorderSize = 0;
            this.CodeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CodeButton.Image = global::WebBrowser.Properties.Resources.CodeButton;
            this.CodeButton.Location = new System.Drawing.Point(130, 0);
            this.CodeButton.Name = "CodeButton";
            this.CodeButton.Size = new System.Drawing.Size(30, 30);
            this.CodeButton.TabIndex = 6;
            this.tooltip.SetToolTip(this.CodeButton, "Toggle between HTML and code view");
            this.CodeButton.UseVisualStyleBackColor = true;
            this.CodeButton.Click += new System.EventHandler(this.CodeButton_Click);
            // 
            // BrowserWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1134, 608);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.tabBar);
            this.KeyPreview = true;
            this.Name = "BrowserWindow";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.BrowserWindow_Load);
            this.SizeChanged += new System.EventHandler(this.BrowserWindow_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BrowserWindow_KeyDown);
            this.tabBar.ResumeLayout(false);
            this.headerPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabBar;
        private System.Windows.Forms.TabPage newTabButton;
        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Button backButton;
        private System.Windows.Forms.Button forwardButton;
        private System.Windows.Forms.Button historyButton;
        private System.Windows.Forms.Button homeButton;
        private System.Windows.Forms.Button favouritesButton;
        private System.Windows.Forms.Button refreshButton;
        private System.Windows.Forms.ToolTip tooltip;
        private System.Windows.Forms.Button CodeButton;
    }
}

