using EasyJob.Pojo.Pojo.Bases;
using System;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 商品资料表
    /// </summary>
    public class GoodsInfo : TbBase
    {
        /// <summary>
        /// 商品编号
        /// </summary>
        public virtual string GoodsCode {get;set;}
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string GoodsName {get;set;}
        /// <summary>
        /// 商品条码
        /// </summary>
        public virtual string BarCode { get; set; }

        /// <summary>
        /// 商品类别ID。
        /// </summary>
        public virtual GoodsType GoodsType { get; set; }
        /// <summary>
        /// 型号
        /// </summary>
        public virtual string ModelNum {get;set;}
        /// <summary>
        /// 计量单位
        /// </summary>
        public virtual string UnitName {get;set;}
        /// <summary>
        /// 包装数量/规格
        /// </summary>
        public virtual string Spec {get;set;}
        /// <summary>
        /// 是否使用,是否使用。0：使用；1：停用。
        /// </summary>
        public virtual string IsUse {get;set;}
        /// <summary>
        /// 销售价格
        /// </summary>
        public virtual double SellPrice {get;set;}
        /// <summary>
        /// 安装价格
        /// </summary>
        public virtual double SetupPrice {get;set;}
        /// <summary>
        /// 商品图片值
        /// </summary>
        public virtual byte[] Photo {get;set;}
        /// <summary>
        /// 销售折扣
        /// </summary>
        public virtual double Discount {get;set;}
        /// <summary>
        /// 供应商主键
        /// </summary>
        public virtual VendorInfo Vendor { get; set; }
    }
}
