using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyJob.Pojo.Pojo.Bases
{
    public class TbLocation:TbBase
    {
        /// <summary>
        /// 地址,具体到街道
        /// </summary>
        public virtual string Addr { get; set; }

        /// <summary>
        /// 位置，具体第几号第几房
        /// </summary>
        public virtual string Location { get; set; }

        /// <summary>
        /// 地址代码,具体到街道的地址代码
        /// </summary>
        public virtual string AddrCode { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public virtual double Lng { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public virtual double Lat { get; set; }
    }
}
