using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Xml.Linq;

namespace WebBrowser {
    /// <summary>
    /// Stores favourite webpages. Allows for persistent favourites between sessions by saving and loading favourites in XML files
    /// </summary>
    class Favourites {

        private List<FavouriteEntry> favourites;
        private static string favouritesFilePath = "favourites.xml"; //path to favourites file.
        //locks to ensure the favourites file is not written or read by more than one thread at a time
        private readonly object fileLock = new object();

        /// <summary>
        /// Constructor to create a new favourites list
        /// </summary>
        public Favourites() {
            favourites = new List<FavouriteEntry>();
        }

        /// <summary>
        /// Removes the specified FavouriteEntry from the favourites list
        /// </summary>
        /// <param name="favourite">The FavouriteEntry to remove</param>
        public void RemoveFromFavourites(FavouriteEntry favourite) {
            Contract.Requires(favourite != null); //code contract to ensure that the favourite entry exists

            favourites.Remove(favourite);
            SaveFavourites();
        }

        /// <summary>
        /// Inserts the specified FavouriteEntry into the favourites list
        /// </summary>
        /// <param name="favourite">The FavouriteEntry to add</param>
        public void AddToFavourites(FavouriteEntry favourite) {
            Contract.Requires(favourite != null); //code contract to ensure that the favourite entry exists

            favourites.Add(favourite);
            SaveFavourites();
        }

        /// <summary>
        /// Returns the list of favourite entries
        /// </summary>
        /// <returns>A list of type FavouriteEntry</returns>
        public List<FavouriteEntry> GetFavourites() {
            return favourites;
        }

        /// <summary>
        /// Formats a XML file, and saves each FavouriteEntry into an xml file.
        /// This is performed in a new thread.
        /// </summary>
        public void SaveFavourites() {

            string favouritesXml = "<favourites>";
            foreach (FavouriteEntry fav in favourites) {
                favouritesXml += "\n" + fav.ToString(); //fav.ToString() returns a webpage in xml format
            }
            favouritesXml += "</favourites>";
            lock (fileLock) { //lock to ensure the favourites file is not written to by more than one thread at a time
                BrowserManager.Instance.WriteToFileInThread(favouritesXml, favouritesFilePath); //writes to favourites.xml in a new thread
            }
        }

        /// <summary>
        /// Loads favourites from an xml file and adds them to the favourites list.
        /// </summary>
        public void LoadFavourites() {

            lock (fileLock) { //lock to ensure file is only read by a single thread at a time

                XDocument favouritesXml = null;

                try { //get favourites.xml
                    favouritesXml = XDocument.Load(favouritesFilePath);
                }
                catch (Exception e) {
                    Console.WriteLine("Could not load file {0} reason: {1}", favouritesFilePath, e);
                }

                if (favouritesXml != null) { //if favourites.xml exists
                    foreach (XElement favouritesEntry in favouritesXml.Root.Elements()) { //add each favourite entry into the favourites list
                        string name = favouritesEntry.Attribute("name").Value;
                        string url = favouritesEntry.Attribute("url").Value;
                        favourites.Add(new FavouriteEntry(name, new Webpage(url)));
                    }
                }
            }
        }
    }
}
