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
    /// 采购单明细表接口
    /// </summary>
    public class PurOrderDetailController : ApiPowerController
    {
        private TbBaseOper<PurOrderDetail> purOrderDetailOper = null;

        public PurOrderDetailController()
            : base()
        {
            purOrderDetailOper = new TbBaseOper<PurOrderDetail>(HibernateOper, typeof(PurOrderDetail));
        }

       private void IsExistsCode(ISession session, PurOrderDetail retDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(PurOrderDetail));

            ICriterion criterion = null;
            if (retDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(retDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("PurOrderID", retDetail.PurOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.PurOrderIsExistsException();//采购明细单号已经存在
            }
        }

        /// <summary>
        /// 添加采购单明细表
        /// </summary>
        /// <param name="purOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
       public ActionResult Add(PurOrderDetail purOrderDetail)
        {
            return Json(purOrderDetailOper.Add(purOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在采购明细单号
                    IsExistsCode(session, purOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改采购单明细表
        /// </summary>
        /// <param name="purOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(PurOrderDetail purOrderDetail)
        {
            return Json(purOrderDetailOper.Update(purOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在采购明细单号
                    IsExistsCode(session, purOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除采购单明细表
        /// </summary>
        /// <param name="purOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(PurOrderDetail purOrderDetail)
        {
            return Json(purOrderDetailOper.Del(purOrderDetail));
        }

        /// <summary>
        /// 查询采购单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string purOrderID, string goodsID, string price, string qty)
        {
            return Json(purOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(purOrderID))
                    {
                        ICriterion criterion = Restrictions.Eq("PurOrderID", PojoUtil.InitPojo<PurOrder>(purOrderID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID))
                    {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(price))
                    {
                        ICriterion criterion = Restrictions.Like("Price", price, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty))
                    {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("PurOrderID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询采购单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retOrderID, string goodsID, string price, string qty)
        {
            return Json(purOrderDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retOrderID)) {
                        ICriterion criterion = Restrictions.Eq("RetOrderID", PojoUtil.InitPojo<RetOrder>(retOrderID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID)) {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(price)) {
                        ICriterion criterion = Restrictions.Like("Price", price, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty)) {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
