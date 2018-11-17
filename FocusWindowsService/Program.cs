using System.ServiceProcess;
using Autofac;
using FocusWcfService;
using FocusWcfService.ProcessesHelpers;

namespace FocusWindowsService {
    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main() {
            //Debugger.Launch();
            IoC.Initialize(new Module[] {new ServiceModule()});
            var processesListSqlLiteService = IoC.Resolve<IProcessesListSqlLiteService>();
            var processesOperationsService = IoC.Resolve<IProcessesOperationsService>();

            var servicesToRun = new ServiceBase[] {
                new FocusHostService(processesListSqlLiteService, processesOperationsService)
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}