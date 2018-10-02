﻿using System.Diagnostics;
using System.ServiceProcess;
using Autofac;
using FocusWcfService;
using FocusWcfService.Common;
using FocusWcfService.ProcessesHelpers;

namespace FocusWindowsService {
    internal static class Program {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        private static void Main() {
            Debugger.Launch();
            IoC.Initialize(new Module[] {new ServiceModule()});
            var processesListSqlLiteService = IoC.Resolve<IProcessesListSqlLiteService>();
            var processesOperationsService = IoC.Resolve<IProcessesOperationsService>();
            var cache = IoC.Resolve<IWatchedProcessesCache>();

            var servicesToRun = new ServiceBase[] {
                new FocusHostService(processesListSqlLiteService, processesOperationsService, cache)
            };
            ServiceBase.Run(servicesToRun);
        }
    }
}