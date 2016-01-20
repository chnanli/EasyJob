using EasyJob.Pojo.Pojo.Bases;
using System;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 仓库
    /// </summary>
    public class Storehouse : TbLocation
    {
        /// <summary>
        /// 仓库编号
        /// </summary>
        public virtual string StoreCode { get; set; }
        /// <summary>
        /// 仓库名称
        /// </summary>
        public virtual string StoreName { get; set; }
        /// <summary>
        /// 管理员
        /// </summary>
        public virtual string Manager { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        public virtual Department Dept { get; set; }
        /// <summary>
        /// 是否有负库存。0：否；1：允许负库存。
        /// </summary>
        public virtual string IsNegative { get; set; }

    }
}
