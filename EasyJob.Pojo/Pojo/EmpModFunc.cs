using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 员工模块功能权限,在页面的Add、Update、Del等功能时做控制
    /// </summary>
    public class EmpModFunc : TbBase
    {
        /// <summary>
        /// 绑定员工
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 绑定模块功能
        /// </summary>
        public virtual ModFunc ModFunc { get; set; }

        /// <summary>
        /// 功能函数名称,可以存储多个用|隔开,如 |Add|Update|Del|Get|
        /// </summary>
        public virtual string FuncNames { get; set; }
    }
}
