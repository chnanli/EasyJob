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
    public class DefaultValFilterAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var methodInfo = ((ReflectedActionDescriptor)filterContext.ActionDescriptor).MethodInfo;
            foreach (var p in methodInfo.GetParameters())
            {
                if (p.ParameterType.IsValueType)
                {
                    if (filterContext.ActionParameters[p.Name] == null)
                    {
                        filterContext.ActionParameters[p.Name] = Activator.CreateInstance(p.ParameterType);
                    }
                }
            }
        }

    }
}