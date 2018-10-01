using System.Xml.Linq;

namespace FocusWcfService.ProcessesHelpers {
    public interface IProcessesListFileService : IProcessesListService {
        void CreateFile();

        bool? IsProcessWithTheSameNameAlreadyWatched(XContainer doc, string processName);
    }
}