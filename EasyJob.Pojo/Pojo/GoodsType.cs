using EasyJob.Pojo.Pojo.Bases;
using System;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 商品类型
    /// </summary>
    public class GoodsType : TbBase
    {
        /// <summary>
        /// 上级ID
        /// </summary>
        public virtual Guid PId { get; set; }
        /// <summary>
        /// 单位描述
        /// </summary>
        public virtual string UnitDesc {get;set;}
        /// <summary>
        /// 树状结构的线性编码,采用形如：001001001的结构来编码
        /// </summary>
        public virtual string Xpath {get;set;}
        /// <summary>
        /// 类型编号
        /// </summary>
        public virtual string GoodsTypeCode { get; set; }

        /// <summary>
        /// 类型名
        /// </summary>
        public virtual string GoodsTypeName { get; set; }

    }
}
