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
    /// 仓库登记接口
    /// </summary>
    public class StorehouseController : ApiPowerController
    {
        private TbBaseOper<Storehouse> storehouseOper = null;

        public StorehouseController()
            : base()
        {
            storehouseOper = new TbBaseOper<Storehouse>(HibernateOper, typeof(Storehouse));
        }

        private void IsExistsCode(ISession session, Storehouse sh)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Storehouse));

            ICriterion criterion = null;
            if (sh.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(sh.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("StoreCode", sh.StoreCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.StorehouseCodeIsExistsException();//库存Code已经存在
            }
        }

        /// <summary>
        /// 添加仓库登记
        /// </summary>
        /// <param name="storehouse"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Storehouse storehouse)
        {
            //根据地址码获取地址
            storehouse.Addr = PojoUtil.GetAddrForCode(HibernateOper, storehouse.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(storehouse.Addr + storehouse.Location);
            if (loc != null)
            {
                storehouse.Lat = loc.lat;
                storehouse.Lng = loc.lng;
            }
            return Json(storehouseOper.Add(storehouse,
                delegate(object sender, ISession session)
                {
                    //判断是否存在库存Code
                    IsExistsCode(session, storehouse);
                }
                ));
        }


        /// <summary>
        /// 修改仓库登记
        /// </summary>
        /// <param name="storehouse"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Storehouse storehouse)
        {
            //根据地址码获取地址
            storehouse.Addr = PojoUtil.GetAddrForCode(HibernateOper, storehouse.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(storehouse.Addr + storehouse.Location);
            if (loc != null)
            {
                storehouse.Lat = loc.lat;
                storehouse.Lng = loc.lng;
            }
            return Json(storehouseOper.Update(storehouse,
                delegate(object sender, ISession session)
                {
                    //判断是否存在库存Code
                    IsExistsCode(session, storehouse);
                }
                ));
        }

        /// <summary>
        /// 删除仓库登记
        /// </summary>
        /// <param name="storehouse"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Storehouse storehouse)
        {
            return Json(storehouseOper.Del(storehouse));
        }

        /// <summary>
        /// 查询仓库登记
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string storeCode, string storeName, string deptID)
        {
            return Json(storehouseOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(storeCode))
                    {
                        ICriterion criterion = Restrictions.Like("StoreCode", storeCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeName))
                    {
                        ICriterion criterion = Restrictions.Like("StoreName", storeName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(deptID))
                    {
                        ICriterion criterion = Restrictions.Eq("Dept", PojoUtil.InitPojo<Department>(deptID));
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("StoreName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询仓库登记页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string storeCode, string storeName, string deptID)
        {
            return Json(storehouseOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(storeCode))
                    {
                        ICriterion criterion = Restrictions.Like("StoreCode", storeCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(storeName))
                    {
                        ICriterion criterion = Restrictions.Like("StoreName", storeName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(deptID))
                    {
                        ICriterion criterion = Restrictions.Eq("Dept", PojoUtil.InitPojo<Department>(deptID));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
