using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJob.Pojo.Pojo.Bases
{
    /// <summary>
    /// 数据库表基本表
    /// </summary>
    public class TbBase
    {
        private DateTime _modDate = DateTime.Now;
        private DateTime _uploadDate = DateTime.Now;

        public virtual Guid Id { get; set; }

        public virtual DateTime ModDate
        {
            get{return _modDate; }
            set { _modDate = value; }
        }

        public virtual DateTime UploadDate
        {
            get { return _uploadDate; }
            set { _uploadDate = value; }
        }
        public virtual bool Del { get; set; }
        public virtual bool TrashFlag { get; set; }
        public virtual string ExHost { get; set; }
    }
}