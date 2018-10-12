using System;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListSqlLiteService : IProcessesListService {
        bool? IsProcessWithTheSameNameAlreadyWatched(string processName);
        void SetProcessAsCurrentlyWatched(string processName, bool isCurrentlyWatched);
        void ChangeAllowedTimeForWatchedProcess(string processName, TimeSpan allowedTime);
    }
}