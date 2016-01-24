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
    /// 库存警告设置表接口
    /// </summary>
    public class StorageAlarmController : ApiPowerController
    {
        private TbBaseOper<StorageAlarm> storageAlarmOper = null;

        public StorageAlarmController()
            :base()
        {
            storageAlarmOper = new TbBaseOper<StorageAlarm>(HibernateOper, typeof(StorageAlarm));
        }

        /// <summary>
        /// 添加库存警告设置表
        /// </summary>
        /// <param name="storageAlarm"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(StorageAlarm storageAlarm)
        {
            return Json(storageAlarmOper.Add(storageAlarm));
        }


        /// <summary>
        /// 修改库存警告设置表
        /// </summary>
        /// <param name="storageAlarm"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(StorageAlarm storageAlarm)
        {
            return Json(storageAlarmOper.Update(storageAlarm));
        }

        /// <summary>
        /// 删除库存警告设置表
        /// </summary>
        /// <param name="storageAlarm"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(StorageAlarm storageAlarm)
        {
            return Json(storageAlarmOper.Del(storageAlarm));
        }

        /// <summary>
        /// 查询库存警告设置表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string storehouseId, string goodsId)
        {
            return Json(storageAlarmOper.Get(
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
        /// 查询库存警告设置表页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string storehouseId, string goodsId)
        {
            return Json(storageAlarmOper.GetPageCount(
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
