using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 结算明细
    /// </summary>
    public class SettlementDetail : TbBase
    {
        /// <summary>
        /// 工单维修明细
        /// </summary>
        public virtual WorkDetail WorkDetail { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 结算人
        /// </summary>
        public virtual Employee SettlementEmp { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public virtual DateTime SettlementDateTime { get; set; }

        /// <summary>
        /// 结算金额
        /// </summary>
        public virtual float Amount { get; set; }

    }
}
