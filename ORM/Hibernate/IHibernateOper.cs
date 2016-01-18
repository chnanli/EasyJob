using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.IO;

namespace ORM.Hibernate
{
    public delegate void OnSession(object sender, ISession session);

    public interface IHibernateOper
    {
        ISessionFactory GetSessionFactory();

        ISession GetSession();

        ISession GetCurrentSession();

        void Close(ISession session);
    }
}
