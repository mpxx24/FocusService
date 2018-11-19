using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FocusWcfService.ProcessesHelpers {
    public static class ProcessesHelper {
        public static bool KillProcess(string name) {
            try {
                if (string.IsNullOrEmpty(name)) {
                    return false;
                }

                var processess = Process.GetProcesses().Where(x => x.ProcessName == name);

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