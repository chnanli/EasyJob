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
    /// 盘点单明细表接口
    /// </summary>
    public class CheckStockDetailController : ApiPowerController
    {
        private TbBaseOper<CheckStockDetail> checkStockDetailOper = null;

        public CheckStockDetailController()
            : base()
        {
            checkStockDetailOper = new TbBaseOper<CheckStockDetail>(HibernateOper, typeof(CheckStockDetail));
        }

        private void IsExistsCode(ISession session, CheckStockDetail checkStockDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(CheckStockDetail));

            ICriterion criterion = null;
            if (checkStockDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(checkStockDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("CheckStockID", checkStockDetail.CheckStock);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.CheckStockIsExistsException();//盘点明细单号已经存在
            }
        }

        /// <summary>
        /// 添加盘点单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(CheckStockDetail checkStockDetail)
        {
            return Json(checkStockDetailOper.Add(checkStockDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在盘点明细单号
                    IsExistsCode(session, checkStockDetail);
                }
                ));
        }


        /// <summary>
        /// 修改盘点单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(CheckStockDetail checkStockDetail)
        {
            return Json(checkStockDetailOper.Update(checkStockDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在盘点明细单号
                    IsExistsCode(session, checkStockDetail);
                }
                ));
        }

        /// <summary>
        /// 删除盘点单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(CheckStockDetail checkStockDetail)
        {
            return Json(checkStockDetailOper.Del(checkStockDetail));
        }

        /// <summary>
        /// 查询盘点单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string checkStockID, string goodsID, string sysQty, string qty, string profitLoss)
        {
            return Json(checkStockDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(checkStockID))
                    {
                        ICriterion criterion = Restrictions.Eq("CheckStockID", PojoUtil.InitPojo<CheckStock>(checkStockID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID))
                    {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sysQty))
                    {
                        ICriterion criterion = Restrictions.Like("SysQty", sysQty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty))
                    {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(profitLoss)) {
                        ICriterion criterion = Restrictions.Like("ProfitLoss", profitLoss, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("CheckStockID"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询盘点单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string checkStockID, string goodsID, string sysQty, string qty, string profitLoss)
        {
            return Json(checkStockDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(checkStockID)) {
                        ICriterion criterion = Restrictions.Eq("CheckStockID", PojoUtil.InitPojo<CheckStock>(checkStockID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsID)) {
                        ICriterion criterion = Restrictions.Eq("GoodsID", PojoUtil.InitPojo<GoodsInfo>(goodsID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(sysQty)) {
                        ICriterion criterion = Restrictions.Like("SysQty", sysQty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(qty)) {
                        ICriterion criterion = Restrictions.Like("Qty", qty, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(profitLoss)) {
                        ICriterion criterion = Restrictions.Like("ProfitLoss", profitLoss, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
