using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using FocusWcfService.Models;

namespace FocusWcfService.ProcessesHelpers {
    [Obsolete]
    public class ProcessesListFileService : IProcessesListFileService {
        private static readonly string pathToFileWithWatchedProcesses = @"D:\FocusService\";
        private static readonly string fileName = "watchedProcessess.xml";
        private static readonly string fullPathToProcessesListFile = Path.Combine(pathToFileWithWatchedProcesses, fileName);
        private static readonly string parentNodeNameString = "WatchedProcesses";
        private static readonly string processNodeNameString = "Process";
        private static readonly string nameElementString = "Name";
        private static readonly string allowedTimeElementString = "AllowedTime";
        private static readonly string valueAttributeString = "value";
        private static XContainer document;

        private static readonly object locker = new object();

        public void CreateFile() {
            if (!Directory.Exists(pathToFileWithWatchedProcesses)) {
                Directory.CreateDirectory(pathToFileWithWatchedProcesses);
            }

            if (File.Exists(fullPathToProcessesListFile)) {
                return;
            }

            var processesListFile = new XDocument(
                new XElement(parentNodeNameString)
            );

            processesListFile.Save(fullPathToProcessesListFile);
        }

        public WatchedProcess GetProcess(string name) {
            return new WatchedProcess();
        }

        public IEnumerable<WatchedProcess> GetCurrentlyWatchedProcesses() {
            if (!File.Exists(fullPathToProcessesListFile)) {
                throw new InvalidOperationException($"Can't get list of watched processes because file [{fullPathToProcessesListFile}] does not exist!");
            }

            var doc = XDocument.Load(fullPathToProcessesListFile);

            var processes =  doc.Element(parentNodeNameString)?.Descendants().Where(x => x.Name == "Name")?.Select(y => y.Attribute("value")?.Value);
            return processes?.Select(x => new WatchedProcess {Name = x});
        }

        public void AddWatchedProcess(WatchedProcess process) {
            if (!File.Exists(fullPathToProcessesListFile)) {
                throw new InvalidOperationException($"Can't add the watched process because file [{fullPathToProcessesListFile}] does not exist!");
            }

            lock (locker) {
                var doc = XDocument.Load(fullPathToProcessesListFile);
                var isProcessAlreadyWatched = this.IsProcessWithTheSameNameAlreadyWatched(doc, process.Name);
                if (isProcessAlreadyWatched.HasValue && !isProcessAlreadyWatched.Value) {
                    var newElement = new XElement(processNodeNameString);

                    newElement.Add(new XElement(nameElementString, new XAttribute(valueAttributeString, process.Name)),
                                   new XElement(allowedTimeElementString, new XAttribute(valueAttributeString, process.TimeAllowedPerDay.ToString("g"))));

                    doc.Element(parentNodeNameString)?.Add(newElement);

                    doc.Save(fullPathToProcessesListFile);
                }
            }
        }

        public void RemoveWatchedProcess(WatchedProcess process) {
            if (!File.Exists(fullPathToProcessesListFile)) {
                throw new InvalidOperationException($"Can't add the watched process because file [{fullPathToProcessesListFile}] does not exist!");
            }

            var doc = XDocument.Load(fullPathToProcessesListFile);
            var root = doc.Element(parentNodeNameString);

            if (root == null) {
                throw new InvalidOperationException("Could not find root element for watched processes!");
            }

            doc.Element(parentNodeNameString)?.Elements(processNodeNameString).ToList().ForEach(
                x => {
                    var nameElement = x.Element(nameElementString);
                    if (nameElement != null && nameElement.HasAttributes && nameElement.Attribute(valueAttributeString)?.Value == process.Name) {
                        x.Remove();
                    }
                }
            );

            doc.Save(fullPathToProcessesListFile);
        }

        public void UpdateTimeForWatchedProcess(WatchedProcess process) {
            if (!File.Exists(fullPathToProcessesListFile)) {
                throw new InvalidOperationException($"Can't update the watched process because file [{fullPathToProcessesListFile}] does not exist!");
            }

            var doc = XDocument.Load(fullPathToProcessesListFile);
            var root = doc.Element(parentNodeNameString);

            if (root == null) {
                throw new InvalidOperationException("Could not find root element for watched processes!");
            }

            doc.Element(parentNodeNameString)?.Elements(processNodeNameString)
               .Where(p => p.Descendants().Any(x => x.HasAttributes && x.Attribute(valueAttributeString)?.Value == process.Name)).ToList().ForEach(
                   x => {
                       var allowedTimeElement = x.Element(allowedTimeElementString);
                       if (allowedTimeElement != null && allowedTimeElement.HasAttributes && allowedTimeElement.Attribute(valueAttributeString) != null && TimeSpan.TryParse(allowedTimeElement.Attribute(valueAttributeString)?.Value, out var time)) {
                           if (time == new TimeSpan()) {
                               if (ProcessesHelper.IsProcessRunning(process.Name)) {
                                   ProcessesHelper.KillProcess(process.Name);
                               }
                           }
                           else {
                               var newTime = time.Add(TimeSpan.FromMinutes(-1));

                               allowedTimeElement.Attribute(valueAttributeString).Value = newTime.ToString("g");
                           }
                       }
                   }
               );

            doc.Save(fullPathToProcessesListFile);
        }

        public bool? IsProcessWithTheSameNameAlreadyWatched(XContainer doc, string processName) {
            return doc.Element(parentNodeNameString)?.Descendants()
                      .Any(x => x.HasAttributes && x.Attribute(valueAttributeString)?.Value == processName);
        }
    }
}