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
    /// 销售单明细表接口
    /// </summary>
    public class SellOrderDetailController : ApiPowerController
    {
        private TbBaseOper<SellOrderDetail> sellOrderDetailOper = null;

        public SellOrderDetailController()
            : base()
        {
            sellOrderDetailOper = new TbBaseOper<SellOrderDetail>(HibernateOper, typeof(SellOrderDetail));
        }

        private void IsExistsCode(ISession session, SellOrderDetail sellOrderDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(SellOrderDetail));

            ICriterion criterion = null;
            if (sellOrderDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(sellOrderDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("SellOrderID", sellOrderDetail.SellOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.SellOrderIsExistsException();//销售明细单号已经存在
            }
        }

        /// <summary>
        /// 添加销售单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(SellOrderDetail sellOrderDetail)
        {
            return Json(sellOrderDetailOper.Add(sellOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在销售明细单号
                    IsExistsCode(session, sellOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改销售单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(SellOrderDetail sellOrderDetail)
        {
            return Json(sellOrderDetailOper.Update(sellOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在销售明细单号
                    IsExistsCode(session, sellOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除销售单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(SellOrderDetail sellOrderDetail)
        {
            return Json(sellOrderDetailOper.Del(sellOrderDetail));
        }

        /// <summary>
        /// 查询销售单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string sellOrderID, string goodsID, string qty, string sellPrice, string realSellPrice, string totalMoney)
        {
            return Json(sellOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(sellOrderID))
                    {
                        ICriterion criterion = Restrictions.Eq("SellOrderID", PojoUtil.InitPojo<SellOrder>(sellOrderID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID))
                    {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty))
                    {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sellPrice)) {
                        ICriterion criterion = Restrictions.Like("SellPrice", sellPrice, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(realSellPrice)) {
                        ICriterion criterion = Restrictions.Like("RealSellPrice", realSellPrice, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(totalMoney)) {
                        ICriterion criterion = Restrictions.Like("TotalMoney", totalMoney, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("SellOrderID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询销售单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string sellOrderID, string goodsID, string qty, string sellPrice, string realSellPrice, string totalMoney)
        {
            return Json(sellOrderDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(sellOrderID)) {
                        ICriterion criterion = Restrictions.Eq("SellOrderID", PojoUtil.InitPojo<SellOrder>(sellOrderID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID)) {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty)) {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sellPrice)) {
                        ICriterion criterion = Restrictions.Like("SellPrice", sellPrice, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(realSellPrice)) {
                        ICriterion criterion = Restrictions.Like("RealSellPrice", realSellPrice, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(totalMoney)) {
                        ICriterion criterion = Restrictions.Like("TotalMoney", totalMoney, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
