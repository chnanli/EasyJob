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
    /// 调拨单明细表接口
    /// </summary>
    public class ReqOrderDetailController : ApiPowerController
    {
        private TbBaseOper<ReqOrderDetail> reqOrderDetailOper = null;

        public ReqOrderDetailController()
            : base()
        {
            reqOrderDetailOper = new TbBaseOper<ReqOrderDetail>(HibernateOper, typeof(ReqOrderDetail));
        }

        private void IsExistsCode(ISession session, ReqOrderDetail reqDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(ReqOrderDetail));

            ICriterion criterion = null;
            if (reqDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(reqDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("ReqOrderID", reqDetail.ReqOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.ReqOrderIsExistsException();//调拨明细单号已经存在
            }
        }

        /// <summary>
        /// 添加调拨单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(ReqOrderDetail reqOrderDetail)
        {
            return Json(reqOrderDetailOper.Add(reqOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在调拨明细单号
                    IsExistsCode(session, reqOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改调拨单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(ReqOrderDetail reqOrderDetail)
        {
            return Json(reqOrderDetailOper.Update(reqOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在调拨明细单号
                    IsExistsCode(session, reqOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除调拨单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(ReqOrderDetail reqOrderDetail)
        {
            return Json(reqOrderDetailOper.Del(reqOrderDetail));
        }

        /// <summary>
        /// 查询调拨单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string reqOrderID, string goodsID, string price, string realQty)
        {
            return Json(reqOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(reqOrderID))
                    {
                        ICriterion criterion = Restrictions.Eq("ReqOrderID", PojoUtil.InitPojo<ReqOrder>(reqOrderID));
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
                    if (!string.IsNullOrEmpty(realQty))
                    {
                        ICriterion criterion = Restrictions.Like("RealQty", realQty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("ReqOrderID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询调拨单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retOrderID, string goodsID, string price, string realQty)
        {
            return Json(reqOrderDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retOrderID)) {
                        ICriterion criterion = Restrictions.Eq("RetOrderID", PojoUtil.InitPojo<ReqOrder>(retOrderID));
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
                    if (!string.IsNullOrEmpty(realQty)) {
                        ICriterion criterion = Restrictions.Like("RealQty", realQty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
