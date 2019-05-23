using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Windows.Forms;

namespace WebBrowser {
    /// <summary>
    /// The main window of the web browser
    /// </summary>
    public partial class BrowserWindow : Form {

        private SlidingPanel historyPanel;
        private SlidingPanel favouritesPanel;

        /// <summary>
        /// Constructor: Initilizes the browser window
        /// </summary>
        public BrowserWindow() {
            InitializeComponent();
        }

        
        /// <summary>
        /// Callback when main window form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowserWindow_Load(object sender, EventArgs e) {
            BrowserManager.Instance.Window = this; //save this window in our manager so we can access GUI controls easily

            //create favourites and history sliding panels
            historyPanel = new SlidingPanel();
            Controls.Add(historyPanel);
            Controls.SetChildIndex(historyPanel, 0);
            favouritesPanel = new SlidingPanel();
            Controls.Add(favouritesPanel);
            Controls.SetChildIndex(favouritesPanel, 0);

            CreateNewTab(0); //create the first tab when the program is launched
        }

        /// <summary>
        /// Creates a new tab and displays it in the tab bar at position index
        /// </summary>
        /// <param name="index">The position of the new tab within the tabbar</param>
        public void CreateNewTab(int index) {
            Tab newTab = new Tab();
            tabBar.TabPages.Insert(index, newTab);
            ResizeTabs(); //update the tabs width to fit within the tabbar
            tabBar.SelectedIndex = index;
        }

        /// <summary>
        /// Closes the tab at the position of index within the tab bar
        /// </summary>
        /// <param name="index">The index of the tab to close</param>
        public void CloseTab(int index) {
            tabBar.TabPages.RemoveAt(index); //remove the tab
            tabBar.SelectedIndex = index; //select the tab beside the one we just closed
            ResizeTabs(); //update the tabs width to fit within the tabbar 
            if (this.tabBar.TabPages.Count <= 1) { //if we closed all tabs, close the web browser
                Application.Exit();
            }
        }

        /// <summary>
        /// Updates the width of all tabs to fit within the tab bar
        /// </summary>
        private void ResizeTabs() {
            int maxTabWidth = 150; //the maximum size of a single tab
            int tabWidth = (this.Width / this.tabBar.TabCount) - 5; //divides the screen width - 5px offset by the number of tabs

            if (tabWidth > maxTabWidth) { //limit the width of a tab to the maxTabWidth
                tabWidth = maxTabWidth;
            }
            tabBar.ItemSize = new Size(tabWidth, tabBar.ItemSize.Height); //update the size of all tabs within the tab bar
        }

        /// <summary>
        /// Returns a reference to the back button
        /// </summary>
        /// <returns>The back button control object</returns>
        public Button GetBackButton() {
            return backButton;
        }
        /// <summary>
        /// Returns a reference to the forward button
        /// </summary>
        /// <returns>The forward button control object</returns>
        public Button GetForwardButton() {
            return forwardButton;
        }

        /// <summary>
        /// Called when a tab is clicked. A new tab is added if the "new tab" tab is clicked,
        /// otherwise we check if the close button was pressed.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void tabBar_MouseClick(object sender, MouseEventArgs e) {
            int lastIndex = tabBar.TabCount - 1; //the index of the last tab, aka the new tab button

