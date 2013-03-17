using System.Linq;
using NHibernate;
using NHibernate.Criterion;

namespace FindANewman.Data.Repositories
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        void Save(T target);
        IQueryable<T> Linq();
        void Delete(T target);
        ISession Session { get; }
        IQueryable<T> FindAll();
        T FindOne(DetachedCriteria criteria);
        IQueryable<T> FindAll(DetachedCriteria detachedCriteria);
    }
}