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
    /// 模块功能接口
    /// </summary>
    public class ModFuncController : ApiPowerController
    {
        private TbBaseOper<ModFunc> modFuncOper = null;

        public ModFuncController()
            :base()
        {
            modFuncOper = new TbBaseOper<ModFunc>(HibernateOper, typeof(ModFunc));
        }

        private void IsExists(ISession session, ModFunc modFunc)
        {
            ICriteria criteria = session.CreateCriteria(typeof(ModFunc));

            ICriterion criterion = null;
            if (modFunc.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(modFunc.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Cls", modFunc.Cls);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.ModFuncIsExistsException();//模块功能已经存在
            }
        }

        /// <summary>
        /// 添加模块功能
        /// </summary>
        /// <param name="modFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(ModFunc modFunc)
        {
            return Json(modFuncOper.Add(modFunc,
                delegate(object sender, ISession session)
                {
                    //判断是否存在模块功能
                    IsExists(session, modFunc);
                }
                ));
        }


        /// <summary>
        /// 修改模块功能
        /// </summary>
        /// <param name="modFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(ModFunc modFunc)
        {
            return Json(modFuncOper.Update(modFunc,
                delegate(object sender, ISession session)
                {
                    //判断是否存在模块功能
                    IsExists(session, modFunc);
                }
                ));
        }

        /// <summary>
        /// 删除模块功能
        /// </summary>
        /// <param name="modFunc"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(ModFunc modFunc)
        {
            return Json(modFuncOper.Del(modFunc));
        }

        /// <summary>
        /// 查询模块功能
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string cls)
        {
            return Json(modFuncOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(cls))
                    {
                        ICriterion criterion = Restrictions.Eq("Cls", cls);
                        criteria.Add(criterion);
                    }

                    criteria.AddOrder(Order.Asc("Cls"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询模块功能页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string cls)
        {
            return Json(modFuncOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(cls))
                    {
                        ICriterion criterion = Restrictions.Eq("Cls", cls);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

        /// <summary>
        /// 获取自己的模块功能
        /// </summary>
        /// <returns></returns>
        [LoginActionFilterAttribute]
        public ActionResult GetMyModFunc()
        {
            IList<EmpModFunc> retVal = new List<EmpModFunc>();

            foreach (EmpModFunc empModFunc in MyModFunc)
            {
                EmpModFunc emf = new EmpModFunc();
                emf.ModFunc = new ModFunc();
                emf.ModFunc.Cls = empModFunc.ModFunc.Cls;
                emf.FuncNames = empModFunc.FuncNames;
                retVal.Add(emf);
            }

            return Json(retVal);
        }

    }
}
