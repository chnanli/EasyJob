﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EasyJob.Pojo.Pojo;
using ORM.Hibernate;
using EasyJob.ContractResolver;

namespace EasyJob.Controllers.Api
{
    public class BaseController : Controller
    {
        public IHibernateOper HibernateOper { get; set; }

        public BaseController()
        {
            try
            {
                //获取数据库操作对象
                HibernateOper = HibernateFactory.GetInstance();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string GetUrl()
        {
            return "http://"+Request.Url.Host + Request.Url.PathAndQuery;
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contractResolver">只显示指定字段</param>
        /// <returns>JsonReuslt</returns>
        protected JsonResult Json(object data, LimitPropsContractResolver contractResolver)
        {
            return Json(
                data,
                null,
                System.Text.Encoding.UTF8,
                JsonRequestBehavior.AllowGet,
                contractResolver
            );
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">行为</param>
        /// <returns>JsonReuslt</returns>
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json(
                data,
                contentType,
                contentEncoding,
                behavior,
                null
            );
        }

        /// <summary>
        /// 返回JsonResult
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="contentType">内容类型</param>
        /// <param name="contentEncoding">内容编码</param>
        /// <param name="behavior">行为</param>
        /// <param name="contractResolver">只显示指定字段</param>
        /// <returns>JsonReuslt</returns>
        protected JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior, LimitPropsContractResolver contractResolver)
        {
            return new CustomJsonResult
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                FormateStr = "yyyy-MM-dd HH:mm:ss",
                ContractResolver=contractResolver
            };
        }

        /// <summary>
        /// 返回JsonResult.24         /// </summary>
        /// <param name="data">数据</param>
        /// <param name="behavior">行为</param>
        /// <param name="format">json中dateTime类型的格式</param>
        /// <returns>Json</returns>
        protected JsonResult Json(object data, JsonRequestBehavior behavior, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                JsonRequestBehavior = behavior,
                FormateStr = format
            };
        }

        /// <summary>
        /// 返回JsonResult42         /// </summary>
        /// <param name="data">数据</param>
        /// <param name="format">数据格式</param>
        /// <returns>Json</returns>
        protected JsonResult Json(object data, string format)
        {
            return new CustomJsonResult
            {
                Data = data,
                FormateStr = format
            };
        }
    }
}