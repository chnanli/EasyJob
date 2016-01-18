using EasyJob.Consts;
using EasyJob.Controllers.Api;
using EasyJob.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EasyJob.Filters
{
    public class MyExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public class JsonException
        {
            public string Error { get; set; }
        }
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.Controller is ApiPowerController)
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                //Needed for IIS7.0
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                JsonResult jr = new JsonResult();
                jr.Data = new JsonException() { Error = filterContext.Exception.Message };
                //允许GET请求JSON
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                filterContext.Result = jr;
            }
            else
            {
                if (filterContext.Exception.Message == (new Exceptions.IsNoLoginException()).Message)
                {
                    filterContext.ExceptionHandled = true;
                    filterContext.Result = new RedirectResult("Admin/Login");
                }
            }
        }
    }
}