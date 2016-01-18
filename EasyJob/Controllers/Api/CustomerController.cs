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
    /// 客户接口
    /// </summary>
    public class CustomerController : ApiPowerController
    {
        private TbBaseOper<Customer> customerOper = null;

        public CustomerController()
            :base()
        {
            customerOper = new TbBaseOper<Customer>(HibernateOper, typeof(Customer));
        }

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Customer customer)
        {
            return Json(customerOper.Add(customer));
        }


        /// <summary>
        /// 修改客户
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Customer customer)
        {
            return Json(customerOper.Update(customer));
        }

        /// <summary>
        /// 删除客户
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Customer customer)
        {
            return Json(customerOper.Del(customer));
        }

        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string name, string nickname, string phoneNum,string tel,string qq,string weiXin,string email)
        {
            return Json(customerOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (nickname != null)
                    {
                        ICriterion criterion = Restrictions.Like("Nickname", nickname, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (phoneNum != null)
                    {
                        ICriterion criterion = Restrictions.Like("PhoneNum", phoneNum, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (tel != null)
                    {
                        ICriterion criterion = Restrictions.Like("Tel", tel, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (qq != null)
                    {
                        ICriterion criterion = Restrictions.Like("QQ", qq, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (weiXin != null)
                    {
                        ICriterion criterion = Restrictions.Like("WeiXin", weiXin, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (email != null)
                    {
                        ICriterion criterion = Restrictions.Like("Email", email, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Desc("ModDate"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询客户页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string name, string nickname, string phoneNum, string tel, string qq, string weiXin, string email)
        {
            return Json(customerOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (nickname != null)
                    {
                        ICriterion criterion = Restrictions.Like("Nickname", nickname, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (phoneNum != null)
                    {
                        ICriterion criterion = Restrictions.Like("PhoneNum", phoneNum, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (tel != null)
                    {
                        ICriterion criterion = Restrictions.Like("Tel", tel, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (qq != null)
                    {
                        ICriterion criterion = Restrictions.Like("QQ", qq, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (weiXin != null)
                    {
                        ICriterion criterion = Restrictions.Like("WeiXin", weiXin, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (email != null)
                    {
                        ICriterion criterion = Restrictions.Like("Email", email, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
