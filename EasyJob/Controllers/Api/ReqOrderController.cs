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
    /// 调拨单主表接口
    /// </summary>
    public class ReqOrderController : ApiPowerController
    {
        private TbBaseOper<ReqOrder> reqOrderOper = null;

       public ReqOrderController()
            : base()
        {
            reqOrderOper = new TbBaseOper<ReqOrder>(HibernateOper, typeof(ReqOrder));
        }

       private void IsExistsCode(ISession session, ReqOrder req)
        {
            ICriteria criteria = session.CreateCriteria(typeof(ReqOrder));

            ICriterion criterion = null;
            if (req.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(req.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("ReqOrderCode", req.ReqOrderCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.ReqOrderIsExistsException();//调拨主单号已经存在
            }
        }

        /// <summary>
       /// 添加调拨单主表
        /// </summary>
        /// <param name="reqOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
       public ActionResult Add(ReqOrder reqOrder)
        {
            return Json(reqOrderOper.Add(reqOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在调拨主单号
                    IsExistsCode(session, reqOrder);
                }
                ));
        }


        /// <summary>
        /// 修改调拨单主表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(ReqOrder reqOrder)
        {
            return Json(reqOrderOper.Update(reqOrder,
                delegate(object sender, ISession session)
                {
                    //判断是否存在调拨主单号
                    IsExistsCode(session, reqOrder);
                }
                ));
        }

        /// <summary>
        /// 删除调拨单主表
        /// </summary>
        /// <param name="reqOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(ReqOrder reqOrder)
        {
            return Json(reqOrderOper.Del(reqOrder));
        }

        /// <summary>
        /// 查询调拨单主表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string reqOrderCode, string srcHouseID, string destHouseID, string carrier, string approvalID, string oPerTime, string approvalTime, string state)
        {
            return Json(reqOrderOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(reqOrderCode))
                    {
                        ICriterion criterion = Restrictions.Like("ReqOrderCode", reqOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(srcHouseID))
                    {
                        ICriterion criterion = Restrictions.Eq("SrcHouseID", PojoUtil.InitPojo<Storehouse>(srcHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(destHouseID))
                    {
                        ICriterion criterion = Restrictions.Eq("DestHouseID", PojoUtil.InitPojo<Storehouse>(destHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(carrier))
                    {
                        ICriterion criterion = Restrictions.Like("Carrier", carrier, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalID))
                    {
                        ICriterion criterion = Restrictions.Eq("ApprovalID", PojoUtil.InitPojo<Employee>(approvalID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(oPerTime)) {
                        ICriterion criterion = Restrictions.Like("OPerTime", oPerTime, MatchMode.Anywhere);
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
                    criteria.AddOrder(Order.Asc("ReqOrderCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询调拨单主表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string reqOrderCode, string srcHouseID, string destHouseID, string carrier, string approvalID, string oPerTime, string approvalTime, string state)
        {
            return Json(reqOrderOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(reqOrderCode)) {
                        ICriterion criterion = Restrictions.Like("ReqOrderCode", reqOrderCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(srcHouseID)) {
                        ICriterion criterion = Restrictions.Eq("SrcHouseID", PojoUtil.InitPojo<Storehouse>(srcHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(destHouseID)) {
                        ICriterion criterion = Restrictions.Eq("DestHouseID", PojoUtil.InitPojo<Storehouse>(destHouseID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(carrier)) {
                        ICriterion criterion = Restrictions.Like("Carrier", carrier, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(approvalID)) {
                        ICriterion criterion = Restrictions.Eq("ApprovalID", PojoUtil.InitPojo<Employee>(approvalID));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(oPerTime)) {
                        ICriterion criterion = Restrictions.Like("OPerTime", oPerTime, MatchMode.Anywhere);
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
