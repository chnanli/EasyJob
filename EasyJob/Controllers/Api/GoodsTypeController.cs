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
    /// 商品类别表接口
    /// </summary>
    public class GoodsTypeController : ApiPowerController
    {
        private TbBaseOper<GoodsType> goodsTypeOper = null;

        public GoodsTypeController()
            :base()
        {
            goodsTypeOper = new TbBaseOper<GoodsType>(HibernateOper, typeof(GoodsType));
        }

        /// <summary>
        /// 添加商品类别表
        /// </summary>
        /// <param name="goodsType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(GoodsType goodsType)
        {
            return Json(goodsTypeOper.Add(goodsType));
        }


        /// <summary>
        /// 修改商品类别表
        /// </summary>
        /// <param name="goodsType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(GoodsType goodsType)
        {
            return Json(goodsTypeOper.Update(goodsType));
        }

        /// <summary>
        /// 删除商品类别表
        /// </summary>
        /// <param name="goodsType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(GoodsType goodsType)
        {
            return Json(goodsTypeOper.Del(goodsType));
        }

        /// <summary>
        /// 查询商品类别表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string pId, string unitDesc, string goodsTypeCode, string goodsTypeName)
        {
            return Json(goodsTypeOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (unitDesc != null)
                    {
                        ICriterion criterion = Restrictions.Like("UnitDesc", unitDesc, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsTypeCode", goodsTypeCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeName != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsTypeName", goodsTypeName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("GoodsTypeCode"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询商品类别表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string pId, string unitDesc, string goodsTypeCode, string goodsTypeName)
        {
            return Json(goodsTypeOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (unitDesc != null)
                    {
                        ICriterion criterion = Restrictions.Like("UnitDesc", unitDesc, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsTypeCode", goodsTypeCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (goodsTypeName != null)
                    {
                        ICriterion criterion = Restrictions.Like("GoodsTypeName", goodsTypeName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
