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
    /// 领用单明细表接口
    /// </summary>
    public class RentOrderDetailController : ApiPowerController
    {
       private TbBaseOper<RentOrderDetail> rentOrderDetailOper = null;

       public RentOrderDetailController()
            : base()
        {
            rentOrderDetailOper = new TbBaseOper<RentOrderDetail>(HibernateOper, typeof(RentOrderDetail));
        }

       private void IsExistsCode(ISession session, RentOrderDetail rentOrderDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(RentOrderDetail));

            ICriterion criterion = null;
            if (rentOrderDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(rentOrderDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RentOrderID", rentOrderDetail.RentOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.RentOrderIsExistsException();//领用明细单号已经存在
            }
        }

        /// <summary>
        /// 添加领用单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
       public ActionResult Add(RentOrderDetail rentOrderDetail)
        {
            return Json(rentOrderDetailOper.Add(rentOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在领用明细单号
                    IsExistsCode(session, rentOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改领用单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RentOrderDetail rentOrderDetail)
        {
            return Json(rentOrderDetailOper.Update(rentOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在领用明细单号
                    IsExistsCode(session, rentOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除领用单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RentOrderDetail rentOrderDetail)
        {
            return Json(rentOrderDetailOper.Del(rentOrderDetail));
        }

        /// <summary>
        /// 查询领用单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string rentOrderID, string goodsID, string qty)
        {
            return Json(rentOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(rentOrderID))
                    {
                        ICriterion criterion = Restrictions.Eq("RentOrderID", PojoUtil.InitPojo<RentOrder>(rentOrderID));
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
                    criteria.AddOrder(Order.Asc("RentOrderID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询领用单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string rentOrderID, string goodsID, string qty)
        {
            return Json(rentOrderDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(rentOrderID)) {
                        ICriterion criterion = Restrictions.Eq("RentOrderID", PojoUtil.InitPojo<RentOrder>(rentOrderID));
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
                }
                , pageSize));
        }

    }
}
