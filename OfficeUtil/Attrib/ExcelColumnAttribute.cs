using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeUtil.Attrib
{
    /// <summary>
    /// Excel表字段定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcelColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
