using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 盘点单明细表
    /// </summary>
    public class CheckStockDetail : TbBase
    {
        /// <summary>
        /// 盘点单
        /// </summary>
        public virtual CheckStock CheckStock { get; set; }

        /// <summary>
        /// 商品
        /// </summary>
        public virtual GoodsInfo Goods { get; set; }

        /// <summary>
        /// 盘点数量
        /// </summary>
        public virtual double Qty { get; set; }

        /// <summary>
        /// 系统数量
        /// </summary>
        public virtual double SysQty { get; set; }

        /// <summary>
        /// 盈亏数量
        /// </summary>
        public virtual double ProfitLoss { get; set; }
    }
}
