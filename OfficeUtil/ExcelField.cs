using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeUtil
{
    /// <summary>
    /// EXCEL字段与类字段关系
    /// </summary>
    public class ExcelField
    {
        public ExcelField(string fieldNameCls, string fielNameExcel)
        {
            this.FieldNameCls = fieldNameCls;
            this.FielNameExcel = fielNameExcel;
        }
        public String FieldNameCls { get; set; }//类字段名
        public String FielNameExcel { get; set; }//Excel字段
    }
}
