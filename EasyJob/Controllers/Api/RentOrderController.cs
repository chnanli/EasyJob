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
    /// 领用单主表接口
    /// </summary>
    public class RentOrderController : ApiPowerController
    {
        private TbBaseOper<RentOrder> rentOrderOper = null;

        public RentOrderController()
            : base()
        {
            rentOrderOper = new TbBaseOper<RentOrder>(HibernateOper, typeof(RentOrder));
        }

        private void IsExistsCode(ISession session, RentOrder rent)
        {
            ICriteria criteria = session.CreateCriteria(typeof(ReqOrder));

            ICriterion criterion = null;
            if (rent.Id != Guid.Empty) {
                criterion = Restrictions.Not(Restrictions.IdEq(rent.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RentOrderCode", rent.RentOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0) {
                throw new EasyJob.Tools.Exceptions.RentOrderIsExistsException();//领用主单号已经存在
            }
        }

        /// <summary>
        /// 添加领用单主表
        /// </summary>
        /// <param name="rentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(RentOrder rentOrder)
        {
            return Json(rentOrderOper.Add(rentOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在领用主单号
                    IsExistsCode(session, rentOrder);
                }
                ));
        }


        /// <summary>
        /// 修改领用单主表
        /// </summary>
        /// <param name="rentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RentOrder rentOrder)
        {
            return Json(rentOrderOper.Update(rentOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在领用主单号
                    IsExistsCode(session, rentOrder);
                }
                ));
        }

        /// <summary>
        /// 删除领用单主表
        /// </summary>
        /// <param name="rentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RentOrder rentOrder)
        {
            return Json(rentOrderOper.Del(rentOrder));
        }

        /// <summary>
        /// 查询领用单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string rentOrderCode, string storeHouseID, string borrID, string borrDeptID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(rentOrderOper.Get(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(rentOrderCode)) {
                        ICriterion criterion = Restrictions.Like("RentOrderCode", rentOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(borrID)) {
                        ICriterion criterion = Restrictions.Eq("BorrID", PojoUtil.InitPojo<Employee>(borrID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(borrDeptID)) {
                        ICriterion criterion = Restrictions.Eq("BorrDeptID", PojoUtil.InitPojo<Department>(borrID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(oPerTime)) {
                        ICriterion criterion = Restrictions.Like("OPerTime", oPerTime, MatchMode.Anywhere);
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
                    criteria.AddOrder(Order.Asc("RentOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询领用单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string rentOrderCode, string storeHouseID, string borrID, string borrDeptID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(rentOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(rentOrderCode)) {
                        ICriterion criterion = Restrictions.Like("RentOrderCode", rentOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(borrID)) {
                        ICriterion criterion = Restrictions.Eq("BorrID", PojoUtil.InitPojo<Employee>(borrID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(borrDeptID)) {
                        ICriterion criterion = Restrictions.Eq("BorrDeptID", PojoUtil.InitPojo<Department>(borrID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(oPerTime)) {
                        ICriterion criterion = Restrictions.Like("OPerTime", oPerTime, MatchMode.Anywhere);
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
                }
                , pageSize));
        }

    }
}
