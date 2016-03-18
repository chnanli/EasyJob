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
    /// 盘点单主表接口
    /// </summary>
    public class CheckStockController : ApiPowerController
    {
        private TbBaseOper<CheckStock> checkStockOper = null;

        public CheckStockController()
            : base()
        {
            checkStockOper = new TbBaseOper<CheckStock>(HibernateOper, typeof(CheckStock));
        }

        private void IsExistsCode(ISession session, CheckStock check)
        {
            ICriteria criteria = session.CreateCriteria(typeof(ReqOrder));

            ICriterion criterion = null;
            if (check.Id != Guid.Empty) {
                criterion = Restrictions.Not(Restrictions.IdEq(check.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("CheckStockCode", check.CheckStockCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0) {
                throw new EasyJob.Tools.Exceptions.CheckStockIsExistsException();//盘点主单号已经存在
            }
        }

        /// <summary>
        /// 添加盘点单主表
        /// </summary>
        /// <param name="checkStock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(CheckStock checkStock)
        {
            return Json(checkStockOper.Add(checkStock,
                delegate(object sender, ISession session) {
                    //判断是否存在盘点主单号
                    IsExistsCode(session, checkStock);
                }
                ));
        }


        /// <summary>
        /// 修改盘点单主表
        /// </summary>
        /// <param name="checkStock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(CheckStock checkStock)
        {
            return Json(checkStockOper.Update(checkStock,
                delegate(object sender, ISession session) {
                    //判断是否存在盘点主单号
                    IsExistsCode(session, checkStock);
                }
                ));
        }

        /// <summary>
        /// 删除盘点单主表
        /// </summary>
        /// <param name="checkStock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(CheckStock checkStock)
        {
            return Json(checkStockOper.Del(checkStock));
        }

        /// <summary>
        /// 查询盘点单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string checkStockCode, string storeHouseID, string retOrderStoreHouseID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(checkStockOper.Get(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(checkStockCode)) {
                        ICriterion criterion = Restrictions.Like("CheckStockCode", checkStockCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(retOrderStoreHouseID)) {
                        ICriterion criterion = Restrictions.Eq("RetOrderStoreHouseID", PojoUtil.InitPojo<Storehouse>(retOrderStoreHouseID));
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
                    criteria.AddOrder(Order.Asc("CheckStockCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询盘点单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string checkStockCode, string storeHouseID, string oPerTime, string approvalID, string approvalTime, string state)
        {
            return Json(checkStockOper.GetPageCount(
                delegate(object sender, ICriteria criteria) {
                    if (!string.IsNullOrEmpty(checkStockCode)) {
                        ICriterion criterion = Restrictions.Like("CheckStockCode", checkStockCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeHouseID)) {
                        ICriterion criterion = Restrictions.Eq("StoreHouseID", PojoUtil.InitPojo<Storehouse>(storeHouseID));
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
