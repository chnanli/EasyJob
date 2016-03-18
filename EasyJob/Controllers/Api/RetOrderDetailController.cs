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
    /// 退货单明细表接口
    /// </summary>
    public class RetOrderDetailController : ApiPowerController
    {
       private TbBaseOper<RetOrderDetail> retOrderDetailOper = null;

       public RetOrderDetailController()
            : base()
        {
            retOrderDetailOper = new TbBaseOper<RetOrderDetail>(HibernateOper, typeof(RetOrderDetail));
        }

       private void IsExistsCode(ISession session, RetOrderDetail retDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(RetOrderDetail));

            ICriterion criterion = null;
            if (retDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(retDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RetOrderID", retDetail.RetOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.RetOrderIsExistsException();//退货明细单号已经存在
            }
        }

        /// <summary>
        /// 添加退货单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
       public ActionResult Add(RetOrderDetail retOrderDetail)
        {
            return Json(retOrderDetailOper.Add(retOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在退货明细单号
                    IsExistsCode(session, retOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改退货单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RetOrderDetail retOrderDetail)
        {
            return Json(retOrderDetailOper.Update(retOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在退货明细单号
                    IsExistsCode(session, retOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除退货单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RetOrderDetail retOrderDetail)
        {
            return Json(retOrderDetailOper.Del(retOrderDetail));
        }

        /// <summary>
        /// 查询退货单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string retOrderID, string goodsID, string price, string qty)
        {
            return Json(retOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retOrderID))
                    {
                        ICriterion criterion = Restrictions.Eq("RetOrderID", PojoUtil.InitPojo<RetOrder>(retOrderID));
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
                    criteria.AddOrder(Order.Asc("RetOrderID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询退货单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retOrderID, string goodsID, string price, string qty)
        {
            return Json(retOrderDetailOper.GetPageCount(
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
