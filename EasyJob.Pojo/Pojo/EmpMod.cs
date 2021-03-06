﻿using System;
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
        /// 角色标记,=0 RoleID 为Employee.Id，=1 RoleID为Department.Id，=2 RoleID为Position.Id
        /// </summary>
        public virtual string RoleFlag { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// 绑定模块菜单
        /// </summary>
        public virtual Mod Mod { get; set; }
    }
}
