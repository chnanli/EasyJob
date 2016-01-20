using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 单位
    /// </summary>
    public class Unit : TbBase
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public virtual string UnitName { get; set; }

        /// <summary>
        /// 单位描述
        /// </summary>
        public virtual string UnitDesc { get; set; }

    }
}
