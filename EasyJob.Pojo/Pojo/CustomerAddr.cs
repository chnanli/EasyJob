using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 客户地址
    /// </summary>
    public class CustomerAddr : TbLocation
    {
        /// <summary>
        /// 客户
        /// </summary>
        public virtual Customer Cstmr { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string PhoneNum { get; set; }

    }
}
