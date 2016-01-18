using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 维修单上传的图片
    /// </summary>
    public class WorkPic : TbBase
    {
        /// <summary>
        /// 工单
        /// </summary>
        public virtual WorkSub WorkSub { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public virtual File Pic { get; set; }
    }
}