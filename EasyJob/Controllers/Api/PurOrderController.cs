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
    /// 采购单主表接口
    /// </summary>
    public class PurOrderController : ApiPowerController
    {
         private TbBaseOper<PurOrder> purOrderOper = null;

         public PurOrderController()
            : base()
        {
            purOrderOper = new TbBaseOper<PurOrder>(HibernateOper, typeof(PurOrder));
        }

        private void IsExistsCode(ISession session, PurOrder pur)
        {
            ICriteria criteria = session.CreateCriteria(typeof(PurOrder));

            ICriterion criterion = null;
            if (pur.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(pur.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("PurOrderCode", pur.PurOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.PurOrderIsExistsException();//采购主单号已经存在
            }
        }

        /// <summary>
        /// 添加采购单主表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(PurOrder purOrder)
        {
            return Json(purOrderOper.Add(purOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在采购主单号
                    IsExistsCode(session, purOrder);
                }
                ));
        }


        /// <summary>
        /// 修改采购单主表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(PurOrder purOrder)
        {
            return Json(purOrderOper.Update(purOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在采购主单号
                    IsExistsCode(session, purOrder);
                }
                ));
        }

        /// <summary>
        /// 删除采购单主表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(PurOrder purOrder)
        {
            return Json(purOrderOper.Del(purOrder));
        }

        /// <summary>
        /// 查询采购单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string purOrderCode, string vendorID, string storeHouseID, string buyer, string approvalTime, string state, string totalMoney)
        {
            return Json(purOrderOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(purOrderCode))
                    {
                        ICriterion criterion = Restrictions.Like("PurOrderCode", purOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(vendorID))
                    {
                        ICriterion criterion = Restrictions.Eq("VendorID", PojoUtil.InitPojo<VendorInfo>(vendorID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID))
                    {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(buyer))
                    {
                        ICriterion criterion = Restrictions.Like("Buyer", buyer, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalTime))
                    {
                        ICriterion criterion = Restrictions.Like("ApprovalTime", approvalTime, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(state)) {
                        ICriterion criterion = Restrictions.Like("State", state, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(totalMoney)) {
                        ICriterion criterion = Restrictions.Like("TotalMoney", approvalTime, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("PurOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询采购单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string purOrderCode, string vendorID, string storeHouseID, string buyer, string approvalTime, string state, string totalMoney)
        {
            return Json(purOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(purOrderCode)) {
                        ICriterion criterion = Restrictions.Like("PurOrderCode", purOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(vendorID)) {
                        ICriterion criterion = Restrictions.Eq("VendorID", PojoUtil.InitPojo<VendorInfo>(vendorID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(buyer)) {
                        ICriterion criterion = Restrictions.Like("Buyer", buyer, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalTime)) {
                        ICriterion criterion = Restrictions.Like("ApprovalTime", approvalTime, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(state)) {
                        ICriterion criterion = Restrictions.Like("State", state, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(totalMoney)) {
                        ICriterion criterion = Restrictions.Like("TotalMoney", approvalTime, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
