using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 职位
    /// </summary>
    public class Position : TbBase
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
    }
}
