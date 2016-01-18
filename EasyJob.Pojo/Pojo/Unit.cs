using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 文件
    /// </summary>
    public class Unit : TbBase
    {
        /// <summary>
        /// 单位代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 单位名
        /// </summary>
        public virtual string Name { get; set; }

    }
}
