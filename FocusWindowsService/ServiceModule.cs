using Autofac;
using FocusWcfService;
using FocusWcfService.Common;
using FocusWcfService.LocationsHelpers;
using FocusWcfService.Models;
using FocusWcfService.ProcessesHelpers;
using NHibernate;

namespace FocusWindowsService {
    public class ServiceModule : Module {
        protected override void Load(ContainerBuilder builder) {
            builder.Register(c => NHibernateHelper.CreateSessionFactory())
                   .As<ISessionFactory>()
                   .SingleInstance();
            builder.Register(x => x.Resolve<ISessionFactory>().OpenSession()).As<ISession>();

            builder.RegisterType<Repository<WatchedProcess>>().As<IRepository<WatchedProcess>>();
            builder.RegisterType<Repository<WatchedLocation>>().As<IRepository<WatchedLocation>>();
            builder.RegisterType<ProcessesListSqlLiteService>().As<IProcessesListSqlLiteService>();
            builder.RegisterType<OperationsService>().As<IProcessesOperationsService>();
            builder.RegisterType<OperationsService>().As<ILocationsOperationsService>();
            builder.RegisterType<LocationsListSqlLiteService>().As<ILocationsListSqlLiteService>();
        }
    }
}