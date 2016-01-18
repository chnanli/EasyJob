using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Filters;
using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using ORM.Hibernate;

namespace EasyJob.Controllers.Api
{
    public class HomeController : ApiPowerController
    {
        private TbBaseOper<Employee> empOper = null; 
        public HomeController()
        {
            IHibernateOper hibernateOper = HibernateFactory.GetInstance();
            empOper = new TbBaseOper<Employee>(hibernateOper,typeof(Employee));
        }

        public ActionResult Login(string code, string pwd)
        {
            IList<Employee> emps = empOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("Code", code);
                    criteria.Add(criterion);
                    criterion = Restrictions.Eq("Pwd", pwd);
                    criteria.Add(criterion);
                }
                );
            if (emps != null && emps.Count > 0)
            {
                Session["Employee"] = emps[0];
                emps.Clear();
            }
            else
            {
//                throw new Exception("UserNameOrPwdError");//用户名或密码错误
            }
            return Json("true");
        }

        //
        // GET: /Home/

        [LoginActionFilterAttribute]//登录拦截器
        public ActionResult Index()
        {
            Employee emp = null;

            emp = empOper.Get(new Guid("33057974-256B-4CBB-8AB3-07C097E0B77A"));

            ViewBag.Emp = emp;

            return View();
        }

        public ActionResult GetEmp()
        {
            Employee emp = null;

            emp = empOper.Get(new Guid("33057974-256B-4CBB-8AB3-07C097E0B77A"));

            return Json(emp);
        }

        public ActionResult Zero(int c)
        {
            int a = 10;

            a = a/c;

            return Json(a);
        }
    }
}
