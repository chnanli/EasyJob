using EasyJob.Pojo.Pojo.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 引用参数表
    /// </summary>
    class ParaRef : TbId
    {
        /// <summary>
        /// 功能名称
        /// </summary>
        public virtual string FuncName { get; set; }
        /// <summary>
        /// 参数名称
        /// </summary>
        public virtual string ParaName { get; set; }
        /// <summary>
        /// 参数值
        /// </summary>
        public virtual string ParaValue { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Memo { get; set; }
    }
}
