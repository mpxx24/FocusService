using System;
using System.ServiceProcess;
using Autofac;
using FocusWcfService;
using FocusWcfService.ProcessesHelpers;
using NLog;

namespace FocusWindowsService {
    internal static class Program {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main() {
            try {
                logger.Debug("Initializing container.");
                IoC.Initialize(new Module[] { new ServiceModule() });
                logger.Debug("Container succesfully initialized.");

                var processesListSqlLiteService = IoC.Resolve<IProcessesListSqlLiteService>();
                var processesOperationsService = IoC.Resolve<IProcessesOperationsService>();
                var locationsOperationsService = IoC.Resolve<ILocationsOperationsService>();

                var servicesToRun = new ServiceBase[] {
                    new FocusHostService(processesListSqlLiteService, processesOperationsService, locationsOperationsService)
                };
                ServiceBase.Run(servicesToRun);
            }
            catch (Exception e) {
                logger.Debug($"Exception occured during windows service initialization! {e}");
            }
        }
    }
}