using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 调拨单明细表
    /// </summary>
    public class ReqOrderDetail : TbBase
    {
        /// <summary>
        /// 调拨单
        /// </summary>
        public virtual ReqOrder ReqOrder { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 调拨数量
        /// </summary>
        public virtual double Qty { get; set; }

        /// <summary>
        /// 实际调拨数量
        /// </summary>
        public virtual double RealQty { get; set; }

    }
}
