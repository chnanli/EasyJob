using System.Web;
using System.Web.Mvc;
using EasyJob.Filters;

namespace EasyJob
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new DefaultValFilterAttribute());//自动加默认值过滤
            filters.Add(new MyResultFillters());//自定义返回过滤
            filters.Add(new MyExceptionFilter());//自定义异常过滤
            filters.Add(new HandleErrorAttribute());
        }
    }
}