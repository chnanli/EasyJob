using System;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 小区
    /// </summary>
    public class Garden : TbLocation
    {
        /// <summary>
        /// 小区名
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// 小区管理员
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        public virtual int Level { get; set; }

        /// <summary>
        /// 提成
        /// </summary>
        public virtual float Commission { get; set; }

        /// <summary>
        /// 小区最近的分店
        /// </summary>
        public virtual Department Dept { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
