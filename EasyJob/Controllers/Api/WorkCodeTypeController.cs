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
    public class WorkCodeTypeController : ApiPowerController
    {
        private TbBaseOper<WorkCodeType> workCodeTypeOper = null;
        private TbBaseOper<WorkCodeType1> workCodeType1Oper = null;
        private TbBaseOper<WorkCodeType2> workCodeType2Oper = null;

        public WorkCodeTypeController()
            :base()
        {
            workCodeTypeOper = new TbBaseOper<WorkCodeType>(HibernateOper, typeof(WorkCodeType));
            workCodeType1Oper = new TbBaseOper<WorkCodeType1>(HibernateOper, typeof(WorkCodeType1));
            workCodeType2Oper = new TbBaseOper<WorkCodeType2>(HibernateOper, typeof(WorkCodeType2));
        }

        /// <summary>
        /// 添加类型
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(WorkCodeType workCodeType)
        {
            return Json(workCodeTypeOper.Add(workCodeType));
        }


        /// <summary>
        /// 修改类型
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(WorkCodeType workCodeType)
        {
            return Json(workCodeTypeOper.Update(workCodeType));
        }

        /// <summary>
        /// 删除类型
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(WorkCodeType workCodeType)
        {
            return Json(workCodeTypeOper.Del(workCodeType));
        }

        /// <summary>
        /// 获取所有类型
        /// </summary>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum)
        {
            return Json(workCodeTypeOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    criteria.AddOrder(Order.Desc("ModDate"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询所有类型页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize)
        {
            return Json(workCodeTypeOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                }
                , pageSize));
        }


        /// <summary>
        /// 添加类型1
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult AddWorkCodeType1(WorkCodeType1 workCodeType1)
        {
            return Json(workCodeType1Oper.Add(workCodeType1));
        }


        /// <summary>
        /// 修改类型1
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult UpdateWorkCodeType1(WorkCodeType1 workCodeType1)
        {
            return Json(workCodeType1Oper.Update(workCodeType1));
        }

        /// <summary>
        /// 删除类型1
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult DelWorkCodeType1(WorkCodeType1 workCodeType1)
        {
            return Json(workCodeType1Oper.Del(workCodeType1));
        }

        /// <summary>
        /// 获取所有类型1
        /// </summary>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkCodeType1(int pageSize, int pageNum)
        {
            return Json(workCodeType1Oper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    criteria.AddOrder(Order.Asc("Code"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询所有类型1页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkCodeType1PageCount(int pageSize)
        {
            return Json(workCodeType1Oper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                }
                , pageSize));
        }

        /// <summary>
        /// 添加类型2
        /// </summary>
        /// <param name="workCodeType2"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult AddWorkCodeType2(WorkCodeType2 workCodeType2)
        {
            return Json(workCodeType2Oper.Add(workCodeType2));
        }


        /// <summary>
        /// 修改类型2
        /// </summary>
        /// <param name="workCodeType2"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult UpdateWorkCodeType2(WorkCodeType2 workCodeType2)
        {
            return Json(workCodeType2Oper.Update(workCodeType2));
        }

        /// <summary>
        /// 删除类型2
        /// </summary>
        /// <param name="workCodeType"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult DelWorkCodeType2(WorkCodeType2 workCodeType2)
        {
            return Json(workCodeType2Oper.Del(workCodeType2));
        }

        /// <summary>
        /// 获取所有类型2
        /// </summary>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkCodeType2(int pageSize, int pageNum)
        {
            return Json(workCodeType2Oper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    criteria.AddOrder(Order.Asc("Code"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询所有类型2页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetWorkCodeType2PageCount(int pageSize)
        {
            return Json(workCodeType2Oper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                }
                , pageSize));
        }

        /// <summary>
        /// 获取所有类型1
        /// </summary>
        /// <returns></returns>
        public ActionResult GetWorkCodeType1ForWeiXin()
        {
            IList<WorkCodeType1> retVal = workCodeType1Oper.Get(delegate(object sender, ICriteria criteria)
            {
                criteria.AddOrder(Order.Asc("Code"));
            });

            return Json(retVal);
        }

        /// <summary>
        /// 根据类型1获取所有类型2
        /// </summary>
        /// <param name="workCodeType1"></param>
        /// <returns></returns>
        public ActionResult GetWorkCodeType2ForWeiXin(string workCodeType1Id)
        {
            IList<WorkCodeType> wcts = workCodeTypeOper.Get(delegate(object sender, ICriteria criteria)
            {
                ICriterion criterion = Restrictions.Eq("Type1", PojoUtil.InitPojo<WorkCodeType1>(workCodeType1Id));
                criteria.Add(criterion);

                ICriteria criteria2=criteria.CreateCriteria("Type2","t2");
                criteria2.AddOrder(Order.Asc("Code"));
            });

            IList<WorkCodeType2> retVal = new List<WorkCodeType2>();
            if (wcts.Count > 0)
            {
                foreach (WorkCodeType wct in wcts)
                {
                    retVal.Add(wct.Type2);
                }
            }
            wcts.Clear();

            return Json(retVal);
        }
    }
}
