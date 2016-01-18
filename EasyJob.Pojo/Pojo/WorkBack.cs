using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 工单维修退单
    /// </summary>
    public class WorkBack : TbBase
    {
        /// <summary>
        /// 工单明细
        /// </summary>
        public virtual WorkDetail WorkDetail { get; set; }

        /// <summary>
        /// 退单说明,描述
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 退单金额
        /// </summary>
        public virtual float Amount { get; set; }

        /// <summary>
        /// 状态,0为申请退单,1为退单审核失败，2为退单成功
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
