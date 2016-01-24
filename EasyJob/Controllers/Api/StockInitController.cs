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
    /// 仓库初始化接口
    /// </summary>
    public class StockInitController : ApiPowerController
    {
        private TbBaseOper<StockInit> stockInitOper = null;

        public StockInitController()
            :base()
        {
            stockInitOper = new TbBaseOper<StockInit>(HibernateOper, typeof(StockInit));
        }

        /// <summary>
        /// 添加仓库初始化
        /// </summary>
        /// <param name="stockInit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(StockInit stockInit)
        {
            return Json(stockInitOper.Add(stockInit));
        }


        /// <summary>
        /// 修改仓库初始化
        /// </summary>
        /// <param name="stockInit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(StockInit stockInit)
        {
            return Json(stockInitOper.Update(stockInit));
        }

        /// <summary>
        /// 删除仓库初始化
        /// </summary>
        /// <param name="stockInit"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(StockInit stockInit)
        {
            return Json(stockInitOper.Del(stockInit));
        }

        /// <summary>
        /// 查询仓库初始化
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string storehouseId, string goodsId)
        {
            return Json(stockInitOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (storehouseId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<Storehouse>(storehouseId));
                        criteria.Add(criterion);
                    }
                    if (goodsId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<GoodsInfo>(goodsId));
                        criteria.Add(criterion);
                    }

                    //创建对象属性别名
                    criteria.CreateAlias("Storehouse", "sh");
                    criteria.AddOrder(Order.Asc("sh.StoreName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询仓库初始化页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string storehouseId, string goodsId)
        {
            return Json(stockInitOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (storehouseId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<Storehouse>(storehouseId));
                        criteria.Add(criterion);
                    }
                    if (goodsId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Storehouse", PojoUtil.InitPojo<GoodsInfo>(goodsId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
