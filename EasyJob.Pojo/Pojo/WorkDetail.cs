using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 工单维修明细
    /// </summary>
    public class WorkDetail : TbBase
    {
        /// <summary>
        /// 子工单
        /// </summary>
        public virtual WorkSub WorkSub { get; set; }

        /// <summary>
        /// 维修类型
        /// </summary>
        public virtual RepairType RepairType { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual Material Material { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public virtual DateTime CompleteDateTime { get; set; }

        /// <summary>
        /// 结算时间
        /// </summary>
        public virtual DateTime SettlementDateTime { get; set; }

        /// <summary>
        /// 维修金额
        /// </summary>
        public virtual float Amount { get; set; }

        /// <summary>
        /// 优惠减扣
        /// </summary>
        public virtual float Discount { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
