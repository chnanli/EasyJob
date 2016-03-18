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
    /// 销售单主表接口
    /// </summary>
    public class SellOrderController : ApiPowerController
    {
        private TbBaseOper<SellOrder> sellOrderOper = null;

        public SellOrderController()
            : base()
        {
            sellOrderOper = new TbBaseOper<SellOrder>(HibernateOper, typeof(SellOrder));
        }

        private void IsExistsCode(ISession session, SellOrder sellOrder)
        {
            ICriteria criteria = session.CreateCriteria(typeof(SellOrder));

            ICriterion criterion = null;
            if (sellOrder.Id != Guid.Empty) {
                criterion = Restrictions.Not(Restrictions.IdEq(sellOrder.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("SellOrderCode", sellOrder.SellOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0) {
                throw new EasyJob.Tools.Exceptions.RetRentOrderIsExistsException();//销售主单号已经存在
            }
        }

        /// <summary>
        /// 添加销售单主表
        /// </summary>
        /// <param name="sellOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(SellOrder sellOrder)
        {
            return Json(sellOrderOper.Add(sellOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在销售主单号
                    IsExistsCode(session, sellOrder);
                }
                ));
        }


        /// <summary>
        /// 修改销售单主表
        /// </summary>
        /// <param name="sellOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(SellOrder sellOrder)
        {
            return Json(sellOrderOper.Update(sellOrder,
                delegate(object sender, ISession session) {
                    //判断是否存在销售主单号
                    IsExistsCode(session, sellOrder);
                }
                ));
        }

        /// <summary>
        /// 删除销售单主表
        /// </summary>
        /// <param name="sellOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(SellOrder sellOrder)
        {
            return Json(sellOrderOper.Del(sellOrder));
        }

        /// <summary>
        /// 查询销售单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string sellOrderCode, string storeHouseID, string salespersonID, string salespersonDeptID, string sellDateTime, string totalMoney)
        {
            return Json(sellOrderOper.Get(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(sellOrderCode)) {
                        ICriterion criterion = Restrictions.Like("SellOrderCode", sellOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(salespersonID)) {
                        ICriterion criterion = Restrictions.Eq("SalespersonID", PojoUtil.InitPojo<Employee>(salespersonID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(salespersonDeptID)) {
                        ICriterion criterion = Restrictions.Eq("SalespersonDeptID", PojoUtil.InitPojo<Department>(salespersonDeptID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sellDateTime)) {
                        ICriterion criterion = Restrictions.Like("SellDateTime", sellDateTime, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(totalMoney)) {
                        ICriterion criterion = Restrictions.Like("TotalMoney", totalMoney, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("SellOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询销售单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string sellOrderCode, string storeHouseID, string salespersonID, string salespersonDeptID, string sellDateTime, string totalMoney)
        {
            return Json(sellOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(sellOrderCode)) {
                        ICriterion criterion = Restrictions.Like("SellOrderCode", sellOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(salespersonID)) {
                        ICriterion criterion = Restrictions.Eq("SalespersonID", PojoUtil.InitPojo<Employee>(salespersonID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(salespersonDeptID)) {
                        ICriterion criterion = Restrictions.Eq("SalespersonDeptID", PojoUtil.InitPojo<Department>(salespersonDeptID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sellDateTime)) {
                        ICriterion criterion = Restrictions.Like("SellDateTime", sellDateTime, MatchMode.Anywhere);
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
