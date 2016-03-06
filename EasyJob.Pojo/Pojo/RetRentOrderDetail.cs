using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 归还单明细表
    /// </summary>
    public class RetRentOrderDetail : TbBase
    {
        /// <summary>
        /// 归还单
        /// </summary>
        public virtual RetRentOrder RetRentOrder { get; set; }

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
