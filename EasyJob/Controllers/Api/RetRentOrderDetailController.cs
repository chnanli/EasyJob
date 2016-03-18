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
    /// 归还单明细表接口
    /// </summary>
    public class RetRentOrderDetailController : ApiPowerController
    {
        private TbBaseOper<RetRentOrderDetail> retRentOrderDetailOper = null;

        public RetRentOrderDetailController()
            : base()
        {
            retRentOrderDetailOper = new TbBaseOper<RetRentOrderDetail>(HibernateOper, typeof(RetRentOrderDetail));
        }

        private void IsExistsCode(ISession session, RetRentOrderDetail retRentOrderDetail)
        {
            ICriteria criteria = session.CreateCriteria(typeof(RetRentOrderDetail));

            ICriterion criterion = null;
            if (retRentOrderDetail.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(retRentOrderDetail.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("RetRentIOrderD", retRentOrderDetail.RetRentOrder);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.RetRentOrderIsExistsException();//归还明细单号已经存在
            }
        }

        /// <summary>
        /// 添加归还单明细表
        /// </summary>
        /// <param name="retOrderDetail"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(RetRentOrderDetail retRentOrderDetail)
        {
            return Json(retRentOrderDetailOper.Add(retRentOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在归还明细单号
                    IsExistsCode(session, retRentOrderDetail);
                }
                ));
        }


        /// <summary>
        /// 修改归还单明细表
        /// </summary>
        /// <param name="purOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(RetRentOrderDetail retRentOrderDetail)
        {
            return Json(retRentOrderDetailOper.Update(retRentOrderDetail,
                delegate(object sender, ISession session)
                {
                    //判断是否存在归还明细单号
                    IsExistsCode(session, retRentOrderDetail);
                }
                ));
        }

        /// <summary>
        /// 删除归还单明细表
        /// </summary>
        /// <param name="retOrder"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(RetRentOrderDetail retRentOrderDetail)
        {
            return Json(retRentOrderDetailOper.Del(retRentOrderDetail));
        }

        /// <summary>
        /// 查询归还单明细表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string retRentIOrderD, string goodsID, string qty)
        {
            return Json(retRentOrderDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retRentIOrderD))
                    {
                        ICriterion criterion = Restrictions.Eq("RetRentIOrderD", PojoUtil.InitPojo<RetRentOrder>(retRentIOrderD));
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
                    criteria.AddOrder(Order.Asc("RetRentIOrderD"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询归还单明细表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string retRentIOrderD, string goodsID, string qty)
        {
            return Json(retRentOrderDetailOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(retRentIOrderD)) {
                        ICriterion criterion = Restrictions.Eq("RetRentIOrderD", PojoUtil.InitPojo<RetRentOrder>(retRentIOrderD));
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
