using System;
using System.Windows.Forms;

namespace WebBrowser {
    /// <summary>
    /// A control that is used to display a single favourite webpage in the favourites panel
    /// </summary>
    partial class FavouriteEntry : UserControl {

        private string name;
        private Webpage page;

        /// <summary>
        /// Constructor method to initilise variable values and update control text.
        /// </summary>
        /// <param name="name">The name of this favourite entry</param>
        /// <param name="page">The webpage of the favourite page</param>
        public FavouriteEntry(string name, Webpage page) {
            InitializeComponent();
            
            this.name = name;
            favouriteName.Text = name;
            this.page = page;
            favouriteUrl.Text = page.GetUrl();
            Dock = DockStyle.Top;
        }

        /// <summary>
        /// Callback to display the form to edit a favourite entry when the edit button is clicked.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void editFavouriteButton_Click(object sender, EventArgs e) {
            ShowEditFavouriteForm();
        }

        /// <summary>
        /// creates the form to edit a favourite item
        /// </summary>
        private void ShowEditFavouriteForm() {

            //create panels
            Form prompt = new Form() {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Edit Favourite",
                MinimizeBox = false,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            Panel namePanel = new Panel() {
                Dock = DockStyle.Top,
                Height = 25
            };
            Panel urlPanel = new Panel() {
                Dock = DockStyle.Top,
                Height = 25
            };
            //create page name entry controls
            Label nameLabel = new Label() {
                Text = "Name:",
                Dock = DockStyle.Left
            };
            TextBox nameEntry = new TextBox() {
                Text = name,
                Dock = DockStyle.Right,
                Width = 350
            };
            Label urlLabel = new Label() {
                Text = "Url:",
                Dock = DockStyle.Left
            };
            TextBox urlEntry = new TextBox() {
                Text = page.GetUrl(),
                Dock = DockStyle.Right,
                Width = 350
            };
            Button confirmation = new Button() {
                Text = "Save",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            //add event listener to save button
            confirmation.Click += (sender, e) => { UpdateFavouriteEntry(sender, e, nameEntry.Text, urlEntry.Text); prompt.Close(); };

            //add controls
            namePanel.Controls.Add(nameLabel);
            namePanel.Controls.Add(nameEntry);
            urlPanel.Controls.Add(urlEntry);
            urlPanel.Controls.Add(urlLabel);
            prompt.Controls.Add(namePanel);
            prompt.Controls.Add(urlPanel);
            prompt.Controls.Add(confirmation);
            
            prompt.AcceptButton = confirmation; //set prompt accept button to our new button
            prompt.Show(); //show the prompt
        }

        /// <summary>
        /// Updates the favourite entry when the save button is pressed
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        /// <param name="name">The new name of this favourite page</param>
        /// <param name="url">The new url of this favourite page</param>
        private void UpdateFavouriteEntry(object sender, EventArgs e, string name, string url) {
            this.name = name;
            page = new Webpage(url); //create a new webpage based on the new url
            favouriteName.Text = name;
            favouriteUrl.Text = url;
            BrowserManager.Instance.Favourites.SaveFavourites(); //save the favourites list
        }

        //override ToString to return a json object
        public override string ToString() {
            return string.Format("<favouriteEntry name='{0}' url='{1}'></favouriteEntry>", name, page.GetUrl());
        }

        /// <summary>
        /// Callback to remove a favourite entry
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void removeFavouriteButton_Click(object sender, EventArgs e) {
            BrowserManager.Instance.Favourites.RemoveFromFavourites(this);
            this.Parent.Controls.Remove(this); //remove this entry from the favourites side panel
        }

        /// <summary>
        /// Navigate to a favourite page when a favourite page is clicked
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void favouritePanel_Click(object sender, EventArgs e) {
            BrowserManager.Instance.CurrentTab.NavigateToPage(page, true);
        }
    }
}
