using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 商品类型
    /// </summary>
    public class MaterialType : TbBase
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public virtual string Name { get; set; }
    }
}
