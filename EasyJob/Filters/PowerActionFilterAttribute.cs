using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Consts;
using EasyJob.Controllers;
using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using ORM.Hibernate;
using EasyJob.Controllers.Api;
using EasyJob.Tools;

namespace EasyJob.Filters
{
    public class PowerActionFilterAttribute : LoginActionFilterAttribute
    {
        private bool IsFilter = false;//是否过滤
        public enum FuncEnum
        {
            Add,
            Update,
            Del,
            Get
        }

        public FuncEnum FuncName { get; set; }

        private TbBaseOper<EmpModFunc> empModFuncOper = null;
        private TbBaseOper<EmpMod> empModOper = null;

        public PowerActionFilterAttribute()
        {
            IHibernateOper hibernateOper = HibernateFactory.GetInstance();
            empModFuncOper = new TbBaseOper<EmpModFunc>(hibernateOper, typeof(EmpModFunc));
            empModOper = new TbBaseOper<EmpMod>(hibernateOper, typeof(EmpMod));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //父级判断是否已经登录
            base.OnActionExecuting(filterContext);

            Controller controller = (Controller)filterContext.Controller;
            Employee emp = (Employee)controller.Session[SessionConst.Employee];
            IList<EmpModFunc> emfs = (IList<EmpModFunc>)controller.Session[SessionConst.EmpModFuncs];
            //if (emfs == null)
            //{
            //    emfs = GetEmpModFuncs(emp);
            //    //把用户权限赋给SESSION
            //    controller.Session[SessionConst.EmpModFuncs] = emfs;
            //}

            //判断是否有该接口权限
            if (!IsHavePower(emfs, controller, FuncName))
            {
                if (IsFilter)
                {
                    throw new Exceptions.IsNoPowerException();
                }
            }

            //if (filterContext.Controller is PowerController)
            //{
            //    PowerController sb = (PowerController)filterContext.Controller;
            //    sb.MyModFunc = emfs;
            //}
        }

        private bool IsHavePower(IList<EmpModFunc> empModFuncs, Controller serviceBase, FuncEnum funcName)
        {
            bool retVal = false;

            string cls = serviceBase.GetType().FullName;

            if (empModFuncs != null)
            {
                foreach (EmpModFunc empModFunc in empModFuncs)
                {
                    //查找用户是否存在类的权限
                    if (empModFunc.ModFunc.Cls.Equals(cls))
                    {
                        if (empModFunc.FuncNames == null)
                        {
                            empModFunc.FuncNames = "";
                        }
                        //把函数枚举转为字符串形式,如"|Add|"
                        string funcNameStr = "|" + funcName + "|";
                        //查找函数权限
                        int find = empModFunc.FuncNames.IndexOf(funcNameStr);
                        if (find > -1)
                        {
                            retVal = true;
                            break;
                        }
                    }
                }
            }
            return retVal;
        }
    }
}