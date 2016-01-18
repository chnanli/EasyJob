using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace ORM.Hibernate
{
    class HibernateOper : IHibernateOper
    {
        private Configuration cfg;
        private ISessionFactory _sessionFactory;

        public HibernateOper()
        {
            string file = System.AppDomain.CurrentDomain.BaseDirectory + "bin\\Hibernate\\hibernate.cfg.xml";
            cfg = (new Configuration()).Configure(file);
            _sessionFactory=cfg.BuildSessionFactory();
        }

        public HibernateOper(string file)
        {
            cfg = (new Configuration()).Configure(file);
            _sessionFactory = cfg.BuildSessionFactory();
        }

        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory;
        }

        public NHibernate.ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }

        private void BindSession()
        {
            if(!CurrentSessionContext.HasBind(_sessionFactory))
            {
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());
            }
        }

        public NHibernate.ISession GetCurrentSession()
        {
            BindSession();
            return _sessionFactory.GetCurrentSession();
        }

        public void Close(NHibernate.ISession session)
        {
            if (session != null)
            {
                try
                {
                    if (session.IsOpen)
                    {
                        session.Close();
                    }
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
