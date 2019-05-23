using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace WebBrowser {
    /// <summary>
    /// The BrowserManager class uses a singleton pattern. This object allows common
    /// functions to be accessible from all other classes without needing to pass object references around
    /// as paramaters.
    /// </summary>
    class BrowserManager {
        private static BrowserManager instance = new BrowserManager();
        private BrowserWindow window;
        private Tab currentTab;
        private GlobalHistory globalHistory;
        private Favourites favourites;

        /// <summary>
        /// Property to access the instance of this object
        /// </summary>
        public static BrowserManager Instance {
            get {
                return instance;
            }
        }

        /// <summary>
        /// Property to access the global history object
        /// </summary>
        public GlobalHistory History {
            get {
                return globalHistory;
            }
        }

        /// <summary>
        /// Property to access the favourites object
        /// </summary>
        public Favourites Favourites {
            get {
                return favourites;
            }
        }

        /// <summary>
        /// Property to get and set the window object. This should be used to get or set
        /// the reference to the main browser window.
        /// </summary>
        public BrowserWindow Window {
            get {
                return window;
            }
            set {
                window = value;
            }
        }

        /// <summary>
        /// Property to get and set the currently selected tab
        /// </summary>
        public Tab CurrentTab {
            get {
                return currentTab;
            }
            set {
                currentTab = value;
            }
        }

        /// <summary>
        /// Constructor: Creates a GlobalHistory and Favourites object, which are used
        /// by the entire web browser to store history and favourite web pages.
        /// Also makes a call to load past history and favourites from file as a multithreaded operation
        /// </summary>
        public BrowserManager() {
            globalHistory = new GlobalHistory();
            favourites = new Favourites();

            ReadFilesMultithreaded();
        }

        /// <summary>
        /// Creates and displays a prompt, which allows a user
        /// to input a url and save it as their homepage.
        /// </summary>
        public void SetHomePage() {

            //create panels
            Form prompt = new Form() {
                Width = 500,
                Height = 100,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Set Homepage",
                MinimizeBox = false,
                MaximizeBox = false,
                StartPosition = FormStartPosition.CenterScreen
            };
            Panel urlPanel = new Panel() {
                Dock = DockStyle.Top,
                Height = 25
            };
            Label urlLabel = new Label() {
                Text = "Url:",
                Dock = DockStyle.Left
            };
            TextBox urlEntry = new TextBox() {
                Text = currentTab.GetLocalTabHistory().GetCurrentPage().GetUrl(),
                Dock = DockStyle.Right,
                Width = 350
            };
            Button confirmation = new Button() {
                Text = "Save",
                DialogResult = DialogResult.OK,
                Dock = DockStyle.Bottom,
            };
            //add event listener to save button, to save the inputted url as our homepage
            confirmation.Click += (sender, e) => {
                Properties.Settings.Default.homepage = urlEntry.Text; //save the homepage url
                Properties.Settings.Default.Save();
                prompt.Close(); //close the prompt window
            };

            //add controls
            urlPanel.Controls.Add(urlEntry);
            urlPanel.Controls.Add(urlLabel);
            prompt.Controls.Add(urlPanel);
            prompt.Controls.Add(confirmation);

            prompt.AcceptButton = confirmation; //set prompt accept button to our new button
            prompt.Show(); //show the prompt
        }

        /// <summary>
        /// Writes content to a file in a new thread
        /// </summary>
        /// <param name="content">The string to be written to the file</param>
        /// <param name="filePath">The path of the file to be written to</param>
        public void WriteToFileInThread(string content, string filePath) {

            Thread newThread = new Thread(() => { //uses a lambda expression to write to a file in a new thread
                using (StreamWriter writer = new StreamWriter(filePath)) {
                    writer.WriteLine(content);
                }
             });
            
            newThread.Start(); //run the new thread to write to the file.
        }


        /// <summary>
        /// Creates and runs two threads that read the favourites and history files
        /// in seperate threads. This method waits for both threads to complete to ensure all required files
        /// are loaded before the user can interact with the web browser.
        /// </summary>
        public void ReadFilesMultithreaded() {

            Thread favThread = new Thread(new ThreadStart(favourites.LoadFavourites));
            Thread historyThread = new Thread(new ThreadStart(globalHistory.LoadHistory));

            //run the two threads
            favThread.Start();
            historyThread.Start();

            //Wait for both threads to complete before continuing the main thread. This ensures both
            //files will be loaded before the user can interact with the web browser.
            favThread.Join();
            historyThread.Join();
        }
    }
}
