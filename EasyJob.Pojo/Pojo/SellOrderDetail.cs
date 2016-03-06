using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 销售单明细表
    /// </summary>
    public class SellOrderDetail : TbBase
    {
        /// <summary>
        /// 销售单
        /// </summary>
        public virtual SellOrder SellOrder { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        public virtual double Qty { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public virtual double SellPrice{ get; set; }

        /// <summary>
        /// 实现销售数量
        /// </summary>
        public virtual double RealSellPrice { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public virtual double TotalMoney { get; set; }
    }
}
