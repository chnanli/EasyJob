using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 子工单
    /// </summary>
    public class WorkSub : TbBase
    {
        public virtual Work Work { get; set; }
        /// <summary>
        /// 下单方式,比如微信、WEB或者其它
        /// </summary>
        public virtual string OrderType { get; set; }

        /// <summary>
        /// 下单员工，比如阿姨或小区管理员等
        /// </summary>
        public virtual Employee OrderEmp { get; set; }

        /// <summary>
        /// 客户描述内容
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 接单分店
        /// </summary>
        public virtual Department Dept { get; set; }

        /// <summary>
        /// 接单师傅
        /// </summary>
        public virtual Employee Emp { get; set; }

        /// <summary>
        /// 分配方式，0为系统自动分配，1为人工分配
        /// </summary>
        public virtual int DistMode { get; set; }

        /// <summary>
        /// 如果分配方式为人工分配时，记录操作人员
        /// </summary>
        public virtual Employee DistEmp { get; set; }

        /// <summary>
        /// 状态,状态，0为下单，1分配到部门，2分配到师傅，3上门，4维修中，5完成
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
