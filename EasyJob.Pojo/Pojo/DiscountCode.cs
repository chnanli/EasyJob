using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 优惠码
    /// </summary>
    public class DiscountCode : TbBase
    {
        /// <summary>
        /// 客户
        /// </summary>
        public virtual Customer Cstmr { get; set; }

        /// <summary>
        /// 描述内容
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public virtual float Amount { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public virtual DateTime ExpDateTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
