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
    /// 商品资料表接口
    /// </summary>
    public class GoodsInfoController : ApiPowerController
    {
        private TbBaseOper<GoodsInfo> goodsInfoOper = null;

        public GoodsInfoController()
            : base()
        {
            goodsInfoOper = new TbBaseOper<GoodsInfo>(HibernateOper, typeof(GoodsInfo));
        }

        /// <summary>
        /// 添加商品资料表
        /// </summary>
        /// <param name="goodsInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(GoodsInfo goodsInfo)
        {
            return Json(goodsInfoOper.Add(goodsInfo));
        }


        /// <summary>
        /// 修改商品资料表
        /// </summary>
        /// <param name="goodsInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(GoodsInfo goodsInfo)
        {
            return Json(goodsInfoOper.Update(goodsInfo));
        }

        /// <summary>
        /// 删除商品资料表
        /// </summary>
        /// <param name="goodsInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(GoodsInfo goodsInfo)
        {
            return Json(goodsInfoOper.Del(goodsInfo));
        }

        /// <summary>
        /// 查询商品资料表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string goodsCode, string goodsName, string barCode, string goodsTypeID, string modelNum)
        {
            return Json(goodsInfoOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (goodsCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsCode", goodsCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsName != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsName", goodsName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (barCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("BarCode", barCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeID != null)
                    {
                        GoodsType gt = new GoodsType();
                        gt.Id = new Guid(goodsTypeID);
                        ICriterion criterion = Restrictions.Eq("GoodsType", gt);
                        criteria.Add(criterion);
                    }
                    if (modelNum != null)
                    {
                        ICriterion criterion = Restrictions.Like("ModelNum", modelNum, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("GoodsName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询商品资料表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string goodsCode, string goodsName, string barCode, string goodsTypeID, string modelNum)
        {
            return Json(goodsInfoOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (goodsCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsCode", goodsCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsName != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsName", goodsName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (barCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("BarCode", barCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeID != null)
                    {
                        GoodsType gt = new GoodsType();
                        gt.Id = new Guid(goodsTypeID);
                        ICriterion criterion = Restrictions.Eq("GoodsType", gt);
                        criteria.Add(criterion);
                    }
                    if (modelNum != null)
                    {
                        ICriterion criterion = Restrictions.Like("ModelNum", modelNum, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
