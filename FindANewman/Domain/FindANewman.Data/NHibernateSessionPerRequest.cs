using System;
using System.Web;
using FindANewman.Data.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Context;

namespace FindANewman.Data
{
    public class NHibernateSessionPerRequest : IHttpModule
    {
        private static readonly ISessionFactory _sessionFactory;

        static NHibernateSessionPerRequest()
        {
            _sessionFactory = CreateSessionFactory();
        }

        #region IHttpModule Members

        public void Init(HttpApplication context)
        {
            context.BeginRequest += BeginRequest;
            context.EndRequest += EndRequest;
        }

        public void Dispose()
        {
        }

        #endregion

        public static ISession GetCurrentSession()
        {
            return _sessionFactory.GetCurrentSession();
        }

        private static void BeginRequest(object sender, EventArgs e)
        {
            ISession session = _sessionFactory.OpenSession();
            session.BeginTransaction();
            CurrentSessionContext.Bind(session);
        }

        private static void EndRequest(object sender, EventArgs e)
        {
            ISession session = CurrentSessionContext.Unbind(_sessionFactory);

            if (session == null) return;

            try
            {
                session.Transaction.Commit();
            }

            catch (Exception)
            {
                session.Transaction.Rollback();
            }

            finally
            {
                session.Close();
                session.Dispose();
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey("Connection")))
                .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "web"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<UserMapping>());
                
            return configuration.BuildSessionFactory();
        }
    }
}