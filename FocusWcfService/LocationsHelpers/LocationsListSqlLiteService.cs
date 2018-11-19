using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using Focus.Core.LocationsHelpers;
using FocusWcfService.Common;
using FocusWcfService.Dtos;
using FocusWcfService.Models;
using NLog;

namespace FocusWcfService.LocationsHelpers {
    public class LocationsListSqlLiteService : ILocationsListSqlLiteService {
        private readonly ILogger logger = LogManager.GetCurrentClassLogger();

        private readonly IRepository<WatchedLocation> repository;

        public LocationsListSqlLiteService(IRepository<WatchedLocation> repository) {
            this.repository = repository;
        }

        public void AddWatchedLocationAndFile(string locationPath, string fileName, WatchedLocationActionType actionType) {
            try {
                var watchedLocation = new WatchedLocation {
                    Id = Guid.NewGuid(),
                    Path = locationPath,
                    FileName = fileName,
                    ActionType = actionType
                };

                this.repository.Save(watchedLocation);
            }
            catch (Exception e) {
                this.logger.Debug($"Failed to add new watched location! Path {locationPath} - file name {fileName}. {e}");
                throw;
            }
        }

        public void RemoveWatchedLocationAndFile(string locationPath, string fileName) {
            try {
                var watchedLocation = this.repository.Filter<WatchedLocation>(x => x.Path == locationPath && x.FileName == fileName).FirstOrDefault();

                if (watchedLocation == null) {
                    throw new ObjectNotFoundException($"Could not find watched location with location path {locationPath} and file name {fileName}.");
                }

                this.repository.Delete(watchedLocation);
            }
            catch (Exception e) {
                this.logger.Debug($"Failed to remove watched location! Path {locationPath} - file name {fileName}. {e}");
                throw;
            }
        }

        public IEnumerable<WatchedLocationDto> GetAllWatchedLocations() {
            var watchedLocations = this.repository.GetAll().ToList();
            this.logger.Debug($"Found {watchedLocations.Count} watched location(s)");
            watchedLocations.ForEach(x => {
                                         this.logger.Debug($"\t{x.Path} - {x.FileName} - {x.ActionType}");
                                     });

            return this.MapWatchedLocationsToDtos(watchedLocations);
        }

        private IEnumerable<WatchedLocationDto> MapWatchedLocationsToDtos(List<WatchedLocation> watchedLocations) {
            return watchedLocations.Select(x => new WatchedLocationDto {
                Id = x.Id,
                LocationPath = x.Path,
                FileName = x.FileName,
                ActionType = x.ActionType
            });
        }
    }
}