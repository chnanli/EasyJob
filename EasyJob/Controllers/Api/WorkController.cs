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
    public class WorkController : ApiPowerController
    {
        private TbBaseOper<Work> workOper = null;
        private TbBaseOper<WorkSub> workSubOper = null;
        private TbBaseOper<WorkDetail> workDetailOper = null;
        private TbBaseOper<CustomerScore> customerScoreOper = null;
        private TbBaseOper<WorkTypeDetail> workTypeDetailOper = null;

        public WorkController()
            :base()
        {
            workOper = new TbBaseOper<Work>(HibernateOper, typeof(Work));
            workSubOper = new TbBaseOper<WorkSub>(HibernateOper, typeof(WorkSub));
            workDetailOper = new TbBaseOper<WorkDetail>(HibernateOper, typeof(WorkDetail));
            customerScoreOper = new TbBaseOper<CustomerScore>(HibernateOper, typeof(CustomerScore));
            workTypeDetailOper = new TbBaseOper<WorkTypeDetail>(HibernateOper, typeof(WorkTypeDetail));
        }

        /// <summary>
        /// 客户下工单数据类
        /// </summary>
        public class WorkAndWorkTypeDetails
        {
            /// <summary>
            /// 工单基本信息
            /// </summary>
            public virtual Work Work { get; set; }

            /// <summary>
            /// 子工单信息
            /// </summary>
            public virtual WorkSub WorkSub { get; set; }

            /// <summary>
            /// 维修类型数组
            /// </summary>
            public virtual IList<WorkTypeDetail> WorkTypeDetails { get; set; }

            /// <summary>
            /// 图片数组
            /// </summary>
            public virtual IList<WorkPic> WorkPics { get; set; }
        }

        //查找地址码
        public ActionResult Add(WorkAndWorkTypeDetails wawtd)
        {
            bool retVal = false;

            Work work = wawtd.Work;//主工单
            WorkSub workSub = wawtd.WorkSub;//子工单
            if (workSub == null)
            {
                workSub = new WorkSub();
            }
            IList<WorkTypeDetail> workTypeDetails = wawtd.WorkTypeDetails;//子工单维修类型
            IList<WorkPic> workPics=wawtd.WorkPics;//子工单图片

            ISession s = null;
            ITransaction trans = null;
            try
            {
                s = HibernateOper.GetCurrentSession();
                trans = s.BeginTransaction();

                //如果主工单的ID不存在则新建主工单,有可能是在原来的主工单基础上追加的子工单就会把主工单的ID传过来
                if (work.Id == Guid.Empty)
                {
                    //生成工单编号
                    work.Code = DateTime.Now.ToString("yyyyMMddHHmmssfffff");
                    //查找客户ID
                    string openId = work.Cstmr.OpenId;
                    work.Cstmr = PojoUtil.GetCust(s, openId);
                    if (work.Cstmr == null)
                    {
                        Customer cstmr = new Customer();
                        cstmr.OpenId = openId;
                        s.Save(cstmr);
                        work.Cstmr = cstmr;
                    }

                    //保存工单
                    s.Save(work);
                }
                else
                {
                    //根据工单ID找到工单
                    work = s.Get<Work>(work.Id);
                    if (work == null)
                    {
                        //工单不存在
                        throw new Exceptions.WorkIsNotFoundException();
                    }
                }

                //保存子工单
                //如果子工单的下单员工存在
                if (workSub.OrderEmp != null)
                {
                    //如果子工单的下单员ID存在
                    if(workSub.OrderEmp.Id != Guid.Empty){
                        workSub.OrderEmp = s.Get<Employee>(workSub.OrderEmp.Id);
                    }
                    //如果子工单的下单员工号存在
                    if (workSub.OrderEmp.Code != null && !workSub.OrderEmp.Code.ToString().Equals(""))
                    {
                        workSub.OrderEmp = PojoUtil.GetEmployee(s, workSub.OrderEmp.Code);
                    }

                    if (workSub.OrderEmp == null)
                    {
                        //该下单员不存在
                        throw new Exceptions.EmployeeIsNotFoundException();
                    }
                }
                workSub.Work = work;
                //根据工单地址自动分配任务到分店
                workSub.Dept = PojoUtil.GetMinDistDept(s, work);
                s.Save(workSub);

                //保存维修类型
                if (workTypeDetails != null)
                {
                    foreach (WorkTypeDetail wtd in workTypeDetails)
                    {
                        wtd.WorkSub = workSub;
                        s.Save(wtd);
                    }
                }
                else
                {
                    //维修类型明细不存在
                    throw new Exceptions.WorkTypeDetailIsNotFoundException();
                }

                //保存图片
                if (workPics != null)
                {
                    foreach (WorkPic wp in workPics)
                    {
                        wp.WorkSub = workSub;
                        s.Save(wp);
                    }
                }

                trans.Commit();

                retVal = true;
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
        /// 查询工单
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="cstmrName">客户名</param>
        /// <param name="contact">联系人</param>
        /// <param name="phoneNum">联系电话</param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string cstmrName, string contact, string phoneNum)
        {
            return Json(workOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(cstmrName))
                    {
                        ICriterion criterion = Restrictions.Like("Cstmr.Name", cstmrName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(contact))
                    {
                        ICriterion criterion = Restrictions.Like("Contact", contact, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(phoneNum))
                    {
                        ICriterion criterion = Restrictions.Like("PhoneNum", phoneNum, MatchMode.Anywhere);
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
        /// <param name="cstmrName">客户名</param>
        /// <param name="contact">联系人</param>
        /// <param name="phoneNum">联系电话</param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string cstmrName, string contact, string phoneNum)
        {
            return Json(workOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(cstmrName))
                    {
                        ICriterion criterion = Restrictions.Like("Cstmr.Name", cstmrName, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(contact))
                    {
                        ICriterion criterion = Restrictions.Like("Contact", contact, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(phoneNum))
                    {
                        ICriterion criterion = Restrictions.Like("PhoneNum", phoneNum, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }


        /// <summary>
        /// 由工单获取子工单
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkSub(int pageSize, int pageNum, string workId)
        {
            return Json(workSubOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("Work", PojoUtil.InitPojo<Work>(workId));
                    criteria.Add(criterion);

                    criteria.AddOrder(Order.Desc("ModDate"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 由子工单获取子工单明细
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="workSub"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkDetail(int pageSize, int pageNum, string workSubId)
        {
            return Json(workDetailOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("WorkSub", PojoUtil.InitPojo<WorkSub>(workSubId));
                    criteria.Add(criterion);

                    criteria.AddOrder(Order.Desc("ModDate"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 由工单获取客户评价
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <param name="work"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetCustomerScore(int pageSize, int pageNum, string workId)
        {
            return Json(customerScoreOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    ICriterion criterion = Restrictions.Eq("Work", PojoUtil.InitPojo<Work>(workId));
                    criteria.Add(criterion);

                    criteria.AddOrder(Order.Desc("ModDate"));
                }
                , pageSize, pageNum));
        }

    }
}
