using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 文件
    /// </summary>
    public class File : TbBase
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// MD5值
        /// </summary>
        public virtual string Md5 { get; set; }

        /// <summary>
        /// 大小
        /// </summary>
        public virtual long Size { get; set; }

        /// <summary>
        /// 真实路径
        /// </summary>
        public virtual string RealPath { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public virtual string ContentType { get; set; }
    }
}
