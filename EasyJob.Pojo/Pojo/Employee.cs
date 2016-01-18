using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 员工
    /// </summary>
    public class Employee : TbLocation
    {
        /// <summary>
        /// 所属部门
        /// </summary>
        public virtual Department Dept { get; set; }

        /// <summary>
        /// 所属职位
        /// </summary>
        public virtual Position Pos { get; set; }

        ////分店
        //public virtual Shop Shop { get; set; }

        /// <summary>
        /// 员工编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IdCard { get; set; }

        /// <summary>
        /// 根据身份证信息自动计算,性别，false为女,true为男
        /// </summary>
        public virtual bool Sex { get; set; }

        /// <summary>
        /// 根据出生日期自动计算,年龄
        /// </summary>
        public virtual int Age { get; set; }

        /// <summary>
        /// 根据身份证信息自动计算,生日
        /// </summary>
        public virtual DateTime Birthday { get; set; }

        /// <summary>
        /// 身份证图片
        /// </summary>
        public virtual File IdCardPic { get; set; }

        /// <summary>
        /// 寸头像
        /// </summary>
        public virtual File Pic { get; set; }

        /// <summary>
        /// 入职时间
        /// </summary>
        public virtual DateTime Entry { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNum { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}