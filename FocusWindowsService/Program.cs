using System;
using System.ServiceProcess;
using Autofac;
using FocusWcfService;
using FocusWcfService.ProcessesHelpers;
using NLog;

namespace FocusWindowsService {
    internal static class Program {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main() {
            //Debugger.Launch();
            try {
                logger.Debug("Initializing container!");
                IoC.Initialize(new Module[] { new ServiceModule() });

                var processesListSqlLiteService = IoC.Resolve<IProcessesListSqlLiteService>();
                var processesOperationsService = IoC.Resolve<IProcessesOperationsService>();

                var servicesToRun = new ServiceBase[] {
                    new FocusHostService(processesListSqlLiteService, processesOperationsService)
                };
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception e) {
                logger.Debug($"Exception occured during windows service initialization! {e}");
            }
        }
    }
}