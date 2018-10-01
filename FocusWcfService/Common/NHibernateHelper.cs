using System;
using System.Linq;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Mapping;
using FluentNHibernate.Mapping.Providers;
using FocusWcfService.Models;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace FocusWcfService.Common {
    public class NHibernateHelper {
        private static ISessionFactory sessionFactory;

        private static ISessionFactory SessionFactory => sessionFactory ?? (sessionFactory = CreateSessionFactory());

        public static ISession OpenSession() {
            return SessionFactory.OpenSession();
        }

        public static void CloseSession() {
            SessionFactory.Close();
        }

        public static ISessionFactory CreateSessionFactory() {
            return Fluently.Configure()
                           .Database(SQLiteConfiguration.Standard.UsingFile("focusDatabase.sqlite"))
                           .Mappings(m => { m.FluentMappings.AddFromNamespaceOf<WatchedProcess>(); })
                           .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                           .BuildSessionFactory();
        }
    }

    public static class FluentNHibernateExtensions {
        public static FluentMappingsContainer AddFromNamespaceOf<T>(this FluentMappingsContainer fmc) {
            var ns = typeof(T).Namespace;
            var types = typeof(T).Assembly.GetExportedTypes()
                                 .Where(t => t.Namespace == ns)
                                 .Where(x => IsMappingOf<IMappingProvider>(x) ||
                                             IsMappingOf<IIndeterminateSubclassMappingProvider>(x) ||
                                             IsMappingOf<IExternalComponentMappingProvider>(x) ||
                                             IsMappingOf<IFilterDefinition>(x));

            foreach (var t in types) {
                fmc.Add(t);
            }

            return fmc;
        }

        private static bool IsMappingOf<T>(Type type) {
            return !type.IsGenericType && typeof(T).IsAssignableFrom(type);
        }
    }
}