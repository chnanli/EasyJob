using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 地址码
    /// </summary>
    public class AddrCode : TbBase
    {
        /// <summary>
        /// 地址码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 上级代码
        /// </summary>
        public virtual int PId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual byte Level { get; set; }
    }
}