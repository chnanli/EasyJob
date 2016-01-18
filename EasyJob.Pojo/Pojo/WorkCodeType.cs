using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 维修产品类型，关联类型1与类型2
    /// </summary>
    public class WorkCodeType : TbBase
    {
        public virtual WorkCodeType1 Type1 { get; set; }
        public virtual WorkCodeType2 Type2 { get; set; }
    }
}