﻿using Newtonsoft.Json;
using OfficeUtil.Attrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJob.Pojo.Pojo.Bases
{
    /// <summary>
    /// 数据库表基本表
    /// </summary>
    public class TbBase:TbId
    {
        private DateTime _modDate = DateTime.Now;
        private DateTime _uploadDate = DateTime.Now;

        [JsonIgnore]
        public virtual DateTime ModDate
        {
            get{return _modDate; }
            set { _modDate = value; }
        }

        [JsonIgnore]
        public virtual DateTime UploadDate
        {
            get { return _uploadDate; }
            set { _uploadDate = value; }
        }

        [JsonIgnore]
        public virtual bool Del { get; set; }

        [JsonIgnore]
        public virtual bool TrashFlag { get; set; }

        [JsonIgnore]
        public virtual string ExHost { get; set; }

        [JsonIgnore]
        [ExcelColumnAttribute(Name = "导入类型")]
        public virtual string ImportType {
            get
            {
                return _ImportType;
            }
            set
            {
                _ImportType = value;
            }
        }
        private string _ImportType="添加";
    }
}