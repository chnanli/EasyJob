using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Consts;
using EasyJob.Controllers;
using EasyJob.Pojo.Pojo;
using EasyJob.Controllers.Api;
using ORM.Hibernate;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using EasyJob.Tools;

namespace EasyJob.Filters
{
    public class LoginActionFilterAttribute : ActionFilterAttribute
    {
        private bool IsFilter = false;//是否过滤

        private TbBaseOper<EmpModFunc> empModFuncOper = null;

        public LoginActionFilterAttribute(){
            IHibernateOper hibernateOper = HibernateFactory.GetInstance();
            empModFuncOper = new TbBaseOper<EmpModFunc>(hibernateOper, typeof(EmpModFunc));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Controller controller = (Controller) filterContext.Controller;

            Employee emp = (Employee)controller.Session[SessionConst.Employee];
            IList<EmpModFunc> emfs = (IList<EmpModFunc>)controller.Session[SessionConst.EmpModFuncs];
            if (emp!=null && emfs == null)
            {
                emfs = GetEmpModFuncs(emp);
                //把用户权限赋给SESSION
                controller.Session[SessionConst.EmpModFuncs] = emfs;
            }

            if (emp == null)
            {
                if (IsFilter)
                {
                    throw new Exceptions.IsNoLoginException();
                }
            }

            if (filterContext.Controller is PowerController)
            {
                PowerController bc = (PowerController)filterContext.Controller;
                if (emp != null)
                {
                    bc.MySelf = emp;
                    bc.MyModFunc=emfs;
                }
            }
        }

        public IList<EmpModFunc> GetEmpModFuncs(Employee emp)
        {
            IList<EmpModFunc> retVal = empModFuncOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = null;
                    criterion = Restrictions.Eq("Emp", emp);
                    criteria.Add(criterion);
                }
                );
            return retVal;
        }
    }
}