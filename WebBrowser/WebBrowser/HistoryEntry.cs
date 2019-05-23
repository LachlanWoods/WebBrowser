using System;
using System.Windows.Forms;

namespace WebBrowser {
    /// <summary>
    /// A control that is used to display a single webpage in the history panel
    /// </summary>
    partial class HistoryEntry : UserControl {

        private Webpage page;

        /// <summary>
        /// Constructor which creates a new History Entry for the specified Webpage
        /// </summary>
        /// <param name="page">The webpage corresponding to this history entry</param>
        public HistoryEntry(Webpage page) {
            InitializeComponent();

            this.page = page;
            Dock = DockStyle.Top;
            SetupUI(); //create the UI of this history entry
        }

        /// <summary>
        /// Returns the webpage of this history entry
        /// </summary>
        /// <returns>The Webpage of this history entry</returns>
        public Webpage GetPage() {
            return page;
        }

        /// <summary>
        /// Sets the text of the url and date label displayed on this entry.
        /// </summary>
        private void SetupUI() {
            historyURL.Text = page.GetUrl();
            historyDate.Text = "Accessed: " + page.GetAccessDate().ToString(); //display the date this page was accessed
        }

        /// <summary>
        /// Callback when the remove history button is clicked. This will remove a history entry from the history list.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void removeHistroyButton_Click(object sender, EventArgs e) {
            BrowserManager.Instance.History.RemoveFromHistory(this);
            this.Parent.Controls.Remove(this); //remove this entry from the history side panel
        }

        /// <summary>
        /// Button Callback. Navigate to the webpage corresponding to this history entry when it is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void historyPanel_Click(object sender, EventArgs e) {
            Tab currentTab = BrowserManager.Instance.CurrentTab;
            currentTab.NavigateToPage(page, true);
        }

    }
}
