using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 数据权限,系统用户登陆系统后所能查看或者操作的部门数据权限。
    /// </summary>
    public class EmpData : TbBase
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
        /// 绑定部门
        /// </summary>
        public virtual Department Dept { get; set; }

        /// <summary>
        /// 是否仅仅查看本身数据
        /// </summary>
        public virtual string IfSelf { get; set; }
    }
}
