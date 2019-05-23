using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace WebBrowser {
    /// <summary>
    /// Stores the local history of a single tab. This class allows a tab to navigate
    /// back and forwards between previously visted webpages
    /// </summary>
    class TabHistory {
        LinkedList<Webpage> history;
        LinkedListNode<Webpage> currentPage; //linked list node corresponding to the currently displayed page

        /// <summary>
        /// Constructor which creates a new LinkedList of webpages.
        /// </summary>
        public TabHistory() {
            history = new LinkedList<Webpage>();
        }

        /// <summary>
        /// Returns the current page that is being displayed by the parent tab.
        /// </summary>
        /// <returns>The Webpage object of the current page being displayed</returns>
        public Webpage GetCurrentPage() {

            Contract.Requires(currentPage != null); //contract to ensure that the current webpage exists

            if (currentPage == null) {
                return null;
            }
            return currentPage.Value; //return the currently viewed page, held by the currentPage node.
        }

        /// <summary>
        /// If possible, returns the webpage that was viewed before the current webpage.
        /// </summary>
        /// <returns>The Webpage object of the previously visited page, or null if no webpage was visited before the current page</returns>
        public Webpage GetBackPage() {
            if (currentPage.Previous != null) { //if we visited a page before the current page
                Webpage backPage = currentPage.Previous.Value; //get the node before the current page in the linked list
                currentPage = currentPage.Previous; //move current page back one in the linked list
                return backPage;
            }
            return null;
        }

        /// <summary>
        /// If possible, returns the webpage that was viewed after the current webpage.
        /// </summary>
        /// <returns>The Webpage object of the page visited page after the current page, or null if no webpage was visited after the current page</returns>
        public Webpage GetForwardPage() {
            if (currentPage.Next != null) { //if we visited a page after the current page
                Webpage nextPage = currentPage.Next.Value; //get the node after the current page in the linked list
                currentPage = currentPage.Next; //move current page forward one in the linked list
                return nextPage;
            }
            return null;
        }

        /// <summary>
        /// Add a webpage to a tabs local history
        /// </summary>
        /// <param name="page">The Webpage object to add to a tabs local history</param>
        public void AddToHistory(Webpage page) {
            Contract.Requires(page != null); //code contract to ensure that the webpage exists

            LinkedListNode<Webpage> newEntry;

            if (currentPage == null) { //if this is the first page visited by the tab
                newEntry = history.AddFirst(page);
            }
            else {
                //clear forward history if we search for a new page
                if (currentPage.Next != null) {
                    history.Remove(currentPage.Next);
                }
                newEntry = history.AddAfter(currentPage, page); //add the new page after the current page in history
            }
            currentPage = newEntry; //set the current page as the new page
            UpdateNavButtons(); //update the back and forward buttons to make them interactible, if they were previously not interactible
        }


        /// <summary>
        /// Sets the back and forward buttons interaction value based on whether or not a user
        /// can go back and forwards between pages.
        /// </summary>
        public void UpdateNavButtons() {

            Contract.Requires(BrowserManager.Instance.Window != null); //contract to ensure that the BrowserManager singleton has been created

            if (BrowserManager.Instance.Window == null) { //return if the BrowserManager singleton has not been created
                return;
            }

            if (currentPage == null) { //disable back and forward buttons if no currentPage exists.
                BrowserManager.Instance.Window.GetBackButton().Enabled = false;
                BrowserManager.Instance.Window.GetForwardButton().Enabled = false;
                return;
            }

            if (currentPage.Previous != null) { // enable the back button if there is a previous page in history
                BrowserManager.Instance.Window.GetBackButton().Enabled = true;
            }
            else { //disable the back button if there isnt a previous page in history
                BrowserManager.Instance.Window.GetBackButton().Enabled = false;
            }

            if (currentPage.Next != null) { // enable the forward button if there is a next page in history
                BrowserManager.Instance.Window.GetForwardButton().Enabled = true;
            }
            else { // disable the forward button if there isnt a next page in history
                BrowserManager.Instance.Window.GetForwardButton().Enabled = false;
            }
        }
    }
}
