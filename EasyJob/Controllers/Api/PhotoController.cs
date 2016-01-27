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
    /// 相片接口
    /// </summary>
    public class PhotoController : ApiPowerController
    {
        private TbBaseOper<Photo> photoOper = null;

        public PhotoController()
            :base()
        {
            photoOper = new TbBaseOper<Photo>(HibernateOper, typeof(Photo));
        }

        /// <summary>
        /// 添加相片
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Add)]
        public ActionResult Add(Photo photo)
        {
            return Json(photoOper.Add(photo));
        }


        /// <summary>
        /// 修改相片
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Update)]
        public ActionResult Update(Photo photo)
        {
            return Json(photoOper.Update(photo));
        }

        /// <summary>
        /// 删除相片
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Del)]
        public ActionResult Del(Photo photo)
        {
            return Json(photoOper.Del(photo));
        }

        /// <summary>
        /// 查询相片
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult Get(int pageSize, int pageNum, string roleFlag, string roleId)
        {
            return Json(photoOper.Get(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(roleFlag))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleFlag", roleFlag);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleId", new Guid(roleId));
                        criteria.Add(criterion);
                    }

                    criteria.AddOrder(Order.Asc("RoleFlag"));
                }
                , pageSize, pageNum));
        }

        /// <summary>
        /// 查询相片页数
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [PowerActionFilterAttribute(FuncName = PowerActionFilterAttribute.FuncEnum.Get)]
        public ActionResult GetPageCount(int pageSize, string roleFlag, string roleId)
        {
            return Json(photoOper.GetPageCount(
                delegate(object sender, ICriteria criteria)
                {
                    if (!string.IsNullOrEmpty(roleFlag))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleFlag", roleFlag);
                        criteria.Add(criterion);
                    }
                    if (!string.IsNullOrEmpty(roleId))
                    {
                        ICriterion criterion = Restrictions.Eq("RoleId", new Guid(roleId));
                        criteria.Add(criterion);
                    }
                }
                , pageSize));
        }

    }
}
