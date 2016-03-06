using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 归还单主表
    /// </summary>
    public class RetRentOrder : TbBase
    {
        /// <summary>
        /// 归还单号
        /// </summary>
        public virtual string RetRentOrderCode { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public virtual Storehouse Storehouse { get; set; }

        /// <summary>
        /// 借用人
        /// </summary>
        public virtual Employee Borr{ get; set; }

        /// <summary>
        /// 借用人部门
        /// </summary>
        public virtual Department BorrDept { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public virtual Employee Oper { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public virtual DateTime OperTime { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public virtual Employee Approval { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public virtual DateTime ApprovalTime { get; set; }

        /// <summary>
        /// 单据状态。0：未审核；1：审核；
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
