using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 工单
    /// </summary>
    public class Work : TbLocation
    {
        /// <summary>
        /// 客户
        /// </summary>
        public virtual Customer Cstmr { get; set; }

        /// <summary>
        /// 工单代码
        /// </summary>
        public virtual string Code { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public virtual string Contact { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public virtual string PhoneNum { get; set; }

        /// <summary>
        /// 状态,状态，0为下单，1分配到部门，2分配到师傅，3上门，4维修中，5完成(如果子工单都完成了才能完成)
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
