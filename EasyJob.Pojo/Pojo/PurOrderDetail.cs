using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 采购单明细表
    /// </summary>
    public class PurOrderDetail : TbBase
    {
        /// <summary>
        /// 采购单
        /// </summary>
        public virtual PurOrder PurOrder { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 采购单价
        /// </summary>
        public virtual double Price { get; set; }

        /// <summary>
        /// 采购数量
        /// </summary>
        public virtual double Qty { get; set; }

    }
}
