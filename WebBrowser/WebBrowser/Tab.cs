using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace WebBrowser {
    /// <summary>
    /// Extends the TabPage class. This class represents a single tab within the web browser.
    /// Each tab object can perform HTTP requests in a new thread using a background worker.
    /// </summary>
    class Tab :  TabPage{
        private RichTextBox webDisplay;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlDisplay;
        private TextBox searchBar;
        private TabHistory localHistory;
        private BackgroundWorker backgroundWorker; //the background worker that is used to implement search multithreading
        private bool showingHtml = false;
        private string currentHtml; //stores the html string of the current webpage. This is used for switching between code and rendered view.

        /// <summary>
        /// Constructor method. Creates the tabs' GUI and background worker, then goes to the homepage
        /// </summary>
        public Tab() {
            Text = "New Tab";
            localHistory = new TabHistory(); //create a new local history objects

            //create our background worker to perform non-blocking http requests
            backgroundWorker = new BackgroundWorker {
                WorkerSupportsCancellation = true
            };
            backgroundWorker.DoWork += SearchInNewThread;
            backgroundWorker.RunWorkerCompleted += SearchResultReturned;

            CreateGUI(); //setup the tabs' gui

            //go to the home page when a new tab is opened
            if (Properties.Settings.Default.homepage.Length > 0) {
                Webpage homepage = new Webpage(Properties.Settings.Default.homepage);
                NavigateToPage(homepage, true);
            }
        }

        /// <summary>
        /// Sets the background worker to load the requested page
        /// </summary>
        /// <param name="page">The webpage to load</param>
        /// <param name="addToHistory">Should the current page be added to the history</param>
        public void NavigateToPage(Webpage page, bool addToHistory) {

            Contract.Requires(page != null); //contrtact to ensure that the requested page is not null

            if (page == null) {
                return;
            }

            searchBar.Text = page.GetUrl(); //get the text from the search bar

            if (backgroundWorker.IsBusy) { //if there is already a running search request, cancel it
                backgroundWorker.CancelAsync();
            }
            else {
                backgroundWorker.RunWorkerAsync(page); //run the background worker, to fetch the requested page in a new thread

                if (addToHistory) { //add the page to local and global history if the addToHistory flag is set
                    localHistory.AddToHistory(page);
                    BrowserManager.Instance.History.AddToHistory(page);
                }

                localHistory.UpdateNavButtons(); //update the back and forward buttons (i.e) enable them if we can now go back or forward
            }
        }

        /// <summary>
        /// Callback when the Search button is pressed. This will navigate to url specified in the search text box
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void SearchButtonPressed(object sender, EventArgs e) {
            Webpage newPage = new Webpage(searchBar.Text);
            NavigateToPage(newPage, true);
        }

        /// <summary>
        /// Callback when a key is pressed and the searchbar is selected.
        /// If the enter key is pressed, then this tab will navigate to the url specified in the search text box
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void SearchBarKeyPressed(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)Keys.Return) {
                Webpage newPage = new Webpage(searchBar.Text);
                NavigateToPage(newPage, true);
            }
        }

        /// <summary>
        /// Callback when a link displayed in the webDisplay is clicked. This will navigate the current page
        /// to the url of the clicked link.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void WebLinkPressed(object sender, LinkClickedEventArgs e) {
            Webpage newPage = new Webpage(e.LinkText);
            NavigateToPage(newPage, true);
        }

        /// <summary>
        /// Callback when a link displayed in the htmlDisplay is clicked. This will navigate the current page
        /// to the url of the clicked link.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void HTMLWebLinkPressed(object sender, HtmlLinkClickedEventArgs e) {
            Webpage newPage = new Webpage(e.Link);
            NavigateToPage(newPage, true);
        }

        /// <summary>
        /// Refreshes the current webpage
        /// </summary>
        public void RefreshPage() {
            NavigateToPage(localHistory.GetCurrentPage(), false); //refresh and do not add the page to history again
        }

        /// <summary>
        /// Returns the local history of this tab
        /// </summary>
        /// <returns>The local history list</returns>
        public TabHistory GetLocalTabHistory() {
            return localHistory;
        }

        /// <summary>
        /// Toggles between showing HTML code and a rendered website
        /// on this tab only
        /// </summary>
        public void ToggleHTMLView() {
            if (showingHtml) { //if we are currently showing a rendered website, show code
                htmlDisplay.Visible = false;
                webDisplay.Visible = true;
                webDisplay.Text = currentHtml;
                showingHtml = false;
            } else { //we are currently showing html code, so show the rendered html instead
                htmlDisplay.Visible = true;
                webDisplay.Visible = false;
                htmlDisplay.Text = currentHtml;
                showingHtml = true;
            }
        }

        /// <summary>
        /// Creates the controls (GUI) of the new tab when the tab is first created
        /// </summary>
        private void CreateGUI() {
            Panel searchbarPanel = new Panel();
            searchbarPanel.Dock = DockStyle.Top;
            this.Controls.Add(searchbarPanel);

            searchBar = new TextBox();
            searchBar.Dock = DockStyle.Top;
            searchBar.Font = new Font("Serif", 18);
            //attach even hander for key presses, so we can listen for the enter key
            searchBar.KeyPress += SearchBarKeyPressed;
            searchbarPanel.Controls.Add(searchBar);

            searchbarPanel.Height = searchBar.Height;

            Button searchButton = new Button();
            searchButton.Image = (Image)(new Bitmap(Properties.Resources.SearchButton, new Size(searchButton.Height, searchButton.Height)));
            searchButton.Dock = DockStyle.Right;
            searchButton.FlatStyle = FlatStyle.Flat;
            searchButton.FlatAppearance.BorderSize = 0;
            // attach event handler for Click event 
            searchButton.Click += SearchButtonPressed;
            searchbarPanel.Controls.Add(searchButton);

            Panel displayPanel = new Panel();
            displayPanel.Dock = DockStyle.Fill;
            this.Controls.Add(displayPanel);
            displayPanel.BringToFront();

            webDisplay = new RichTextBox();
            webDisplay.AutoSize = false;
            webDisplay.Dock = DockStyle.Fill;
            webDisplay.ReadOnly = true;
            webDisplay.BorderStyle = BorderStyle.None;
            webDisplay.BackColor = Color.White;
            displayPanel.Controls.Add(webDisplay);
            webDisplay.LinkClicked += WebLinkPressed; //callback for when a link is clicked

            //display for html (additional work)
            htmlDisplay = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            htmlDisplay.Dock = DockStyle.Fill;
            htmlDisplay.AutoSize = false;
            htmlDisplay.BorderStyle = BorderStyle.None;
            htmlDisplay.BackColor = Color.White;
            htmlDisplay.LinkClicked += HTMLWebLinkPressed;
            htmlDisplay.Visible = false;
            displayPanel.Controls.Add(htmlDisplay);

            //creates a context menu for when the webdisplay is right clicked.
            ContextMenu menu = new ContextMenu();
            menu.MenuItems.Add("Set as home page (control h)", (x, y) => BrowserManager.Instance.SetHomePage());
            menu.MenuItems.Add("Add to favourites (control f)", (x, y) => BrowserManager.Instance.Favourites.AddToFavourites(new FavouriteEntry(localHistory.GetCurrentPage().GetUrl(), localHistory.GetCurrentPage())));
            webDisplay.ContextMenu = menu;
        }


        /// <summary>
        /// Performes the work of the Background worker.
        /// Given a webpage, this method will perform a http request in a new tab, and return a string of HTML, or an error status code
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        public void SearchInNewThread(object sender, DoWorkEventArgs e) {

            if (backgroundWorker.CancellationPending) { //if we have requested to cancel this worker, cancel.
                e.Cancel = true;
                return;
            } 

            UseWaitCursor = true; //set the cursor to loading

            Webpage requestedPage = (Webpage)e.Argument; //get the requested webpage from the arguments
            string requestedUrl = CorrectUrl(requestedPage.GetUrl()); //store the corrected url
            int statusCode = 0;
            string statusText = "";

            try { //attempt to retreive a HTTP response from the requested url
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestedUrl); //perform the http request
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse()) { //get the response from the request

                    if (HttpStatusCode.OK == response.StatusCode) { //successfully retreived http request
                        using (Stream dataStream = response.GetResponseStream()) {
                            using (StreamReader reader = new StreamReader(dataStream)) { //read the response
                                string result = reader.ReadToEnd();
                                e.Result = result; //store the result for use in RunWorkerCompletedEventArgs
                                //search for a page title
                                Match match = Regex.Match(result, @"<title>([^<]+)", RegexOptions.IgnoreCase);
                                string pageTitle = requestedUrl;
                                if (match.Success) {
                                    pageTitle = match.Groups[1].Value;
                                }

                                this.Invoke((MethodInvoker)delegate { // runs on UI thread
                                    statusCode = (int)response.StatusCode; //get the status code number
                                    statusText = response.StatusCode.ToString(); //get the status code description
                                    string fullStatusDescription = statusCode.ToString() + " " + statusText + " " + pageTitle;
                                    BrowserManager.Instance.Window.Text = fullStatusDescription;
                                    this.Text = pageTitle; //update the title of this tab to the tab page
                                });
                            }
                        }
                    }
                }

            } catch (WebException exception) { //an error occured
                using (WebResponse response = exception.Response) { // read the http response
                    using (HttpWebResponse httpResponse = (HttpWebResponse)response) {
                        this.Invoke((MethodInvoker)delegate {
                            if (httpResponse != null) {
                                statusCode = (int)httpResponse.StatusCode; //get the status code number
                                statusText = httpResponse.StatusCode.ToString(); //get the status code description
                                string fullErrorDescription = statusCode.ToString() + " " + statusText;
                                BrowserManager.Instance.Window.Text = fullErrorDescription; // runs on UI thread
                                this.Text = requestedUrl; //update the title of this tab to the requested url
                            }
                            else { //condition used is no http response was returned at all
                                BrowserManager.Instance.Window.Text = "Could not be found";
                                this.Text = "Could not be found";
                            }
                        });
                        if (response != null) { //store the response for use in RunWorkerCompletedEventArgs
                            using (Stream data = response.GetResponseStream()) {
                                //return the status text + any response HTML
                                e.Result = statusText + "\n " + new StreamReader(data).ReadToEnd();
                            }
                        }
                        else { //no http response was found
                            e.Result = "";
                        }
                    }
                }
            } catch (UriFormatException exception) {
                MessageBox.Show("Unknown URI Format: " + exception);
            }
        }

        /// <summary>
        /// Processes the result of our background worker once it completes
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        public void SearchResultReturned(object sender, RunWorkerCompletedEventArgs e) {
            UseWaitCursor = false; //stop using the waiting cursor
            if (e.Result != null) { //if we got a result, display the html
                currentHtml = (string)e.Result;
                if (showingHtml) { //if we are currently showing a rendered website, add result to htmlDisplay
                    htmlDisplay.Text = currentHtml;
                }
                else { //we are currently showing html code, add result to webDisplay
                    webDisplay.Text = currentHtml;
                } 
            }
        }

        /// <summary>
        /// Takes an input and appends http:// if the string does not start with
        ///  either http:// or https://
        /// </summary>
        /// <param name="input">The string that will be used as the url for a http request</param>
        /// <returns>A string that starts with http://</returns>
        public string CorrectUrl(string input) {
            if (input.StartsWith("http://") || input.StartsWith("https://")) {
                return input;
            }
            else {
                return "http://" + input;
            }
        }
    }
}
