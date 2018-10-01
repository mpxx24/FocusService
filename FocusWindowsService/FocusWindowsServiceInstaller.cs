using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FocusWindowsService {
    [RunInstaller(true)]
    public class FocusWindowsServiceInstaller : Installer {
        public FocusWindowsServiceInstaller() {
            var processInstaller = new ServiceProcessInstaller {
                Account = ServiceAccount.LocalService
            };

            var serviceInstaller = new ServiceInstaller {
                ServiceName = nameof(FocusHostService),
                Description = "Hosting for FocusWcfService",
                DelayedAutoStart = true
            };

            this.Installers.Add(processInstaller);
            this.Installers.Add(serviceInstaller);
        }
    }
}