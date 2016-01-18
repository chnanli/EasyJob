using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : TbLocation
    {
        /// <summary>
        /// 上一级部门ID
        /// </summary>
        public virtual Guid PId { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string PhoneNum { get; set; }
    }
}