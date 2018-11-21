using System;
using System.IO;
using System.Threading;
using Focus.Core.LocationsHelpers;
using FocusWcfService.Dtos;
using NLog;

namespace FocusWindowsService.FilesHelpers {
    public class FilesWatcherHelper {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static void StartFilesWatcher(WatchedLocationDto locationToWatch) {
            var watcher = new FileSystemWatcher();
            watcher.Path = locationToWatch.LocationPath;
            watcher.Created += (sender, e) => OnFileCreated(e, locationToWatch.ActionType);

            watcher.Filter = $"{Path.GetFileNameWithoutExtension(locationToWatch.FileName)}*";
            watcher.EnableRaisingEvents = true;
        }

        private static void OnFileCreated(FileSystemEventArgs e, WatchedLocationActionType actionType) {
            try {
                //TODO: fix this
                //NOTE: this (sometimes) will not work when downloading the same file right after it was deleted
                //at least for ~70MB file
                //File.Delete() sadly just marks file to be deleted and calling File.Exists() right after File.Delete() will return true (at least when e.g. downloading file in the browser)
                if (!File.Exists(e.FullPath)) {
                    //HACKS, ALL THE WAY
                    logger.Debug($"Waiting for file {e.FullPath} to be creeated!");
                    Thread.Sleep(1000);
                    OnFileCreated(e, actionType);
                }

                switch (actionType) {
                    case WatchedLocationActionType.Nothing:
                        break;
                    case WatchedLocationActionType.Delete:
                        if(File.Exists(e.FullPath)) {
                            logger.Debug($"File {e.FullPath} created! Removing the file.");
                            File.Delete(e.FullPath);
                            logger.Debug($"File {e.FullPath} removed!");
                        }
                        break;
                }
            }
            catch (Exception exc) {
                logger.Debug($"Exception occured in the OnFileCreated event! {exc}");
            }
        }
    }
}