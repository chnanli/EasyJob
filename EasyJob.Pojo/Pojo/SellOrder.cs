using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 销售单主表
    /// </summary>
    public class SellOrder : TbBase
    {
        /// <summary>
        /// 销售单号
        /// </summary>
        public virtual string SellOrderCode { get; set; }

        /// <summary>
        /// 仓库
        /// </summary>
        public virtual Storehouse Storehouse { get; set; }

        /// <summary>
        /// 销售员
        /// </summary>
        public virtual Employee Salesperson { get; set; }

        /// <summary>
        /// 销售员部门
        /// </summary>
        public virtual Department SalespersonDept { get; set; }

        /// <summary>
        /// 销售时间
        /// </summary>
        public virtual DateTime SellDateTime { get; set; }

        /// <summary>
        /// 销售总金额
        /// </summary>
        public virtual double TotalMoney { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
