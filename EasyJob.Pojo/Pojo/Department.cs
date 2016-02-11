using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;
using Newtonsoft.Json;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 部门
    /// </summary>
    [JsonObject(MemberSerialization.OptOut)]
    public class Department : TbLocation
    {
        /// <summary>
        /// 上一级部门ID
        /// </summary>
        public virtual Guid PId { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name { get; set; }
        public virtual string Phone1{get;set;}
        public virtual string Phone2{get;set;}
        public virtual string Phone3{get;set;}
        public virtual string ContactName{get;set;}
        public virtual string ContactPhone{get;set;}
        public virtual string DeptType{get;set;}
        public virtual int OrderID { get; set; }

        [JsonIgnore]
        public virtual string TestExcel
        {
            get {
                return "abc";
            }
            set { }
        }
    }
}