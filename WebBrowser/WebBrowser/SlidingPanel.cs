using System;
using System.Drawing;
using System.Windows.Forms;

namespace WebBrowser {
    /// <summary>
    /// A sliding panel control that extends the Panel class. A SlidingPanel object creates
    /// a panel that can slide in and out from the right side of a form.
    /// </summary>
    class SlidingPanel : Panel {

        private bool showingSidePanel; //indicates if this panel is opened or closed.
        private int maxWidth = 214; //the width of the panel when it is fully opened.
        private Timer timer;

        /// <summary>
        /// Constructor to create a new sliding panel control
        /// </summary>
        public SlidingPanel() {
            timer = new Timer(); //creates a new timer, which is used to open and close this panel
            timer.Tick += new EventHandler(RunTimer); //add a callback to the new timer's tick event
            timer.Interval = 10;
            Width = 0; //start with the panel closed
            AutoScroll = true;
            Dock = DockStyle.Right;
            Anchor = (AnchorStyles.Top | AnchorStyles.Left);
            BringToFront(); //ensure that this panel is displayed above all other controld
            BackColor = Color.AliceBlue;
        }

        /// <summary>
        /// Property that returns a boolean indicating if this panel is opened or closed
        /// </summary>
        public bool IsOpen {
            get {
                return showingSidePanel;
            }
        }

        /// <summary>
        /// Opens this panel if it is closed, or closes it if it is open.
        /// </summary>
        public void TogglePanel() {
            timer.Start();
        }

        /// <summary>
        /// Opens this panel if it is not already open
        /// </summary>
        public void OpenPanel() {
            if (!showingSidePanel) {
                timer.Start();
            }
        }

        /// <summary>
        /// Closes this panel if it is not already closed.
        /// </summary>
        public void ClosePanel() {
            if (showingSidePanel) {
                timer.Start();
            }
        }

        /// <summary>
        /// Called on the timers tick even. This function will change the width
        /// of the side panel to make it grow or shrink from the right side of the screen.
        /// </summary>
        /// <param name="sender">The object that triggered the event</param>
        /// <param name="e">Event specific arguments</param>
        private void RunTimer(object sender, EventArgs e) {

            if (!showingSidePanel) { //open the side panel
                Width = Width + 50;
                if (Width > maxWidth) { //increase the side panel until the width matches the maxWidth
                    timer.Stop();
                    showingSidePanel = true;
                }
            }
            else { //close the side panel
                Width = Width - 50;
                if (Width <= 0) { //shrink the panel until its width is 0
                    timer.Stop();
                    showingSidePanel = false;
                    Controls.Clear(); //remove all child controls.
                }
            }
        }
    }
}
