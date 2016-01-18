using EasyJob.Pojo.Pojo.Bases;

namespace EasyJob.Pojo.Pojo
{
    /// <summary>
    /// 客户评价
    /// </summary>
    public class CustomerScore : TbBase
    {
        /// <summary>
        /// 工单
        /// </summary>
        public virtual Work Work { get; set; }

        /// <summary>
        /// 评价，描述说明
        /// </summary>
        public virtual string Depict { get; set; }

        /// <summary>
        /// 评分
        /// </summary>
        public virtual int Score { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get; set; }
    }
}
