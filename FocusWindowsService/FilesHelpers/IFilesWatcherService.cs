namespace FocusWindowsService.FilesHelpers {
    public interface IFilesWatcherService {
        void StartFilesWatcher(string locationToWatch);
    }
}