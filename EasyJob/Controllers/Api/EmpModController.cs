using EasyJob.Filters;
using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using EasyJob.Tools;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyJob.Controllers.Api
{
    /// <summary>
    /// 员工模块权限表(即菜单)接口
    /// </summary>
    public class EmpModController : ApiPowerController
    {
        private TbBaseOper<EmpMod> empModOper = null;

        public EmpModController()
            :base()
        {
            empModOper = new TbBaseOper<EmpMod>(HibernateOper, typeof(EmpMod));
        }

        /// <summary>
        /// 添加员工模块权限表(即菜单)
        /// </summary>
        /// <param name="empMod"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(EmpMod empMod)
        {
            return Json(empModOper.Add(empMod));
        }


        /// <summary>
        /// 修改员工模块权限表(即菜单)
        /// </summary>
        /// <param name="empMod"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(EmpMod empMod)
        {
            return Json(empModOper.Update(empMod));
        }

        /// <summary>
        /// 删除员工模块权限表(即菜单)
        /// </summary>
        /// <param name="empMod"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(EmpMod empMod)
        {
            return Json(empModOper.Del(empMod));
        }

        /// <summary>
        /// 查询员工模块权限表(即菜单)
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string roleFlag, string roleId, string modId)
        {
            return Json(empModOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(roleFlag))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleFlag", roleFlag);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleId", new Guid(roleId));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(modId))
                    {
                        ICriterion criterion = Restrictions.Eq("Mod", PojoUtil.InitPojo<Mod>(modId));
                        criteria.Add(criterion);
                    }

                    //创建对象属性别名
                    criteria.CreateAlias("Mod", "m");
                    criteria.AddOrder(Order.Asc("m.Name"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询员工模块权限表(即菜单)页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string roleFlag, string roleId, string modId)
        {
            return Json(empModOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(roleFlag))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleFlag", roleFlag);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleId", new Guid(roleId));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(modId))
                    {
                        ICriterion criterion = Restrictions.Eq("Mod", PojoUtil.InitPojo<Mod>(modId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
