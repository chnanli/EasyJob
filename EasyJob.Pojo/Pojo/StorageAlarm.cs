using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 库存警告设置表（库存数量在上限与下限之间时，销售时，报警提示；当达到下限时，通过ifStopSell判断是否停售，有停售提示）
    /// </summary>
    public class StorageAlarm : TbBase
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
        /// 上限数量
        /// </summary>
        public virtual double UpperLimit { get; set; }

        /// <summary>
        /// 下限数量
        /// </summary>
        public virtual double LowerLimit { get; set; }

        /// <summary>
        /// 当达到下限时，是否停售商品
        /// </summary>
        public virtual string IfStopSell { get; set; }

    }
}
