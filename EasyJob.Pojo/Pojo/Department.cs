using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;
using Newtonsoft.Json;
using OfficeUtil.Attrib;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 部门
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    [ExcelTableAttribute(Name="部门")]
    public class Department : TbLocation
    {
        /// <summary>
        /// 上一级部门ID
        /// </summary>
        [ExcelColumnAttribute(Name = "上级代码")]
        public virtual Guid PId { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [ExcelColumnAttribute(Name="代码")]
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [ExcelColumnAttribute(Name = "名字")]
        public virtual string Name { get; set; }
        public virtual string Phone1{get;set;}
        public virtual string Phone2{get;set;}
        public virtual string Phone3{get;set;}
        public virtual string ContactName{get;set;}
        public virtual string ContactPhone{get;set;}
        public virtual string DeptType{get;set;}
        public virtual int OrderID { get; set; }

        //[JsonIgnore]
        //[ExcelColumnAttribute(Name = "测试Excel")]
        //public virtual string TestExcel
        //{
        //    get {
        //        return "abc";
        //    }
        //    set { }
        //}

        //[JsonIgnore]
        //[ExcelColumnAttribute(Name = "TestBoolean")]
        //public virtual bool TestBoolean {get;set; }

        //[JsonIgnore]
        //[ExcelColumnAttribute(Name = "TestInt")]
        //public virtual int TestInt { get; set; }

        //[JsonIgnore]
        //[ExcelColumnAttribute(Name = "TestDateTime")]
        //public virtual DateTime TestDateTime { get; set; }
    }
}