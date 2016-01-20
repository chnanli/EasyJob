using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Material : TbBase
    {
        /// <summary>
        /// 条码
        /// </summary>
        public virtual string Barcode { get; set; }

        /// <summary>
        /// 说明,描述
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public virtual string Model { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public virtual GoodsType Type { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public virtual Unit Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public virtual float Price { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
