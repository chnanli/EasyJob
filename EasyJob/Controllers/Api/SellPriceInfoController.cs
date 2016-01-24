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
    /// 销售价格管理（商品资料子表）接口
    /// </summary>
    public class SellPriceInfoController : ApiPowerController
    {
        private TbBaseOper<SellPriceInfo> sellPriceInfoOper = null;

        public SellPriceInfoController()
            : base()
        {
            sellPriceInfoOper = new TbBaseOper<SellPriceInfo>(HibernateOper, typeof(SellPriceInfo));
        }

        private void IsExists(ISession session, SellPriceInfo spi)
        {
            ICriteria criteria = session.CreateCriteria(typeof(SellPriceInfo));

            ICriterion criterion = null;
            if (spi.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(spi.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Storehouse", spi.Storehouse);
            criteria.Add(criterion);

            criterion = Restrictions.Eq("Goods", spi.Goods);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.SellPriceInfoIsExistsException();//销售价格管理（商品资料子表）已经存在
            }
        }

        /// <summary>
        /// 添加销售价格管理（商品资料子表）
        /// </summary>
        /// <param name="sellPriceInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(SellPriceInfo sellPriceInfo)
        {
            return Json(sellPriceInfoOper.Add(sellPriceInfo,
                delegate(object sender, ISession session)
                {
                    //判断是否存在销售价格管理（商品资料子表）
                    IsExists(session, sellPriceInfo);
                }
                ));
        }


        /// <summary>
        /// 修改销售价格管理（商品资料子表）
        /// </summary>
        /// <param name="sellPriceInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(SellPriceInfo sellPriceInfo)
        {
            return Json(sellPriceInfoOper.Update(sellPriceInfo,
                delegate(object sender, ISession session)
                {
                    //判断是否存在销售价格管理（商品资料子表）
                    IsExists(session, sellPriceInfo);
                }
                ));
        }

        /// <summary>
        /// 删除销售价格管理（商品资料子表）
        /// </summary>
        /// <param name="sellPriceInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(SellPriceInfo sellPriceInfo)
        {
            return Json(sellPriceInfoOper.Del(sellPriceInfo));
        }

        /// <summary>
        /// 查询销售价格管理（商品资料子表）
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string storehouseID, string goodsID)
        {
            return Json(sellPriceInfoOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (storehouseID != null)
                    {
                        Storehouse s = new Storehouse();
                        s.Id = new Guid(storehouseID);
                        ICriterion criterion = Restrictions.Eq("Storehouse", s);
                        criteria.Add(criterion);
                    }
                    if (goodsID != null)
                    {
                        GoodsInfo gi = new GoodsInfo();
                        gi.Id = new Guid(goodsID);
                        ICriterion criterion = Restrictions.Eq("Goods", gi);
                        criteria.Add(criterion);
                    }
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询销售价格管理（商品资料子表）页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string storehouseID, string goodsID)
        {
            return Json(sellPriceInfoOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (storehouseID != null)
                    {
                        Storehouse s = new Storehouse();
                        s.Id = new Guid(storehouseID);
                        ICriterion criterion = Restrictions.Eq("Storehouse", s);
                        criteria.Add(criterion);
                    }
                    if (goodsID != null)
                    {
                        GoodsInfo gi = new GoodsInfo();
                        gi.Id = new Guid(goodsID);
                        ICriterion criterion = Restrictions.Eq("Goods", gi);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
