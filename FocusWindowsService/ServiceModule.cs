using Autofac;
using Autofac.Extras.DynamicProxy;
using FocusWcfService;
using FocusWcfService.Aspects;
using FocusWcfService.Common;
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
            //builder.RegisterType<ProcessesListFileService>().As<IProcessesListFileService>();
            builder.RegisterType<ProcessesListSqlLiteService>().As<IProcessesListSqlLiteService>();
            builder.RegisterType<ProcessesOperationsService>().As<IProcessesOperationsService>();

            var sessionInterceptor = new SessionInterceptor();
            builder.RegisterInstance(sessionInterceptor);
        }
    }
}