using System;
using System.Linq;
using System.Linq.Expressions;

namespace FocusWcfService.Common {
    public interface IRepository<T> : IDisposable {
        T Get(string name);
        void Update(T obj);
        void Save(T obj);
        void Delete(T obj);
        IQueryable<T> GetAll();
        IQueryable<T> Filter<T>(Expression<Func<T, bool>> func) where T : class;
    }
}