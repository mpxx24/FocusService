using System.IO;

namespace FocusWindowsService.FilesHelpers {
    public class FilesWatcherService : IFilesWatcherService {
        public void StartFilesWatcher(string locationToWatch) {
            var watcher = new FileSystemWatcher();
            watcher.Path = locationToWatch;
            watcher.Created += this.OnFileCreated;
            watcher.Filter = "*.*";
            watcher.EnableRaisingEvents = true;
        }

        private void OnFileCreated(object sender, FileSystemEventArgs e) {
            //process watched location
        }
    }
}