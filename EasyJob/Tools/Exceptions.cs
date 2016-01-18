using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EasyJob.Tools
{
    public class Exceptions
    {
        /// <summary>
        /// 地址码错误
        /// </summary>
        public class AddrCodeErrorException : Exception
        {
            public AddrCodeErrorException() : base("AddrCodeError") { }
        }

        /// <summary>
        /// 地址码不存在
        /// </summary>
        public class AddrCodeIsNotFoundException : Exception
        {
            public AddrCodeIsNotFoundException() : base("AddrCodeIsNotFound") { }
        }

        /// <summary>
        /// 维修类型明细,主要是客户下单时选择类型，关联类型1与类型2不存在
        /// </summary>
        public class WorkTypeDetailIsNotFoundException : Exception
        {
            public WorkTypeDetailIsNotFoundException() : base("WorkTypeDetailIsNotFound") { }
        }

        //工单不存在
        public class WorkIsNotFoundException : Exception
        {
            public WorkIsNotFoundException() : base("WorkIsNotFound") { }
        }

        //员工不存在
        public class EmployeeIsNotFoundException : Exception
        {
            public EmployeeIsNotFoundException() : base("EmployeeIsNotFound") { }
        }

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        public class UserNameOrPwdErrorException : Exception
        {
            public UserNameOrPwdErrorException() : base("UserNameOrPwdError") { }
        }

        /// <summary>
        /// 是否没有登录
        /// </summary>
        public class IsNoLoginException : Exception
        {
            public IsNoLoginException() : base("IsNoLogin") { }
        }

        /// <summary>
        /// 是否没有权限
        /// </summary>
        public class IsNoPowerException : Exception
        {
            public IsNoPowerException() : base("IsNoPower") { }
        }

        /// <summary>
        /// 客户不存在
        /// </summary>
        public class CstmrIsNotExistsException : Exception
        {
            public CstmrIsNotExistsException() : base("CstmrIsNotExists") { }
        }

    }
}