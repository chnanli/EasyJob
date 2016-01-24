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
        public enum RoleFlagVal
        {
            Emp,
            Dept,
            Pos
        };

        public static string RoleFlagValStr(RoleFlagVal val)
        {
            return ((int)val).ToString();
        }

        /// <summary>
        /// 角色标记,=0 RoleID为Employee.Id，=1 RoleID为Department.Id，=2 RoleID为Position.Id
        /// </summary>
        public virtual string RoleFlag { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual Guid RoleId { get; set; }

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
