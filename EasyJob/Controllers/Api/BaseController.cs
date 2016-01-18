using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Pojo.Pojo;
using ORM.Hibernate;

namespace EasyJob.Controllers.Api
{
    public class BaseController : Controller
    {
        public IHibernateOper HibernateOper { get; set; }

        public BaseController()
        {
            try
            {
                //获取数据库操作对象
                HibernateOper = HibernateFactory.GetInstance();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetUrl()
        {
            return "http://"+Request.Url.Host + Request.Url.PathAndQuery;
        }
    }
}