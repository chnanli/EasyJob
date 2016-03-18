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
    /// 退货单主表接口
    /// </summary>
    public class RetOrderController : ApiPowerController
    {
        private TbBaseOper<RetOrder> retOrderOper = null;

       public RetOrderController()
            : base()
        {
            retOrderOper = new TbBaseOper<RetOrder>(HibernateOper, typeof(RetOrder));
        }

       private void IsExistsCode(ISession session, RetOrder pet)
        {
            ICriteria criteria = session.CreateCriteria(typeof(RetOrder));

            ICriterion criterion = null;
            if (pet.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(pet.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RetOrderCode", pet.RetOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.RetOrderIsExistsException();//退货主单号已经存在
            }
        }

        /// <summary>
        /// 添加退货单主表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
       public ActionResult Add(RetOrder retOrder)
        {
            return Json(retOrderOper.Add(retOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在退货主单号
                    IsExistsCode(session, retOrder);
                }
                ));
        }


        /// <summary>
        /// 修改退货单主表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RetOrder retOrder)
        {
            return Json(retOrderOper.Update(retOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在退货主单号
                    IsExistsCode(session, retOrder);
                }
                ));
        }

        /// <summary>
        /// 删除退货单主表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RetOrder retOrder)
        {
            return Json(retOrderOper.Del(retOrder));
        }

        /// <summary>
        /// 查询退货单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string retOrderCode, string vendorID, string storeHouseID, string retMember, string approvalID, string approvalTime, string state, string totalMoney)
        {
            return Json(retOrderOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retOrderCode))
                    {
                        ICriterion criterion = Restrictions.Like("RetOrderCode", retOrderCode, MatchMode.Anywhere);
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
                    if (!string.IsNullOrEmpty(retMember))
                    {
                        ICriterion criterion = Restrictions.Like("RetMember", retMember, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalID))
                    {
                        ICriterion criterion = Restrictions.Eq("ApprovalID", PojoUtil.InitPojo<Employee>(approvalID));
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
                        ICriterion criterion = Restrictions.Like("TotalMoney", totalMoney, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("RetOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询退货单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retOrderCode, string vendorID, string storeHouseID, string retMember, string approvalID, string approvalTime, string state, string totalMoney)
        {
            return Json(retOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retOrderCode)) {
                        ICriterion criterion = Restrictions.Like("RetOrderCode", retOrderCode, MatchMode.Anywhere);
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
                    if (!string.IsNullOrEmpty(retMember)) {
                        ICriterion criterion = Restrictions.Like("RetMember", retMember, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalID)) {
                        ICriterion criterion = Restrictions.Eq("ApprovalID", PojoUtil.InitPojo<Employee>(approvalID));
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
                        ICriterion criterion = Restrictions.Like("TotalMoney", totalMoney, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
