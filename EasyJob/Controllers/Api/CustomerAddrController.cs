using EasyJob.Consts;
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
    public class CustomerAddrController : ApiPowerController
    {
        private TbBaseOper<CustomerAddr> customerAddrOper = null;

        public CustomerAddrController()
            :base()
        {
            customerAddrOper = new TbBaseOper<CustomerAddr>(HibernateOper, typeof(CustomerAddr));
        }

        /// <summary>
        /// 客户的地址
        /// </summary>
        /// <returns></returns>
        public ActionResult Get(Customer cstmr)
        {
            IList<CustomerAddr> retVal = null;

            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = HibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                //查找客户ID
                string openId = cstmr.OpenId;
                cstmr = PojoUtil.GetCust(s, openId);
                if (cstmr == null)
                {
                    //客户不存在
                    throw new Exceptions.CstmrIsNotExistsException();
                }
                else
                {
                    //查找客户的所有地址
                    ICriteria criteria = s.CreateCriteria(typeof(CustomerAddr));

                    criteria.Add(Restrictions.Eq("Cstmr", cstmr));

                    retVal = criteria.List<CustomerAddr>();
                }

                trans.Commit();
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }

            return Json(retVal);
        }

        /// <summary>
        /// 添加客户的地址
        /// </summary>
        /// <param name="cstmrAddr"></param>
        /// <returns></returns>
        public ActionResult Add(CustomerAddr cstmrAddr)
        {
            CustomerAddr retVal = null;

            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = HibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                //查找客户ID
                if (cstmrAddr.AddrCode != null && !cstmrAddr.AddrCode.Equals(""))
                {
                    cstmrAddr.Addr = PojoUtil.GetAddrForCode(s, cstmrAddr.AddrCode);

                    LocationUtil.Location loc = LocationUtil.GetLocation(cstmrAddr.Addr+cstmrAddr.Location);
                    if (loc != null)
                    {
                        cstmrAddr.Lat = loc.lat;
                        cstmrAddr.Lng = loc.lng;
                    }
                }
                string openId = cstmrAddr.Cstmr.OpenId;
                cstmrAddr.Cstmr = PojoUtil.GetCust(s, openId);
                if (cstmrAddr.Cstmr == null)
                {
                    if (openId == null || openId.Equals(""))
                    {
                        //客户不存在
                        throw new Exceptions.CstmrIsNotExistsException();
                    }

                    Customer cstmr = new Customer();
                    cstmr.OpenId = openId;
                    s.Save(cstmr);
                    cstmrAddr.Cstmr = cstmr;
                }

                s.Save(cstmrAddr);

                trans.Commit();

                retVal = cstmrAddr;
            }
            catch (Exception e)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw e;
            }

            return Json(retVal);
        }

    }
}
