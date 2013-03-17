using NHibernate;
using NHibernate.Criterion;

namespace FindANewman.Data.Repositories.Generic
{
    public interface IDataExecutionContext<T>
    {
        DetachedCriteria GetDetachedCriteria();
        DetachedCriteria GetDetachedCriteria(string field, object value);
        ICriteria ExecuteCriteria(ISession session, DetachedCriteria criteria);
    }
}