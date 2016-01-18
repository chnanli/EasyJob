using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 员工调动
    /// </summary>
    public class EmployeeTransfer : TbBase
    {
        /// <summary>
        /// 员工
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 原分店
        /// </summary>
        public virtual Department SrcDept { get; set; }

        /// <summary>
        /// 现分店
        /// </summary>
        public virtual Department DestDept { get; set; }


        /// <summary>
        /// 原职位
        /// </summary>
        public virtual Position SrcPos { get; set; }

        /// <summary>
        /// 现职位
        /// </summary>
        public virtual Position DestPos { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}