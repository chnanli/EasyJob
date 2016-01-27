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
    /// 员工模块功能权限表接口
    /// </summary>
    public class EmpModFuncController : ApiPowerController
    {
        private TbBaseOper<EmpModFunc> empModFuncOper = null;

        public EmpModFuncController()
            :base()
        {
            empModFuncOper = new TbBaseOper<EmpModFunc>(HibernateOper, typeof(EmpModFunc));
        }

        /// <summary>
        /// 添加员工模块功能权限表
        /// </summary>
        /// <param name="empModFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(EmpModFunc empModFunc)
        {
            return Json(empModFuncOper.Add(empModFunc));
        }


        /// <summary>
        /// 修改员工模块功能权限表
        /// </summary>
        /// <param name="empModFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(EmpModFunc empModFunc)
        {
            return Json(empModFuncOper.Update(empModFunc));
        }

        /// <summary>
        /// 删除员工模块功能权限表
        /// </summary>
        /// <param name="empModFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(EmpModFunc empModFunc)
        {
            return Json(empModFuncOper.Del(empModFunc));
        }

        /// <summary>
        /// 查询员工模块功能权限表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string roleFlag, string roleId, string modFuncId)
        {
            return Json(empModFuncOper.Get(
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
                    if (!string.IsNullOrEmpty(modFuncId))
                    {
                        ICriterion criterion = Restrictions.Eq("ModFunc", PojoUtil.InitPojo<ModFunc>(modFuncId));
                        criteria.Add(criterion);
                    }

                    //创建对象属性别名
                    criteria.CreateAlias("ModFunc", "mf");
                    criteria.AddOrder(Order.Asc("mf.Cls"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询员工模块功能权限表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string roleFlag, string roleId, string modFuncId)
        {
            return Json(empModFuncOper.GetPageCount(
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
                    if (!string.IsNullOrEmpty(modFuncId))
                    {
                        ICriterion criterion = Restrictions.Eq("ModFunc", PojoUtil.InitPojo<ModFunc>(modFuncId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
