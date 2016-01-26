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
using Tools;

namespace EasyJob.Controllers.Api
{
    /// <summary>
    /// 小区接口
    /// </summary>
    public class GardenController : ApiPowerController
    {
        private TbBaseOper<Garden> gardenOper = null;

        public GardenController()
            :base()
        {
            gardenOper = new TbBaseOper<Garden>(HibernateOper, typeof(Garden));
        }

        /// <summary>
        /// 添加小区
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Garden garden)
        {
            //根据地址码获取地址
            garden.Addr = PojoUtil.GetAddrForCode(HibernateOper,garden.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(garden.Addr + garden.Location);
            if (loc != null)
            {
                garden.Lat = loc.lat;
                garden.Lng = loc.lng;
            }
            return Json(gardenOper.Add(garden));
        }


        /// <summary>
        /// 修改小区
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Garden garden)
        {
            //根据地址码获取地址
            garden.Addr = PojoUtil.GetAddrForCode(HibernateOper, garden.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(garden.Addr + garden.Location);
            if (loc != null)
            {
                garden.Lat = loc.lat;
                garden.Lng = loc.lng;
            }
            return Json(gardenOper.Update(garden));
        }

        /// <summary>
        /// 删除小区
        /// </summary>
        /// <param name="garden"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Garden garden)
        {
            return Json(gardenOper.Del(garden));
        }

        /// <summary>
        /// 查询小区
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum,string name,string addr)
        {
            return Json(gardenOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (addr != null)
                    {
                        ICriterion criterion = Restrictions.Like("Addr", addr,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Desc("ModDate"));
                    criteria.AddOrder(Order.Asc("AddrCode"));
                    //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
                    //            criteria.Add(criterion);
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询小区页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize,string name, string addr)
        {
            return Json(gardenOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (addr != null)
                    {
                        ICriterion criterion = Restrictions.Like("Addr", addr, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
                    //            criteria.Add(criterion);
                }
                , pageSize));
        }

    }
}
