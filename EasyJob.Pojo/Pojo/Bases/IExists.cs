using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyJob.Pojo.Pojo.Bases
{
    public interface IExists
    {
        bool IsExists(ISession session);
    }
}
