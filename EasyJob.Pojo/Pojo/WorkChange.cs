using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 转单
    /// </summary>
    public class WorkChange : TbBase
    {
        /// <summary>
        /// 子工单
        /// </summary>
        public virtual WorkSub WorkSub { get; set; }

        /// <summary>
        /// 原分店
        /// </summary>
        public virtual Department SrcDept { get; set; }

        /// <summary>
        /// 目标分店
        /// </summary>
        public virtual Department DestDept { get; set; }

        /// <summary>
        /// 描述说明
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 状态,0为申请转单,1为转单审核失败，2为转单成功
        /// </summary>
        public virtual int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
