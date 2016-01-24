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
    public class Photo : TbBase
    {
        public enum RoleFlagVal
        {
            EmpInchPhoto = 1,//员工寸照
            EmpNickPhoto = 2,//员工昵照
            EmpIdPhoto = 3,//员工身份照
            EmpSkillPhoto = 4,//员工技能照

            CustInchPhoto = 11,//客户寸照
            CustNickPhoto = 12//客户昵照
        };

        public static string RoleFlagValStr(RoleFlagVal val)
        {
            return ((int)val).ToString();
        }

        /// <summary>
        /// 角色标记,"针对员工：RoleID=hrEmployee.pID
        ///=01，员工寸照；
        ///=02，员工昵照
        ///=03，员工身份照；
        ///=04,员工技能照；
        ///针对客户：RoleID=ctCustData.PID
        ///=11, 客户寸照；
        ///=12，客户昵照；"
        /// </summary>
        public virtual string RoleFlag { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual Guid RoleId { get; set; }

        /// <summary>
        /// 文件
        /// </summary>
        public virtual File File { get; set; }
    }
}
