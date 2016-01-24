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
    /// 部门、分店接口
    /// </summary>
    public class DepartmentController : ApiPowerController
    {
        private TbBaseOper<Department> departmentOper = null;

        public DepartmentController()
            :base()
        {
            departmentOper = new TbBaseOper<Department>(HibernateOper, typeof(Department));
        }

        private void IsExistsCode(ISession session, Department dept)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Department));

            ICriterion criterion = null;
            if (dept.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(dept.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Code", dept.Code);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.DeptCodeIsExistsException();//部门Code已经存在
            }
        }

        /// <summary>
        /// 添加部门、分店
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Department department)
        {
            //根据地址码获取地址
            department.Addr = PojoUtil.GetAddrForCode(HibernateOper,department.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(department.Addr + department.Location);
            if (loc != null)
            {
                department.Lat = loc.lat;
                department.Lng = loc.lng;
            }
            return Json(departmentOper.Add(department,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, department);
                }
                ));
        }


        /// <summary>
        /// 修改部门、分店
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Department department)
        {
            //根据地址码获取地址
            department.Addr = PojoUtil.GetAddrForCode(HibernateOper, department.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(department.Addr + department.Location);
            if (loc != null)
            {
                department.Lat = loc.lat;
                department.Lng = loc.lng;
            }
            return Json(departmentOper.Update(department,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, department);
                }
                ));
        }

        /// <summary>
        /// 删除部门、分店
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Department department)
        {
            return Json(departmentOper.Del(department));
        }

        /// <summary>
        /// 查询部门、分店
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum,string pId,string code,string name,string addr)
        {
            return Json(departmentOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (code != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (addr != null)
                    {
                        ICriterion criterion = Restrictions.Like("Addr", addr,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("Code"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询部门、分店页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string pId, string code, string name, string addr)
        {
            return Json(departmentOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (code != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (addr != null)
                    {
                        ICriterion criterion = Restrictions.Like("Addr", addr, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
