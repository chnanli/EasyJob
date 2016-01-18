using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 模块功能
    /// </summary>
    public class ModFunc : TbBase
    {
        /// <summary>
        /// 功能所属类全称
        /// </summary>
        public virtual string Cls { get; set; }

        /// <summary>
        /// 功能函数名称,可以存储多个用|隔开,如 |Add|Update|Del|Get|
        /// </summary>
        public virtual string FuncNames { get; set; }
    }
}
