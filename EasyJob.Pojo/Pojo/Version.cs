using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 数据库版本
    /// </summary>
    public class Version
    {
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public virtual int Ver { get; set; }

    }
}
