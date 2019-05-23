using System;

namespace WebBrowser {
    /// <summary>
    /// Object that represents a webpage. Stores the url and access date of a web page.
    /// </summary>
    class Webpage {
        private string url;
        private DateTime accessDate;

        /// <summary>
        /// Constructor which creates a new Webpage object given a url and sets the access time
        /// to the current time
        /// </summary>
        /// <param name="url">The url of the webpage</param>
        public Webpage(string url) {
            this.url = url;
            accessDate = DateTime.Now;
        }

        /// <summary>
        /// overload constructor which takes an access date.
        /// used for creating a webpage when loading history
        /// </summary>
        /// <param name="url">The url of the webpage</param>
        /// <param name="accessDate">The date this page was originally accessed</param>
        public Webpage(string url, DateTime accessDate) {
            this.url = url;
            this.accessDate = accessDate;
        }

        /// <summary>
        /// Returns the url of this webpage
        /// </summary>
        /// <returns>The url of this webpage as a string</returns>
        public string GetUrl() {
            return url;
        }

        /// <summary>
        /// Returns the time this page was accessed
        /// </summary>
        /// <returns>A DateTime object detailing when this webpage was accessed</returns>
        public DateTime GetAccessDate() {
            return accessDate;
        }

        //override ToString to return a json object
        public override string ToString() {
            return string.Format("<historyEntry url=\"{0}\" date =\"{1}\"></historyEntry>", url, accessDate.ToString());
        }

    }
}
