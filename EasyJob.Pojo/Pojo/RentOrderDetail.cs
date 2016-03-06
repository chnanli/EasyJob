using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 领用单明细表
    /// </summary>
    public class RentOrderDetail : TbBase
    {
        /// <summary>
        /// 领用单
        /// </summary>
        public virtual RentOrder RentOrder { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 领用数量
        /// </summary>
        public virtual double Qty { get; set; }


    }
}
