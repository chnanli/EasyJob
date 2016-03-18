using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;
using Newtonsoft.Json;
using OfficeUtil.Attrib;
using ORM.Hibernate;
using NHibernate;
using NHibernate.Criterion;
using System.Collections;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 员工
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    [ExcelTableAttribute(Name = "部门")]
    public class Employee : TbLocation,IExists
    {
        private DateTime _birthday = DateTime.Now;
        private DateTime _comeDate = DateTime.Now;
        private DateTime _staffDate = DateTime.Now;
        private DateTime _leaveDate = DateTime.Now;
        private DateTime _onDuty = DateTime.Now;
        private DateTime _offDuty = DateTime.Now;
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
        [ExcelColumnAttribute(Name = "代码")]
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ExcelColumnAttribute(Name = "名字")]
        public virtual string EmpName { get; set; }
        public virtual string NickName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public virtual string PwdWeb { get; set; }
        /// <summary>
        /// =0 离职，=1 入职，=2 试用，=3 转正，=4 在职
        /// </summary>
        [ExcelColumnAttribute(Name = "状态")]
        public virtual string State { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [ExcelColumnAttribute(Name = "身份证")]
        public virtual string IDCode { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public virtual string Native { get; set; }

        /// <summary>
        /// 根据身份证信息自动计算,性别，false为女,true为男
        /// </summary>
        [ExcelColumnAttribute(Name = "性别")]
        public virtual string Sex { get; set; }
        /// <summary>
        /// 民族
        /// </summary>
        public virtual string Nation { get; set; }
        public virtual string Education { get; set; }
        public virtual string Institution { get; set; }

        /// <summary>
        /// 根据身份证信息自动计算,生日
        /// </summary>
        [ExcelColumnAttribute(Name = "生日")]
        public virtual DateTime Birthday
        {
            get { return _birthday; }
            set { _birthday = value; }
        }
        [ExcelColumnAttribute(Name = "到岗日期")]
        public virtual DateTime ComeDate
        {
            get { return _comeDate; }
            set { _comeDate = value; }
        }
        [ExcelColumnAttribute(Name = "转正")]
        public virtual DateTime StaffDate
        {
            get { return _staffDate; }
            set { _staffDate = value; }
        }
        [ExcelColumnAttribute(Name = "离职")]
        public virtual DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }
        public virtual string Tel { get; set; }
        public virtual string Mobile { get; set; }
        public virtual string Tel1 { get; set; }
        public virtual string Tel2 { get; set; }
        public virtual string OtherAddress { get; set; }
        public virtual string Resume { get; set; }
        public virtual string EMail { get; set; }
        public virtual string QQ { get; set; }
        [ExcelColumnAttribute(Name = "部门负责人")]
        public virtual string IfPrincipal { get; set; }
        [ExcelColumnAttribute(Name = "系统用户")]
        public virtual string IfSysUser { get; set; }
        [ExcelColumnAttribute(Name = "内勤人员")]
        public virtual string IfWork { get; set; }
        public virtual string OneCardCode { get; set; }
         [ExcelColumnAttribute(Name = "上班时间")]
        public virtual DateTime OnDuty
        {
            get { return _onDuty; }
            set { _onDuty = value; }
        }
         [ExcelColumnAttribute(Name = "下班时间")]
         public virtual DateTime OffDuty
         {
             get { return _offDuty; }
             set { _offDuty = value; }
         }

        [JsonIgnore]
        [ExcelColumnAttribute(Name = "部门名称")]
        public virtual string Deptcode
        {
            get
            {
                return Dept.Id.ToString();
            }
            set
            {
                TbBaseOper<Department> deptOper = new TbBaseOper<Department>(HibernateFactory.GetInstance(), typeof(Department));
                IList<Department> d = deptOper.Get(
                    delegate(object sender, ICriteria criteria) {
                        ICriterion criterion = Restrictions.Eq("Name", value);
                        criteria.Add(criterion);
                        //Dept = criteria.Add(criterion).UniqueResult<Department>();
                    });
                Dept = d[0];
            }
        }
        [JsonIgnore]
        [ExcelColumnAttribute(Name = "职位")]
        public virtual string PosI
        {
            get
            {
                return Pos.Id.ToString();
            }
            set
            {
                TbBaseOper<Position> position = new TbBaseOper<Position>(HibernateFactory.GetInstance(), typeof(Position));
                IList<Position> p = position.Get(
                    delegate(object sender, ICriteria criteria) {
                        ICriterion criterion = Restrictions.Eq("Name", value);
                        criteria.Add(criterion);
                        //Pos = criteria.Add(criterion).UniqueResult<Position>();
                    });
                Pos = p[0];
            }
        }
        
        public virtual bool IsExists(ISession session)
        {
            //导入数据存在时返回true
            bool ralVal = false;
            try {
                IList<Employee> employee = session.CreateCriteria<Employee>().Add(Restrictions.Eq("IDCode", IDCode)).List<Employee>();
                if (employee.Count > 0) {
                    this.Id = employee[0].Id;
                    ralVal = true;
                }
            } catch(Exception e) {
                string ex = e.Message;
            }
            return ralVal;
            //TbBaseOper<Employee> employee = new TbBaseOper<Employee>(HibernateFactory.GetInstance(), typeof(Employee));
            //try {
            //    IList<Employee> elyee = employee.Get(
            //    delegate(object sender, ICriteria criteria) {
            //        ICriterion criterion = Restrictions.Eq("IDCode", IDCode);
            //        criteria.Add(criterion);
            //    });
            //    if (elyee.Count > 0) {
            //        ralVal = true;
            //    }
            //} catch(Exception e) {
            //    string ex = e.Message;
            //}
            
        }
    }
}
