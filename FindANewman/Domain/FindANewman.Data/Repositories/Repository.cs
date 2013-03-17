using System.Linq;
using FindANewman.Data.Repositories.Generic;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace FindANewman.Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private IDataExecutionContext<T> ExecutionContext { get; set; }

        public Repository(IDataExecutionContext<T> dataExecutionContext)
        {
            ExecutionContext = dataExecutionContext;
        }

        //TODO: create mockable SessionPerRequest
        public virtual ISession Session
        {
            get { return NHibernateSessionPerRequest.GetCurrentSession(); }
        }
    
        public T GetById(int id)
        {
            return Session.Get<T>(id);
        }

        public virtual void Save(T target)
        {
            Session.SaveOrUpdate(target);
        }

        public void Delete(T target)
        {
            Session.Delete(target);
        }

        public void Delete(string target)
        {
            Session.Delete(target);
        }

        public IQueryable<T> Linq()
        {
            return Session.Query<T>().Cacheable();
        }

        public IQueryable<T> FindAll()
        {
            var criteria = ExecutionContext.GetDetachedCriteria();
            return ExecutionContext.ExecuteCriteria(Session, criteria).List<T>().AsQueryable();
        }

        public T FindOne(string field, object value)
        {
            var criteria = ExecutionContext.GetDetachedCriteria(field, value);
            return FindOne(criteria);
        }

        public T FindOne(DetachedCriteria criteria)
        {
            var crit = GetExecutableCriteria(Session, criteria);
            return crit.UniqueResult<T>();
        }

        protected ICriteria GetExecutableCriteria(ISession session, DetachedCriteria criteria)
        {
            return ExecutionContext.ExecuteCriteria(session, criteria);
        }

        public IQueryable<T> FindAll(DetachedCriteria detachedCriteria)
        {
            return ExecutionContext.ExecuteCriteria(Session, detachedCriteria).List<T>().AsQueryable();
        }
    }
}