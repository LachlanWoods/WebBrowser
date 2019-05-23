using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace WebBrowser {
    /// <summary>
    /// Stores the history of the web browser. Allows for persistent history by saving and loading history in XML files
    /// </summary>
    class GlobalHistory {

        private List<HistoryEntry> history;
        private static string historyFilePath = "history.xml"; //path to history file.
        //locks to ensure the history file is not written or read by more than one thread at a time
        private readonly object fileLock = new object();

        /// <summary>
        /// Constructor which creates a new list of type HistoryEntry
        /// </summary>
        public GlobalHistory() {
            history = new List<HistoryEntry>();
        }

        /// <summary>
        /// Removes a HistoryEntry from the history list and saves the history to file
        /// </summary>
        /// <param name="page">The HistoryEntry to remove from history</param>
        public void RemoveFromHistory(HistoryEntry page) {
            Contract.Requires(page != null); //code contract to ensure that the history entry exists

            history.Remove(page);
            SaveHistory();
        }

        /// <summary>
        /// Adds a webpage to history and saves the history to file
        /// </summary>
        /// <param name="page">The webpage to add to history</param>
        public void AddToHistory(Webpage page) {
            Contract.Requires(page != null); //code contract to ensure that the history entry exists

            history.Add(new HistoryEntry(page));
            SaveHistory();
        }

        /// <summary>
        /// Returns the history list
        /// </summary>
        /// <returns>The global history list</returns>
        public List<HistoryEntry> GetHistory() {
            return history;
        }

        /// <summary>
        /// Writes the history list to an XML file.
        /// This is performed in a new thread.
        /// </summary>
        public void SaveHistory() {

            string historyXml = "<history>";
            foreach (HistoryEntry entry in history) {
                historyXml += "\n" + entry.GetPage().ToString(); // entry.GetPage().ToString() returns a string in xml format
            }
            historyXml += "</history>";

            lock (fileLock) { //lock to ensure the history file is not written to by more than one thread at a time
                BrowserManager.Instance.WriteToFileInThread(historyXml, historyFilePath); //writes to history.xml in a new thread
            }
        }

        /// <summary>
        /// Loads past history from history.xml, and stores each entry
        /// in the history list.
        /// </summary>
        public void LoadHistory() {
            lock (fileLock) { //lock to ensure file is only read by a single thread at a time

                XDocument historyXml = null;

                try { //get history.xml
                    historyXml = XDocument.Load(historyFilePath);
                }
                catch (Exception e) {
                    Console.WriteLine("Could not load file {0} reason: {1}", historyFilePath, e);
                }

                if (historyXml != null) { //if historyXml exists
                    foreach (XElement historyEntry in historyXml.Root.Elements()) { //add each history entry to the history list
                        string url = historyEntry.Attribute("url").Value;
                        DateTime visitDate = DateTime.Parse(historyEntry.Attribute("date").Value);
                        history.Add(new HistoryEntry(new Webpage(url, visitDate)));
                    }
                }
            }
        }
    }
}
