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
    /// 库存表接口
    /// </summary>
    public class StockController : ApiPowerController
    {
        private TbBaseOper<Stock> stockOper = null;

        public StockController()
            :base()
        {
            stockOper = new TbBaseOper<Stock>(HibernateOper, typeof(Stock));
        }

        private void IsExists(ISession session, Stock stock)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Stock));

            ICriterion criterion = null;
            if (stock.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(stock.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Storehouse", stock.Storehouse);
            criteria.Add(criterion);

            criterion = Restrictions.Eq("Goods", stock.Goods);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.StockIsExistsException();//库存表存在
            }
        }

        /// <summary>
        /// 添加库存表
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Stock stock)
        {
            return Json(stockOper.Add(stock,
                delegate(object sender, ISession session)
                {
                    //判断是否存在库存数据
                    IsExists(session, stock);
                }
                ));
        }


        /// <summary>
        /// 修改库存表
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Stock stock)
        {
            return Json(stockOper.Update(stock,
                delegate(object sender, ISession session)
                {
                    //判断是否存在库存数据
                    IsExists(session, stock);
                }
                ));
        }

        /// <summary>
        /// 删除库存表
        /// </summary>
        /// <param name="stock"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Stock stock)
        {
            return Json(stockOper.Del(stock));
        }

        /// <summary>
        /// 查询库存表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string storehouseId, string goodsId)
        {
            return Json(stockOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(storehouseId))
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<Storehouse>(storehouseId));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsId))
                    {
                        ICriterion criterion = Restrictions.Eq("Goods", PojoUtil.InitPojo<GoodsInfo>(goodsId));
                        criteria.Add(criterion);
                    }

                    //创建对象属性别名
                    criteria.CreateAlias("Storehouse", "sh");
                    criteria.AddOrder(Order.Asc("sh.StoreName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询库存表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string storehouseId, string goodsId)
        {
            return Json(stockOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(storehouseId))
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<Storehouse>(storehouseId));
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(goodsId))
                    {
                        ICriterion criterion = Restrictions.Eq("Goods", PojoUtil.InitPojo<GoodsInfo>(goodsId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
