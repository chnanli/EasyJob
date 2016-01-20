using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 销售价格管理（商品资料子表）
    /// </summary>
    public class SellPriceInfo : TbBase
    {
        public virtual Storehouse Storehouse{get;set;}
        public virtual GoodsInfo Goods{get;set;}
        public virtual double SellPrice{get;set;}
        public virtual double SetupPrice{get;set;}
    }
}
