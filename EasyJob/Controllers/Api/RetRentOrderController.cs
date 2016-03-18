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
    /// 归还单主表接口
    /// </summary>
    public class RetRentOrderController : ApiPowerController
    {

        private TbBaseOper<RetRentOrder> retRentOrderOper = null;

        public RetRentOrderController()
            : base()
        {
            retRentOrderOper = new TbBaseOper<RetRentOrder>(HibernateOper, typeof(RetRentOrder));
        }

        private void IsExistsCode(ISession session, RetRentOrder retRentOrder)
        {
            ICriteria criteria = session.CreateCriteria(typeof(RetRentOrder));

            ICriterion criterion = null;
            if (retRentOrder.Id != Guid.Empty) {
                criterion = Restrictions.Not(Restrictions.IdEq(retRentOrder.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RetRentOrderCode", retRentOrder.RetRentOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0) {
                throw new EasyJob.Tools.Exceptions.RetRentOrderIsExistsException();//归还主单号已经存在
            }
        }

        /// <summary>
        /// 添加归还单主表
        /// </summary>
        /// <param name="retRentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(RetRentOrder retRentOrder)
        {
            return Json(retRentOrderOper.Add(retRentOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在归还主单号
                    IsExistsCode(session, retRentOrder);
                }
                ));
        }


        /// <summary>
        /// 修改归还单主表
        /// </summary>
        /// <param name="retRentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RetRentOrder retRentOrder)
        {
            return Json(retRentOrderOper.Update(retRentOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在归还主单号
                    IsExistsCode(session, retRentOrder);
                }
                ));
        }

        /// <summary>
        /// 删除归还单主表
        /// </summary>
        /// <param name="retRentOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RetRentOrder retRentOrder)
        {
            return Json(retRentOrderOper.Del(retRentOrder));
        }

        /// <summary>
        /// 查询归还单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string retRentOrderCode, string storeHouseID, string borrID, string borrDeptID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(retRentOrderOper.Get(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(retRentOrderCode)) {
                        ICriterion criterion = Restrictions.Like("RetRentOrderCode", retRentOrderCode, MatchMode.Anywhere);
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
                        ICriterion criterion = Restrictions.Eq("BorrDeptID", PojoUtil.InitPojo<Department>(borrDeptID));
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
                    criteria.AddOrder(Order.Asc("RetRentOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询归还单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retRentOrderCode, string storeHouseID, string borrID, string borrDeptID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(retRentOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(retRentOrderCode)) {
                        ICriterion criterion = Restrictions.Like("RetRentOrderCode", retRentOrderCode, MatchMode.Anywhere);
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
                        ICriterion criterion = Restrictions.Eq("BorrDeptID", PojoUtil.InitPojo<Department>(borrDeptID));
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
