using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 退货单明细表
    /// </summary>
    public class RetOrderDetail : TbBase
    {
        /// <summary>
        /// 退货单
        /// </summary>
        public virtual RetOrder RetOrder { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 退货单价
        /// </summary>
        public virtual double Price { get; set; }

        /// <summary>
        /// 退货数量
        /// </summary>
        public virtual double Qty { get; set; }

    }
}
