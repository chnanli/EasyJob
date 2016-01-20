using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 公司信息
    /// </summary>
    public class Company : TbLocation
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public virtual string CnName { get; set; }
        /// <summary>
        /// 英文名
        /// </summary>
        public virtual string EnName { get; set; }
        /// <summary>
        /// 公司的英文简称。
        /// </summary>
        public virtual string CompCode { get; set; }
        /// <summary>
        /// 公司简称。
        /// </summary>
        public virtual string CompShortTitle { get; set; }
        /// <summary>
        /// 最高职位代码
        /// </summary>
        public virtual string TopPosCode { get; set; }
        /// <summary>
        /// 最高职位名称
        /// </summary>
        public virtual string TopPosName { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public virtual string Zipcode { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public virtual string Tel { get; set; }
        /// <summary>
        /// 传真
        /// </summary>
        public virtual string Fax { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        public virtual string Mail { get; set; }
        /// <summary>
        /// 网站
        /// </summary>
        public virtual string Web { get; set; }
    }
}