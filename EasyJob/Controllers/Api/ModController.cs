using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Consts;
using EasyJob.Filters;
using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using ORM.Hibernate;
using System.Runtime.Serialization;

namespace EasyJob.Controllers.Api
{
    public class ModController : ApiPowerController
    {
        private TbBaseOper<Mod> modOper = null;
        private TbBaseOper<ModFunc> modFuncOper = null;
        
        public ModController()
            : base()
        {
            modOper = new TbBaseOper<Mod>(HibernateOper, typeof(Mod));
            modFuncOper = new TbBaseOper<ModFunc>(HibernateOper, typeof(ModFunc));
        }

        public void Init()
        {
            try
            {
                Guid pId;

                Mod mod = new Mod();

                mod = new Mod();
                mod.Name = "SystemManager";
                mod.Text = "系统管理";
                mod.Icon = "glyphicon glyphicon-cog";
                mod.Type = 1; //有子菜单
                mod.Href = "";
                mod.Index = 1;
                modOper.Add(mod);

                pId = mod.Id;

                Type type = typeof(ModController);
                //添加菜单的操作函数
                ModFunc modFunc = new ModFunc();
                modFunc.Cls = type.FullName;
                modFuncOper.Add(modFunc);

                Mod subMod = new Mod();
                subMod.Name = "ModManager";
                subMod.Text = "菜单管理";
                subMod.Icon = "glyphicon glyphicon-user";
                subMod.Type = 0; //没有子菜单
                subMod.Href = "/Admin/ModManager";
                subMod.PId = pId;
                subMod.Index = 0;
                subMod.ModFunc = modFunc;
                modOper.Add(subMod);

                type = typeof(EmployeeController);
                //添加菜单的操作函数
                modFunc = new ModFunc();
                modFunc.Cls = type.FullName;
                modFuncOper.Add(modFunc);

                subMod = new Mod();
                subMod.Name = "PowerManager";
                subMod.Text = "权限管理";
                subMod.Icon = "glyphicon glyphicon-user";
                subMod.Type = 0; //没有子菜单
                subMod.Href = "/Admin/PowerManager";
                subMod.PId = pId;
                subMod.Index = 0;
                subMod.ModFunc = modFunc;
                modOper.Add(subMod);

            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        public void Init2()
        {
            try
            {
                //给员工初始化
                Employee e = new Employee();
                e.Code = "999";
                e.EmpName = "System";
                e.PwdWeb = "123456";
                e.Birthday = DateTime.Now;
                e.ComeDate = DateTime.Now;
                e.StaffDate = DateTime.Now;
                e.LeaveDate = DateTime.Now;
                e.OnDuty = DateTime.Now;
                e.OffDuty = DateTime.Now;

                TbBaseOper<Employee> employeeOper = new TbBaseOper<Employee>(HibernateFactory.GetInstance(), typeof(Employee));
                employeeOper.Add(e);

                TbBaseOper<EmpModFunc> emfOper = new TbBaseOper<EmpModFunc>(HibernateFactory.GetInstance(), typeof(EmpModFunc));
                IList<ModFunc> funcs = modFuncOper.Get();

                foreach (ModFunc mf in funcs)
                {
                    EmpModFunc emf = new EmpModFunc();
                    emf.Emp = e;
                    emf.ModFunc = mf;
                    emf.FuncNames = "|Add|Update|Del|Get|";
                    emfOper.Add(emf);
                }

                TbBaseOper<EmpMod> emOper = new TbBaseOper<EmpMod>(HibernateFactory.GetInstance(), typeof(EmpMod));
                IList<Mod> mods = modOper.Get();

                foreach (Mod m in mods)
                {
                    EmpMod em = new EmpMod();
                    em.Emp = e;
                    em.Mod = m;
                    emOper.Add(em);
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
        }

        //
        // GET: /Mod/

        public ActionResult Index()
        {
            return View();
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Mod mod)
        {
            return Json(modOper.Add(mod));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Mod mod)
        {
            return Json(modOper.Update(mod));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Mod mod)
        {
            return Json(modOper.Del(mod));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum)
        {
            return Json(modOper.Get(Get_OnCriteria, pageSize, pageNum));
        }

        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize)
        {
            return Json(modOper.GetPageCount(GetPageCount_OnCriteria, pageSize));
        }

        public void Get_OnCriteria(object sender, ICriteria criteria)
        {
            criteria.AddOrder(Order.Asc("Index"));
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }
        public void GetPageCount_OnCriteria(object sender, ICriteria criteria)
        {
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }

        [LoginActionFilterAttribute]//登录拦截器
        public ActionResult GetMyMods()
        {
            IList<Mod> retVal = new List<Mod>();

            if (MySelf != null)
            {
                retVal = GetMods(Guid.Empty);

                if (retVal != null)
                {
                    foreach (Mod menu in retVal)
                    {
                        LoadSubMod(menu);
                    }
                }
            }

            return Json(retVal);
        }

        private void LoadSubMod(Mod menu)
        {
            if (menu != null)
            {
                try
                {
                    menu.Mods = GetMods(menu.Id);
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                }

                if (menu.Mods != null && menu.Mods.Count > 0)
                {
                    menu.Type = 1;
                }
                else
                {
                    menu.Type = 0;
                }
            }
        }

        private IList<Mod> GetMods(Guid modPId)
        {
            IList<Mod> retVal = new List<Mod>();

            ISession s = null;
            ITransaction tran = null;
            try
            {
                s = HibernateOper.GetCurrentSession();
                tran = s.BeginTransaction();

                ICriterion criterion = null;

                ICriteria criteria = s.CreateCriteria(typeof(EmpMod));
                criterion = Restrictions.Eq("Del", false);
                criteria.Add(criterion);
                criterion = Restrictions.Eq("Emp", MySelf);
                criteria.Add(criterion);

                ICriteria modCriteria = criteria.CreateCriteria("Mod", "m");
                criterion = Restrictions.Eq("Del", false);
                modCriteria.Add(criterion);

                criterion = Restrictions.Eq("PId", modPId);
                modCriteria.Add(criterion);

                modCriteria.AddOrder(Order.Asc("Index"));

                IList<EmpMod> empMods = criteria.List<EmpMod>();

                if (empMods != null)
                {
                    foreach (EmpMod empMod in empMods)
                    {
                        retVal.Add(empMod.Mod);
                    }

                    empMods.Clear();
                }

                tran.Commit();
            }
            catch (Exception e)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            return retVal;
        }


        [DataContract]
        public class TreeNode
        {
            [DataMember(Name = "id")]
            public virtual string Id { get; set; }
            [DataMember(Name = "text")]
            public virtual string Text { get; set; }
            [DataMember(Name = "state")]
            public virtual string State { get; set; }
            [DataMember(Name = "checked")]
            public virtual bool Checked { get; set; }
            [DataMember(Name = "children")]
            public virtual IList<TreeNode> Children { get; set; }
        }

        public ActionResult GetJson()
        {
            IList<TreeNode> treeNodes = new List<TreeNode>();

            TreeNode tn = null;
            tn = new TreeNode();
            tn.Id = "1";
            tn.Text = "节点1";
            tn.State = "open";
            treeNodes.Add(tn);
            tn.Children = new List<TreeNode>();
            TreeNode tnChild = null;
            tnChild = new TreeNode();
            tnChild.Id = "1_1";
            tnChild.Text = "节点1_1";
            tn.Children.Add(tnChild);
            tnChild = new TreeNode();
            tnChild.Id = "1_2";
            tnChild.Text = "节点1_2";
            tn.Children.Add(tnChild);

            tn = new TreeNode();
            tn.Id = "2";
            tn.Text = "节点2";
            treeNodes.Add(tn);

            return Json(treeNodes);
        }
    }
}
