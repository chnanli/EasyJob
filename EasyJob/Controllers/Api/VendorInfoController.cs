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
    /// 供应商资料接口
    /// </summary>
    public class VendorInfoController : ApiPowerController
    {
        private TbBaseOper<VendorInfo> vendorInfoOper = null;

        public VendorInfoController()
            : base()
        {
            vendorInfoOper = new TbBaseOper<VendorInfo>(HibernateOper, typeof(VendorInfo));
        }

        private void IsExistsCode(ISession session, VendorInfo vi)
        {
            ICriteria criteria = session.CreateCriteria(typeof(VendorInfo));

            ICriterion criterion = null;
            if (vi.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(vi.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("VendorCode", vi.VendorCode);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.VendorInfoCodeIsExistsException();//供应商Code已经存在
            }
        }

        /// <summary>
        /// 添加供应商资料
        /// </summary>
        /// <param name="vendorInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(VendorInfo vendorInfo)
        {
            //根据地址码获取地址
            vendorInfo.Addr = PojoUtil.GetAddrForCode(HibernateOper, vendorInfo.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(vendorInfo.Addr + vendorInfo.Location);
            if (loc != null)
            {
                vendorInfo.Lat = loc.lat;
                vendorInfo.Lng = loc.lng;
            }
            return Json(vendorInfoOper.Add(vendorInfo,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, vendorInfo);
                }
                ));
        }


        /// <summary>
        /// 修改供应商资料
        /// </summary>
        /// <param name="vendorInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(VendorInfo vendorInfo)
        {
            //根据地址码获取地址
            vendorInfo.Addr = PojoUtil.GetAddrForCode(HibernateOper, vendorInfo.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(vendorInfo.Addr + vendorInfo.Location);
            if (loc != null)
            {
                vendorInfo.Lat = loc.lat;
                vendorInfo.Lng = loc.lng;
            }
            return Json(vendorInfoOper.Update(vendorInfo,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, vendorInfo);
                }
                ));
        }

        /// <summary>
        /// 删除供应商资料
        /// </summary>
        /// <param name="vendorInfo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(VendorInfo vendorInfo)
        {
            return Json(vendorInfoOper.Del(vendorInfo));
        }

        /// <summary>
        /// 查询供应商资料
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string vendorCode, string vendorName)
        {
            return Json(vendorInfoOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (vendorCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("VendorCode", vendorCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (vendorName != null)
                    {
                        ICriterion criterion = Restrictions.Like("VendorName", vendorName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("VendorName"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询供应商资料页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string vendorCode, string vendorName)
        {
            return Json(vendorInfoOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (vendorCode != null)
                    {
                        ICriterion criterion = Restrictions.Like("VendorCode", vendorCode, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (vendorName != null)
                    {
                        ICriterion criterion = Restrictions.Like("VendorName", vendorName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
