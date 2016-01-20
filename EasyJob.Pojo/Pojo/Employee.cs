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
        public virtual string EmpName { get; set; }
        public virtual string NickName{get;set;}

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PwdWeb { get; set; }
        /// <summary>
        /// =0 离职，=1 入职，=2 试用，=3 转正，=4 在职
        /// </summary>
        public virtual string State{get;set;}

        /// <summary>
        /// 身份证
        /// </summary>
        public virtual string IDCode { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public virtual string Native{get;set;}

        /// <summary>
        /// 根据身份证信息自动计算,性别，false为女,true为男
        /// </summary>
        public virtual string Sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation{get;set;}
        public virtual string Education{get;set;}
        public virtual string Institution{get;set;}

        /// <summary>
        /// 根据身份证信息自动计算,生日
        /// </summary>
        public virtual DateTime Birthday { get; set; }
        public virtual DateTime ComeDate{get;set;}
        public virtual DateTime StaffDate{get;set;}
        public virtual DateTime LeaveDate{get;set;}
        public virtual string Tel{get;set;}
        public virtual string Mobile{get;set;}
        public virtual string Tel1{get;set;}
        public virtual string Tel2{get;set;}
        public virtual string OtherAddress{get;set;}
        public virtual string Resume{get;set;}
        public virtual string EMail{get;set;}
        public virtual string QQ{get;set;}
        public virtual string IfPrincipal{get;set;}
        public virtual string IfSysUser{get;set;}
        public virtual string IfWork{get;set;}
        public virtual string OneCardCode{get;set;}
        public virtual DateTime OnDuty{get;set;}
        public virtual DateTime OffDuty{get;set;}
    }
}