using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using OfficeUtil.Attrib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OfficeUtil
{
    public class ImportExcel
    {
        private static DecimalFormat df = new DecimalFormat("0");
	
	    private String filePath="";
	    private IList<ExcelField> fields;
	    private int startLine=0;//开始行
	    private ExcelField[] fieldsName=null;
	    private String table="";
	    private bool isE2007=false;

        /// <summary>
        /// 初始化类型的Excel字段
        /// </summary>
        /// <param name="type">要初始化字段的类型</param>
        private void InitTypeExcelField(Type type)
        {
            this.fields = new List<ExcelField>();

            //表名
            Object[] tableAttrs = type.GetCustomAttributes(typeof(ExcelTableAttribute),true);
            if (tableAttrs != null && tableAttrs.Length > 0)
            {
                ExcelTableAttribute eta = (ExcelTableAttribute)tableAttrs[0];
                this.table = eta.Name;
            }

            //字段名
            PropertyInfo[] pis=type.GetProperties();
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

        public ImportExcel(String filePath, Type type)
            : this(filePath, "",null, true, 0) {
                InitTypeExcelField(type);//初始化类型的Excel字段
        }

        public ImportExcel(String filePath, String table, IList<ExcelField> fields)
            : this(filePath, table, fields, true, 0) { }

	    public ImportExcel(String filePath,String table,IList<ExcelField> fields,bool isE2007)
            : this(filePath, table, fields, isE2007, 0){}
	
	    public ImportExcel(String filePath,String table,IList<ExcelField> fields,bool isE2007,int startLine){
		    this.filePath=filePath;
		    this.table=table;
		    this.fields=fields;
		    this.isE2007=isE2007;
		    this.startLine=startLine;
	    }

        public IList<IDictionary<String, object>> List()
        {
            IList<IDictionary<String, object>> retVal = new List<IDictionary<String, object>>();
		
		    try {
                Stream input = new FileStream(filePath, FileMode.Open);	//建立输入流
			    IWorkbook wb  = null;
			    //根据文件格式(2003或者2007)来初始化
			    if(isE2007)
				    wb = new XSSFWorkbook(input);
			    else
				    wb = new HSSFWorkbook(input);

                ISheet sheet = wb.GetSheet(table);//查找表名

			    if(sheet!=null){
				    for(int i=0;i<=sheet.LastRowNum;i++) {
					    IRow row = sheet.GetRow(i);	//获得行数据

					    //如果当前行小于开始行则跳过
					    if(i<startLine){
						    continue;
					    }else{
						    //字段行
						    if(i==startLine){
							    fieldsName=new ExcelField[row.PhysicalNumberOfCells];
                                for (int j = 0; j < row.LastCellNum; j++)
                                {
                                    ICell cell = row.GetCell(j);

								    ExcelField f=GetField(cell.StringCellValue);
								    fieldsName[j]=f;
							    }
						    }
						    //数据行
						    else if(i>startLine){
                                IDictionary<String, object> map = new Dictionary<String, object>();
                                for (int j = 0; j < row.LastCellNum; j++)
                                {
                                    ICell cell = row.GetCell(j);
								
								    String val="";
                                    if (cell!=null)
                                    {
                                        switch (cell.CellType)
                                        {	//根据cell中的类型来输出数据
                                            case CellType.Numeric:
                                                val = df.Format(cell.NumericCellValue);
                                                break;
                                            case CellType.String:
                                                val = cell.StringCellValue;
                                                break;
                                            case CellType.Boolean:
                                                bool valBool = cell.BooleanCellValue;
                                                if (valBool != null)
                                                {
                                                    val = valBool.ToString();
                                                }
                                                else
                                                {
                                                    val = "False";
                                                }
                                                break;
                                            case CellType.Formula:
                                                val = cell.CellFormula;
                                                break;
                                            default:
                                                break;
                                        }
                                    }
                                    map.Add(fieldsName[j].FieldNameCls, val);
							    }
							    retVal.Add(map);
						    }
					    }
				    }
			    }else{
				    throw new Exception("Table is not exists");
			    }
			
		    } catch (Exception ex) {
			    throw ex;
		    }
		
		    return retVal;
	    }

        private ExcelField GetField(String fielNameExcel)
        {
		    ExcelField retVal=null;
		
		    foreach(ExcelField f in fields){
                if (f.FielNameExcel.Equals(fielNameExcel))
                {
				    retVal=f;
				    break;
			    }
		    }
		
		    return retVal;
	    }
    }
}
