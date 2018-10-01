using System;
using System.Linq;
using System.Linq.Expressions;
using NHibernate;

namespace FocusWcfService.Common {
    public class Repository<T> : IRepository<T> {
        private readonly ISession session;

        public Repository(ISession session) {
            this.session = session;
        }

        public void Delete(T obj) {
        }

        public void Dispose() {
        }

        public IQueryable<T> Filter<T>(Expression<Func<T, bool>> func) where T : class {
            IQueryable<T> filteredItems;
            using (var transaction = this.session.BeginTransaction()) {
                filteredItems = this.session.Query<T>().Where(func);
                transaction.Commit();
            }

            return filteredItems;
        }

        public T Get(string name) {
            T result;
            using (var transaction = this.session.BeginTransaction()) {
                result = this.session.Get<T>(name);
                transaction.Commit();
            }
            return result;
        }

        public IQueryable<T> GetAll() {
            IQueryable<T> allItems;
            using (var transaction = this.session.BeginTransaction()) {
                allItems = this.session.Query<T>();
                transaction.Commit();
            }
            return allItems;
        }

        public void Save(T obj) {
            using (var transaction = this.session.BeginTransaction()) {
                this.session.Save(obj);
                transaction.Commit();
            }
        }

        public void Update(T obj) {
        }
    }
}