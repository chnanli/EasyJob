using EasyJob.Pojo.Pojo.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 系统信息
    /// </summary>
    class SetPara : TbId
    {
        public virtual string ParaName { get; set; }
        public virtual string ParaValue { get; set; }
    }
}
