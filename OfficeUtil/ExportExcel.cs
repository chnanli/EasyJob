using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeUtil.Attrib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OfficeUtil
{
    public class ExportExcel
    {
        private String filePath = "";
        private IList<ExcelField> fields;
        private int startLine = 0;//开始行
        private String table = "";
        private bool isE2007 = false;

        /// <summary>
        /// 初始化类型的Excel字段
        /// </summary>
        /// <param name="type">要初始化字段的类型</param>
        private void InitTypeExcelField(Type type)
        {
            this.fields = new List<ExcelField>();

            //表名
            Object[] tableAttrs = type.GetCustomAttributes(typeof(ExcelTableAttribute), true);
            if (tableAttrs != null && tableAttrs.Length > 0)
            {
                ExcelTableAttribute eta = (ExcelTableAttribute)tableAttrs[0];
                this.table = eta.Name;
            }

            //字段名
            PropertyInfo[] pis = type.GetProperties();
            foreach (PropertyInfo pi in pis)
            {
                Object[] columnAttrs = pi.GetCustomAttributes(typeof(ExcelColumnAttribute), true);
                if (columnAttrs != null && columnAttrs.Length > 0)
                {
                    foreach (ExcelColumnAttribute eca in columnAttrs)
                    {
                        ExcelField ef = new ExcelField(pi.Name, eca.Name);
                        this.fields.Add(ef);
                    }
                }
            }
        }

        public ExportExcel(String filePath, Type type)
            : this(filePath, "", null, true, 0) {
                InitTypeExcelField(type);//初始化类型的Excel字段
        }

        public ExportExcel(String filePath, String table, IList<ExcelField> fields)
            : this(filePath, table, fields, true, 0) { }

        public ExportExcel(String filePath, String table, IList<ExcelField> fields, bool isE2007)
            : this(filePath, table, fields, isE2007, 0){}

        public ExportExcel(String filePath, String table, IList<ExcelField> fields, bool isE2007, int startLine)
        {
            this.filePath = filePath;
            this.table = table;
            this.fields = fields;
            this.isE2007 = isE2007;
            this.startLine = startLine;
        }

        public void Save<T>(IList<T> list){
		    try {
			    // 第一步，创建一个webbook，对应一个Excel文件  
			    IWorkbook wb = null;
	            if(isE2007)
				    wb = new XSSFWorkbook();
			    else
				    wb = new HSSFWorkbook();
	            // 第二步，在webbook中添加一个sheet,对应Excel文件中的sheet  
	            ISheet sheet = wb.CreateSheet(table);
	            // 第三步，在sheet中添加表头第0行,注意老版本poi对Excel的行数列数有限制short  
	            IRow row = sheet.CreateRow(startLine);
	            // 第四步，创建单元格，并设置值表头 设置表头居中  
	            ICellStyle style = wb.CreateCellStyle();
                style.Alignment = HorizontalAlignment.Center;// 创建一个居中格式
	        
	            ICell cell=null;
	            for(int i=0;i<fields.Count;i++){
                    ExcelField xf = fields[i];
	        	    cell=row.CreateCell(i);
	        	    cell.SetCellValue(xf.FielNameExcel);
	        	    cell.CellStyle=style;
	            }
	        
	            for (int i = 0; i < list.Count; i++)  
	            {
	        	    object obj=list[i];
	                row = sheet.CreateRow(startLine+i+1);

	                ObjToRow(obj,row);
	            }
	            // 第六步，将文件存到指定位置  
	            try  
	            {
                    FileStream fout = new FileStream(filePath, FileMode.OpenOrCreate);
	                wb.Write(fout);
	                fout.Close();
	                wb.Close();
	            }
	            catch (Exception e)
	            {
                    throw e;
	            }
			
		    } catch (Exception ex) {
			    throw ex;
		    }
	    }
	
        public void ObjToRow(object obj,IRow row){
		    for(int i=0;i<fields.Count;i++){
			    ExcelField ef=fields[i];

			    try{
                    PropertyInfo pi=obj.GetType().GetProperty(ef.FieldNameCls);
                    object val = pi.GetValue(obj, null);
                    string valStr = "";
                    if (val != null)
                    {
                        valStr = val.ToString();
                    }
                    row.CreateCell(i).SetCellValue(valStr);
			    }catch(Exception e){
                    throw e;
			    }
		    }
	    }
    }
}
