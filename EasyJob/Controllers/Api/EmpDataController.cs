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
    /// 数据权限接口
    /// </summary>
    public class EmpDataController : ApiPowerController
    {
        private TbBaseOper<EmpData> empDataOper = null;

        public EmpDataController()
            :base()
        {
            empDataOper = new TbBaseOper<EmpData>(HibernateOper, typeof(EmpData));
        }

        /// <summary>
        /// 添加数据权限
        /// </summary>
        /// <param name="empData"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(EmpData empData)
        {
            return Json(empDataOper.Add(empData));
        }


        /// <summary>
        /// 修改数据权限
        /// </summary>
        /// <param name="empData"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(EmpData empData)
        {
            return Json(empDataOper.Update(empData));
        }

        /// <summary>
        /// 删除数据权限
        /// </summary>
        /// <param name="empData"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(EmpData empData)
        {
            return Json(empDataOper.Del(empData));
        }

        /// <summary>
        /// 查询数据权限
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string roleFlag, string roleId, string deptId)
        {
            return Json(empDataOper.Get(
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
                    if (!string.IsNullOrEmpty(deptId))
                    {
                        ICriterion criterion = Restrictions.Eq("Dept", PojoUtil.InitPojo<Department>(deptId));
                        criteria.Add(criterion);
                    }

                    //创建对象属性别名
                    criteria.CreateAlias("Dept", "d");
                    criteria.AddOrder(Order.Asc("d.Name"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询数据权限页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string roleFlag, string roleId, string deptId)
        {
            return Json(empDataOper.GetPageCount(
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
                    if (!string.IsNullOrEmpty(deptId))
                    {
                        ICriterion criterion = Restrictions.Eq("Dept", PojoUtil.InitPojo<Department>(deptId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
