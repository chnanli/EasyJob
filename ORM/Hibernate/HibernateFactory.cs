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
    public class HibernateFactory
    {

        private static IHibernateOper hibernateOper = null;
        private static object _lock = new object();

        private HibernateFactory()
        {
            
        }

        public static IHibernateOper GetInstance()
        {
            try
            {
                if (hibernateOper == null)
                {
                    lock (_lock)
                    {
                        if (hibernateOper == null)
                        {
                            hibernateOper = new HibernateOper();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
            return hibernateOper;
        }
    }
}
