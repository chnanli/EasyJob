using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 维修类型明细,主要是客户下单时选择类型，关联类型1与类型2
    /// </summary>
    public class WorkTypeDetail : TbBase
    {
        /// <summary>
        /// 子工单
        /// </summary>
        public virtual WorkSub WorkSub { get; set; }

        /// <summary>
        /// 1级类型
        /// </summary>
        public virtual WorkCodeType1 Type1 { get; set; }

        /// <summary>
        /// 2级类型
        /// </summary>
        public virtual WorkCodeType2 Type2 { get; set; }
    }
}