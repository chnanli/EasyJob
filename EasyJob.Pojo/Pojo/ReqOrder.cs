using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 调拨单主表
    /// </summary>
    public class ReqOrder : TbBase
    {
        /// <summary>
        /// 调拨单号
        /// </summary>
        public virtual string ReqOrderCode { get; set; }

        /// <summary>
        /// 原仓库
        /// </summary>
        public virtual Storehouse SrcStorehouse { get; set; }

        /// <summary>
        /// 目地仓库
        /// </summary>
        public virtual Storehouse DestStorehouse { get; set; }

        /// <summary>
        /// 领货人
        /// </summary>
        public virtual string Carrier { get; set; }

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
