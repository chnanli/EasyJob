using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 供应商资料
    /// </summary>
    public class VendorInfo : TbLocation
    {
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string VendorCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string VendorName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string ContactName { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Telephone{get;set;}
        /// <summary>
        /// 传真
        /// </summary>
        public virtual string Fax{get;set;}
        /// <summary>
        /// 移动电话
        /// </summary>
        public virtual string Mobile{get;set;}
        /// <summary>
        /// 电子邮件
        /// </summary>
        public virtual string Email{get;set;}
        /// <summary>
        /// QQ
        /// </summary>
        public virtual string QQ{get;set;}
        /// <summary>
        /// 产地
        /// </summary>
        public virtual string VendorArea{get;set;}
        /// <summary>
        /// 供应商网址
        /// </summary>
        public virtual string WebSite { get; set; }
    }
}