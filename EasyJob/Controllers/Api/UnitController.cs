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
    /// 单位接口
    /// </summary>
    public class UnitController : ApiPowerController
    {
        private TbBaseOper<Unit> unitOper = null;

        public UnitController()
            :base()
        {
            unitOper = new TbBaseOper<Unit>(HibernateOper, typeof(Unit));
        }

        private void IsExistsName(ISession session, Unit unit)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Unit));

            ICriterion criterion = null;
            if (unit.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(unit.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("UnitName", unit.UnitName);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.UnitNameIsExistsException();//单位名已经存在
            }
        }

        /// <summary>
        /// 添加单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Unit unit)
        {
            return Json(unitOper.Add(unit,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsName(session, unit);
                }
                ));
        }


        /// <summary>
        /// 修改单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Unit unit)
        {
            return Json(unitOper.Update(unit,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsName(session, unit);
                }
                ));
        }

        /// <summary>
        /// 删除单位
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Unit unit)
        {
            return Json(unitOper.Del(unit));
        }

        /// <summary>
        /// 查询单位
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string unitName)
        {
            return Json(unitOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(unitName))
                    {
                        ICriterion criterion = Restrictions.Like("UnitName", unitName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("UnitName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询单位页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string unitName)
        {
            return Json(unitOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(unitName))
                    {
                        ICriterion criterion = Restrictions.Like("UnitName", unitName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
