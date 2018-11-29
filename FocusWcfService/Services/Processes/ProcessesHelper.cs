using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using NLog;

namespace FocusWcfService.Services.Processes {
    public static class ProcessesHelper {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public static bool KillProcess(string name) {
            try {
                if (string.IsNullOrEmpty(name)) {
                    return false;
                }

                var processess = Process.GetProcesses().Where(x => x.ProcessName == name);

                logger.Debug($"Killing process {name}.");
                foreach (var process in processess) {
                    process.Kill();
                }

                return true;
            }
            catch (Exception) {
                return false;
            }
        }

        public static bool IsProcessRunning(string name) {
            try {
                return !string.IsNullOrEmpty(name) && Process.GetProcesses().Any(x => x.ProcessName == name);
            }
            catch (Exception) {
                return false;
            }
        }

        public static string GetRealProcessName(string name) {
            return Path.GetFileNameWithoutExtension(name);
        }
    }
}