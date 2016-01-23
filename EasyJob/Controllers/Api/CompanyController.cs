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
    /// 公司接口
    /// </summary>
    public class CompanyController : ApiPowerController
    {
        private TbBaseOper<Company> companyOper = null;

        public CompanyController()
            :base()
        {
            companyOper = new TbBaseOper<Company>(HibernateOper, typeof(Company));
        }

        /// <summary>
        /// 添加公司
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Company company)
        {
            //根据地址码获取地址
            company.Addr = PojoUtil.GetAddrForCode(HibernateOper,company.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(company.Addr + company.Location);
            if (loc != null)
            {
                company.Lat = loc.lat;
                company.Lng = loc.lng;
            }
            return Json(companyOper.Add(company));
        }


        /// <summary>
        /// 修改公司
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Company company)
        {
            //根据地址码获取地址
            company.Addr = PojoUtil.GetAddrForCode(HibernateOper, company.AddrCode);
            LocationUtil.Location loc = LocationUtil.GetLocation(company.Addr + company.Location);
            if (loc != null)
            {
                company.Lat = loc.lat;
                company.Lng = loc.lng;
            }
            return Json(companyOper.Update(company));
        }

        /// <summary>
        /// 删除公司
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Company company)
        {
            return Json(companyOper.Del(company));
        }

        /// <summary>
        /// 查询公司
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get()
        {
            return Json(companyOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                }
                , 1, 1));
        }

        /// <summary>
        /// 查询公司页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount()
        {
            return Json(companyOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                }
                , 1));
        }

    }
}
