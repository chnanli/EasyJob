using EasyJob.Pojo.Pojo.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 用户设置参数
    /// </summary>
    class UserSetPara : TbId
    {
        public virtual string ParaName { get; set; }
        public virtual string ParaValue { get; set; }
        public virtual Employee Oper { get; set; }
    }
}
