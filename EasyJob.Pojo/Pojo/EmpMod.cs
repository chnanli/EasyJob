using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 员工模块菜单权限,在加载菜单时是否显示用的
    /// </summary>
    public class EmpMod : TbBase
    {
        /// <summary>
        /// 绑定员工
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 绑定模块菜单
        /// </summary>
        public virtual Mod Mod { get; set; }
    }
}
