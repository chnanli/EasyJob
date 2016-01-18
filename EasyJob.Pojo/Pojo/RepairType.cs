using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 维修类型
    /// </summary>
    public class RepairType : TbBase
    {
        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual float Amount { get; set; }
    }
}