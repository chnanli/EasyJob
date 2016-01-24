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
        /// 密码错误
        /// </summary>
        public class PwdErrorException : Exception
        {
            public PwdErrorException() : base("PwdErrorException") { }
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

        /// <summary>
        /// 部门Code存在
        /// </summary>
        public class DeptCodeIsExistsException : Exception
        {
            public DeptCodeIsExistsException() : base("DeptCodeIsExistsException") { }
        }

        /// <summary>
        /// 职位Code存在
        /// </summary>
        public class PosCodeIsExistsException : Exception
        {
            public PosCodeIsExistsException() : base("PosCodeIsExistsException") { }
        }

        /// <summary>
        /// 员工Code存在
        /// </summary>
        public class EmpCodeIsExistsException : Exception
        {
            public EmpCodeIsExistsException() : base("EmpCodeIsExistsException") { }
        }

        /// <summary>
        /// 单位名存在
        /// </summary>
        public class UnitNameIsExistsException : Exception
        {
            public UnitNameIsExistsException() : base("UnitNameIsExistsException") { }
        }

        /// <summary>
        /// 商品类型表Code存在
        /// </summary>
        public class GoodsTypeCodeIsExistsException : Exception
        {
            public GoodsTypeCodeIsExistsException() : base("GoodsTypeCodeIsExistsException") { }
        }

        /// <summary>
        /// 商品资料表Code存在
        /// </summary>
        public class GoodsInfoCodeIsExistsException : Exception
        {
            public GoodsInfoCodeIsExistsException() : base("GoodsInfoCodeIsExistsException") { }
        }

        /// <summary>
        /// 商品资料表BarCode存在
        /// </summary>
        public class GoodsInfoBarCodeIsExistsException : Exception
        {
            public GoodsInfoBarCodeIsExistsException() : base("GoodsInfoBarCodeIsExistsException") { }
        }

        /// <summary>
        /// 供应商资料表Code存在
        /// </summary>
        public class VendorInfoCodeIsExistsException : Exception
        {
            public VendorInfoCodeIsExistsException() : base("VendorInfoCodeIsExistsException") { }
        }

        /// <summary>
        /// 库存Code存在
        /// </summary>
        public class StorehouseCodeIsExistsException : Exception
        {
            public StorehouseCodeIsExistsException() : base("StorehouseCodeIsExistsException") { }
        }

        /// <summary>
        /// 销售价格管理（商品资料子表）存在
        /// </summary>
        public class SellPriceInfoIsExistsException : Exception
        {
            public SellPriceInfoIsExistsException() : base("SellPriceInfoIsExistsException") { }
        }

        /// <summary>
        ///库存表存在
        /// </summary>
        public class StockIsExistsException : Exception
        {
            public StockIsExistsException() : base("StockIsExistsException") { }
        }

        /// <summary>
        ///库存表初始化存在
        /// </summary>
        public class StockInitIsExistsException : Exception
        {
            public StockInitIsExistsException() : base("StockInitIsExistsException") { }
        }

        /// <summary>
        ///库存报警表存在
        /// </summary>
        public class StorageAlarmIsExistsException : Exception
        {
            public StorageAlarmIsExistsException() : base("StorageAlarmIsExistsException") { }
        }

        /// <summary>
        ///模块功能存在
        /// </summary>
        public class ModFuncIsExistsException : Exception
        {
            public ModFuncIsExistsException() : base("ModFuncIsExistsException") { }
        }

    }
}