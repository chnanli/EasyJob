using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 库存初始化表
    /// </summary>
    public class StockInit : TbBase
    {
        /// <summary>
        /// 仓库
        /// </summary>
        public virtual Storehouse Storehouse { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public virtual double Quantity { get; set; }

    }
}
