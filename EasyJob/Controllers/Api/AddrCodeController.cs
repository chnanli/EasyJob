using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using NHibernate;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EasyJob.Controllers.Api
{
    public class AddrCodeController : ApiPowerController
    {
        private TbBaseOper<AddrCode> addrCodeOper = null;

        public AddrCodeController()
            :base()
        {
            addrCodeOper = new TbBaseOper<AddrCode>(HibernateOper, typeof(AddrCode));
        }

        /// <summary>
        /// 得到地址码的级别
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        private int GetCodeLevel(string code)
        {
            int retVal = 0;
            if (code == null || code.Equals(""))
            {
                retVal = 0;
            }
            else
            {
                //如果长度不是2的倍数
                if (code.Length % 2 != 0)
                {
                    retVal = -1;
                }
                else
                {
                    retVal = code.Length / 2;
                }
            }
            return retVal;
        }

        //查找地址码
        public ActionResult Search(string code)
        {
            //得到地址码的级别
            int level = GetCodeLevel(code);

            IList<AddrCode> retVal=null;
            if (level >= 0)
            {
                retVal = addrCodeOper.Get(
                    delegate(object sender, ICriteria criteria)
                    {
                        //模糊查找当前代码
                        ICriterion criterion = Restrictions.Like("Code", code+"__%");
                        criteria.Add(criterion);
                        //查找当前代码的一下级
                        criterion = Restrictions.Eq("Level", (byte)(level + 1));
                        criteria.Add(criterion);

                        criteria.AddOrder(Order.Asc("Code"));
                    }
                );
            }
            else
            {
                retVal = new List<AddrCode>();
            }

            return Json(retVal);
        }

    }
}
