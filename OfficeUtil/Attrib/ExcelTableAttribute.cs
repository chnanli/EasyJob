using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OfficeUtil.Attrib
{
    /// <summary>
    /// Excel表定义
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ExcelTableAttribute:Attribute
    {
        public string Name { get; set; }
    }
}
