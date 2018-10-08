namespace FocusWindowsService.FilesHelpers {
    public interface IFilesWatcherService {
        void StartFilesWatcher(string locationToWatch);

        void StartWatchingAllDefinedLocations();
    }
}