            if (tabBar.GetTabRect(lastIndex).Contains(e.Location)) { //if we clicked on the last tab, create a new tab
                CreateNewTab(lastIndex);
            }
            else {
                for (int i = 0; i < tabBar.TabPages.Count; i++) { //for each button, check if we pressed on the close tab button
                    Rectangle tabRect = tabBar.GetTabRect(i); //holds the area of the tab at index i
                    Bitmap closeImage = Properties.Resources.CloseButton;
                    //holds the area of the close image, accounting for offsets
                    Rectangle imageRect = new Rectangle((tabRect.Right - (closeImage.Width + 5)), tabRect.Top + (tabRect.Height - closeImage.Height) / 2,closeImage.Width,closeImage.Height);
                    if (imageRect.Contains(e.Location)) { //if we clicked on the close image
                        CloseTab(i);
                    }
                }
            }
        }

        /// <summary>
        /// Draws a close button and tab name onto tabs, or the "add tab" image to the final lab
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void tabBar_DrawItem(object sender, DrawItemEventArgs e) {
            TabPage tabPage = tabBar.TabPages[e.Index];
            Rectangle tabRect = tabBar.GetTabRect(e.Index); //get the rectangle area of the current tab being drawn

            if (e.Index == tabBar.TabCount - 1) { //display the new tab image on the last tab only
                Bitmap newTabImage = Properties.Resources.NewTab;
                e.Graphics.DrawImage(newTabImage, tabRect.X + (tabRect.Width / 2) - newTabImage.Width/2, tabRect.Top + (tabRect.Height - newTabImage.Height) / 2);
            }
            else { //display the close tab button and url on all other tabs
                Bitmap closeImage = Properties.Resources.CloseButton;
                //clear previous tab names
                e.Graphics.SetClip(tabRect);
                e.Graphics.Clear(Color.White);
                //draw the title of the tab
                TextRenderer.DrawText(e.Graphics, tabPage.Text, tabPage.Font, tabRect, tabPage.ForeColor, TextFormatFlags.Left);
                //draw the close button
                e.Graphics.DrawImage(closeImage, (tabRect.Right - closeImage.Width), tabRect.Top + (tabRect.Height - closeImage.Height) / 2);
            }
        }

        /// <summary>
        /// Callback when selecting a new tab from the tab bar.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void tabBar_Selecting(object sender, TabControlCancelEventArgs e) {
            //do not allow the user to open the new tab page, i.e treat it like a button
            if (e.TabPageIndex == tabBar.TabCount - 1) {e.Cancel = true;}
        }

        /// <summary>
        /// Resizes tabs when the browser window is resized
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void BrowserWindow_SizeChanged(object sender, EventArgs e) {
            ResizeTabs();
        }

        /// <summary>
        /// Gets the currently selected tab, and goes back to the previous page. This
        /// callback is triggered when the back button is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void backButton_Click(object sender, EventArgs e) {
            Tab currentTab = (Tab)tabBar.SelectedTab;
            currentTab.NavigateToPage(currentTab.GetLocalTabHistory().GetBackPage(), false);
        }

        /// <summary>
        /// Gets the currently selected tab, and goes forward to the next page. This callback is triggered
        /// when the forward button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forwardButton_Click(object sender, EventArgs e) {
            Tab currentTab = (Tab)tabBar.SelectedTab;
            currentTab.NavigateToPage(currentTab.GetLocalTabHistory().GetForwardPage(), false);
        }

        /// <summary>
        /// Callback when a new tab is selected
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void tabBar_SelectedIndexChanged(object sender, EventArgs e) {
            Tab currentTab = (Tab)tabBar.SelectedTab;
            BrowserManager.Instance.CurrentTab = currentTab;
            currentTab.GetLocalTabHistory().UpdateNavButtons();
        }

        /// <summary>
        /// Callback when the home button is clicked. Navigates the user to their homepage
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void homeButton_Click(object sender, EventArgs e) {

            Contract.Requires(Properties.Settings.Default.homepage.Length > 0); //contract to ensure a homepage is set

            Tab currentTab = (Tab)tabBar.SelectedTab; //gets the current tab
            if (Properties.Settings.Default.homepage.Length > 0) { //if a homepage has been set
                Webpage homepage = new Webpage(Properties.Settings.Default.homepage); //creates a new webpage object with the homepage url
                currentTab.NavigateToPage(homepage, true); //redirects the current tab to the homepage
            }
            else { //no homepage has been set.
                MessageBox.Show("You have not set your homepage. Right click on a webpage and select 'Set as home page' to create your homepage", "No home page set!");
            }
        }

        /// <summary>
        /// Callback when the history button is pressed. This will open the history sliding panel.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void historyButton_Click(object sender, EventArgs e) {

            Contract.Requires(favouritesPanel != null && historyPanel != null); //contract to ensure the two required side panels have been created

            favouritesPanel.ClosePanel(); //close the favouritesPanel if it is opened
            historyPanel.TogglePanel(); // open or close the historyPanel

            if (!historyPanel.IsOpen) { //fetch and display history if we just opened the side panel
                List<HistoryEntry> historyList = BrowserManager.Instance.History.GetHistory(); //gets all history entry objects
                historyPanel.Controls.AddRange(historyList.ToArray()); //add all history entries to the side panel
            }
        }

        /// <summary>
        /// Callback when the favourites button is pressed. This will open the favourites sliding panel.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void favouritesButton_Click(object sender, EventArgs e) {

            Contract.Requires(favouritesPanel != null && historyPanel != null); //contract to ensure the two required side panels have been created

            historyPanel.ClosePanel(); //close the history panel if it is opened
            favouritesPanel.TogglePanel(); //open or close the favourites panel

            if (!favouritesPanel.IsOpen) { //fetch and display history
                List<FavouriteEntry> favouritesList = BrowserManager.Instance.Favourites.GetFavourites(); //gets all favourites entry objects
                favouritesPanel.Controls.AddRange(favouritesList.ToArray()); //add all favourite entries to the side panel
            }
        }

        /// <summary>
        /// Callback when the refresh button is clicked. Refreshes the current tabs' webpage
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void refreshButton_Click(object sender, EventArgs e) {
            Tab currentTab = (Tab)tabBar.SelectedTab;
            currentTab.RefreshPage();
        }

        /// <summary>
        /// Callback when a key is pressed. This method listens for hotkeys being pressed
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void BrowserWindow_KeyDown(object sender, KeyEventArgs e) {
            //create a new tab when control n is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.N) {
                int lastIndex = tabBar.TabCount - 1; //the index of the last tab, aka the new tab button
                CreateNewTab(lastIndex);
                return;
            }

            //add to favourites when control f is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F) {
                Tab currentTab = (Tab)tabBar.SelectedTab;
                Webpage currentPage = currentTab.GetLocalTabHistory().GetCurrentPage();
                FavouriteEntry newFav = new FavouriteEntry(currentPage.GetUrl(), currentPage);
                BrowserManager.Instance.Favourites.AddToFavourites(newFav);
                return;
            }

            //refresh the page when control r is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.R) {
                Tab currentTab = (Tab)tabBar.SelectedTab;
                currentTab.RefreshPage();
                return;
            }

            //Set the homepage when control h is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.H) {
                BrowserManager.Instance.SetHomePage();
                return;
            }

            //go back when control Z is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.Z) {
                backButton_Click(null, null);
                return;
            }

            //go forward when control X is pressed
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.X) {
                forwardButton_Click(null, null);
                return;
            }
        }

        /// <summary>
        /// Callback when the CodeButton is pressed.
        /// This will toggle the current tab between showing
        /// HTML code and rendered html.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void CodeButton_Click(object sender, EventArgs e) {
            Tab currentTab = (Tab)tabBar.SelectedTab;
            currentTab.ToggleHTMLView();
        }
    }
}
