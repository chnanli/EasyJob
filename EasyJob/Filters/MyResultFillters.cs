using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyJob.Filters
{
    public class MyResultFillters : FilterAttribute, IResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (filterContext.Result is JsonResult)
            {
                JsonResult jr = (JsonResult) filterContext.Result;
                //允许GET请求JSON
                jr.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            }
        }
    }
}