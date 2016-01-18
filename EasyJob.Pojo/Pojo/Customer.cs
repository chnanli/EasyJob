using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 客户
    /// </summary>
    public class Customer : TbBase
    {
        /// <summary>
        /// 微信OpenId
        /// </summary>
        public virtual string OpenId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public virtual string Nickname { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string Pwd { get; set; }

        /// <summary>
        /// 性别,false为女,true为男
        /// </summary>
        public virtual bool Sex { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public virtual string Zcode { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public virtual string PhoneNum { get; set; }

        /// <summary>
        /// 固话
        /// </summary>
        public virtual string Tel { get; set; }

        /// <summary>
        /// QQ
        /// </summary>
        public virtual string QQ { get; set; }

        /// <summary>
        /// 微信号
        /// </summary>
        public virtual string WeiXin { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public virtual string Email { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public virtual int Level { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}