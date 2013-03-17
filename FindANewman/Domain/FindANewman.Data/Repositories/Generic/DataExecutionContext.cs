using NHibernate;
using NHibernate.Criterion;

namespace FindANewman.Data.Repositories.Generic
{
    public class DataExecutionContext<T> : IDataExecutionContext<T>
    {
        #region IDataExecutionContext<T> Members

        public DetachedCriteria GetDetachedCriteria()
        {
            return DetachedCriteria.For<T>();
        }

        public DetachedCriteria GetDetachedCriteria(string field, object value)
        {
            return DetachedCriteria
                .For<T>()
                .Add(Restrictions.Eq(field, value));
        }

        public ICriteria ExecuteCriteria(ISession session, DetachedCriteria criteria)
        {
            return criteria.GetExecutableCriteria(session);
        }

        #endregion
    }
}