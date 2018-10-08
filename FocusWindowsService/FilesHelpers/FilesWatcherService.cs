using System.IO;
using FocusWcfService.Common;
using FocusWcfService.Models;

namespace FocusWindowsService.FilesHelpers {
    public class FilesWatcherService : IFilesWatcherService {
        private readonly IRepository<WatchedLocation> repository;

        public FilesWatcherService(IRepository<WatchedLocation> repository) {
            this.repository = repository;
        }

        public void StartFilesWatcher(string locationToWatch) {
            var watcher = new FileSystemWatcher();
            watcher.Path = locationToWatch;
            watcher.Created += this.OnFileCreated;
            watcher.Filter = "*.*";
            watcher.EnableRaisingEvents = true;
        }

        public void StartWatchingAllDefinedLocations() {
            var locations = this.repository.GetAll();

            foreach (var watchedLocation in locations) {
                this.StartWatchingLocations(watchedLocation);
            }
        }

        private void StartWatchingLocations(WatchedLocation location) {
            //start watching location
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e) {
            //process watched location
        }
    }
}