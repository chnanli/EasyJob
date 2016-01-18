using EasyJob.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiXin.Tools;

namespace EasyJob.Controllers.Admin
{
    public class AdminController : Controller
    {
        public class MenuGroup
        {
            public string id;
            public string icon;
            public IList<Menu> menu;
        }

        public class Menu
        {
            public string text;
            public IList<MenuItem> items;
        }
        public class MenuItem{
            public string id;
            public string text;
            public string href;
        }

        //
        // GET: /Admin/

        //[LoginActionFilterAttribute]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(string OpenId)
        {
            ViewBag.OpenId = OpenId;
            return View();
        }

        //[LoginActionFilterAttribute]
        public ActionResult GetMenuBar()
        {
            return Json(CreateMenuBar());
        }

        private IList<MenuGroup> CreateMenuBar()
        {
            IList<MenuGroup> retVal = new List<MenuGroup>();
            MenuGroup mg = null;
            Menu menu = null;
            MenuItem menuItem = null;

            mg = new MenuGroup();
            mg.id = "1";
            mg.icon = "nav-home";
            mg.menu = new List<Menu>();
            retVal.Add(mg);   

            menu = new Menu();
            menu.text = "系统设置";
            menu.items=new List<MenuItem>();
            mg.menu.Add(menu);

            menuItem=new MenuItem();
            menuItem.id="1_1";
            menuItem.text="权限设置";
            menuItem.href="abc.html";
            menu.items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.id = "1_2";
            menuItem.text = "用户设置";
            menuItem.href = "abc.html";
            menu.items.Add(menuItem);


            mg = new MenuGroup();
            mg.id = "2";
            mg.menu = new List<Menu>();
            retVal.Add(mg);

            menu = new Menu();
            menu.text = "基本信息管理";
            menu.items = new List<MenuItem>();
            mg.menu.Add(menu);

            menuItem = new MenuItem();
            menuItem.id = "1_1";
            menuItem.text = "客户管理";
            menuItem.href = "abc.html";
            menu.items.Add(menuItem);

            menuItem = new MenuItem();
            menuItem.id = "1_2";
            menuItem.text = "师傅管理";
            menuItem.href = "abc.html";
            menu.items.Add(menuItem);

            return retVal;
        }

        /// <summary>
        /// 菜单模块管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ModManager()
        {
            return View();
        }

        /// <summary>
        /// 权限管理
        /// </summary>
        /// <returns></returns>
        public ActionResult PowerManager()
        {
            return View();
        }

        /// <summary>
        /// 小区管理
        /// </summary>
        /// <returns></returns>
        public ActionResult GardenManager()
        {
            return View();
        }

        /// <summary>
        /// 客户管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerManager()
        {
            return View();
        }

        /// <summary>
        /// 工单管理
        /// </summary>
        /// <returns></returns>
        public ActionResult WorkManager()
        {
            return View();
        }

        public ActionResult WorkCodeTypeManager()
        {
            return View();
        }
    }
}
