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
    /// 职位接口
    /// </summary>
    public class PositionController : ApiPowerController
    {
        private TbBaseOper<Position> positionOper = null;

        public PositionController()
            :base()
        {
            positionOper = new TbBaseOper<Position>(HibernateOper, typeof(Position));
        }

        private void IsExistsCode(ISession session, Position pos)
        {
            ICriteria criteria = session.CreateCriteria(typeof(Position));

            ICriterion criterion = null;
            if (pos.Id != Guid.Empty)
            {
                criterion = Restrictions.Not(Restrictions.IdEq(pos.Id));
                criteria.Add(criterion);
            }

            criterion = Restrictions.Eq("Code", pos.Code);
            criteria.Add(criterion);
            //统计
            criteria.SetProjection(
                Projections.ProjectionList()
                .Add(Projections.Count("Id"))
                );

            int count = (int)criteria.UniqueResult();
            if (count > 0)
            {
                throw new EasyJob.Tools.Exceptions.PosCodeIsExistsException();//职位Code已经存在
            }
        }

        /// <summary>
        /// 添加职位
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Position position)
        {
            return Json(positionOper.Add(position,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, position);
                }
                ));
        }


        /// <summary>
        /// 修改职位
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Position position)
        {
            return Json(positionOper.Update(position,
                delegate(object sender, ISession session)
                {
                    //判断是否存在部门Code
                    IsExistsCode(session, position);
                }
                ));
        }

        /// <summary>
        /// 删除职位
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Position position)
        {
            return Json(positionOper.Del(position));
        }

        /// <summary>
        /// 查询职位
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string pId, string code, string name)
        {
            return Json(positionOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (code != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name,MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                    criteria.AddOrder(Order.Asc("Code"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询职位页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string pId, string code, string name)
        {
            return Json(positionOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (pId != null)
                    {
                        ICriterion criterion = Restrictions.Eq("PId", new Guid(pId));
                        criteria.Add(criterion);
                    }
                    else
                    {
                        ICriterion criterion = Restrictions.Eq("PId", Guid.Empty);
                        criteria.Add(criterion);
                    }
                    if (code != null)
                    {
                        ICriterion criterion = Restrictions.Eq("Code", code);
                        criteria.Add(criterion);
                    }
                    if (name != null)
                    {
                        ICriterion criterion = Restrictions.Like("Name", name, MatchMode.Anywhere);
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
