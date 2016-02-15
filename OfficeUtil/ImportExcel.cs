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
        private Type type = null;

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
                this.type = type;
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

        //public IList<object> List()
        //{
        //    return List(type);
        //}

        public IList<object> List(Type type)
        {
            IList<object> retVal = new List<object>();
		
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
                                object newT = type.Assembly.CreateInstance(type.FullName);
                                for (int j = 0; j < row.LastCellNum; j++)
                                {
                                    ICell cell = row.GetCell(j);

                                    SetValue(newT, fieldsName[j].FieldNameCls, cell);

                                    //给属性赋值
                                    
                                    //newT.Add(fieldsName[j].FieldNameCls, val);
							    }
                                retVal.Add(newT);
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

        public IList<T> List<T>() where T:new()
        {
            IList<T> retVal = new List<T>();

            try
            {
                Stream input = new FileStream(filePath, FileMode.Open);	//建立输入流
                IWorkbook wb = null;
                //根据文件格式(2003或者2007)来初始化
                if (isE2007)
                    wb = new XSSFWorkbook(input);
                else
                    wb = new HSSFWorkbook(input);

                ISheet sheet = wb.GetSheet(table);//查找表名

                if (sheet != null)
                {
                    for (int i = 0; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);	//获得行数据

                        //如果当前行小于开始行则跳过
                        if (i < startLine)
                        {
                            continue;
                        }
                        else
                        {
                            //字段行
                            if (i == startLine)
                            {
                                fieldsName = new ExcelField[row.PhysicalNumberOfCells];
                                for (int j = 0; j < row.LastCellNum; j++)
                                {
                                    ICell cell = row.GetCell(j);

                                    ExcelField f = GetField(cell.StringCellValue);
                                    fieldsName[j] = f;
                                }
                            }
                            //数据行
                            else if (i > startLine)
                            {
                                T newT = new T();
                                for (int j = 0; j < row.LastCellNum; j++)
                                {
                                    ICell cell = row.GetCell(j);

                                    SetValue(newT, fieldsName[j].FieldNameCls, cell);

                                    //给属性赋值

                                    //newT.Add(fieldsName[j].FieldNameCls, val);
                                }
                                retVal.Add(newT);
                            }
                        }
                    }
                }
                else
                {
                    throw new Exception("Table is not exists");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retVal;
        }

        private void SetValue(object obj, string propertyName, ICell cell)
        {
            PropertyInfo pi = obj.GetType().GetProperty(propertyName);
            if (pi != null && cell!=null)
            {
                object valObj=null;
                switch (cell.CellType)
                {	//根据cell中的类型来输出数据
                    case CellType.Numeric:
                        if (pi.PropertyType == typeof(string))
                        {
                            valObj = cell.StringCellValue;
                        }
                        else if (pi.PropertyType == typeof(int))
                        {
                            valObj = (int)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(Int16))
                        {
                            valObj = (Int16)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(Int32))
                        {
                            valObj = (Int32)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(Int64))
                        {
                            valObj = (Int64)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(long))
                        {
                            valObj = (long)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(short))
                        {
                            valObj = (short)cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(byte))
                        {
                            valObj = (byte)cell.NumericCellValue;
                        }
                        else if (
                            pi.PropertyType == typeof(float)
                            || pi.PropertyType == typeof(double)
                            )
                        {
                            valObj = cell.NumericCellValue;
                        }
                        else if (pi.PropertyType == typeof(DateTime))
                        {
                            valObj = cell.DateCellValue;
                        }else{
                            valObj=null;
                        }
                        break;
                    case CellType.String:
                        if (pi.PropertyType == typeof(string))
                        {
                            valObj = cell.StringCellValue.ToString();
                        }
                        else if (pi.PropertyType == typeof(int))
                        {
                            valObj = int.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(Int16))
                        {
                            valObj = Int16.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(Int32))
                        {
                            valObj = Int32.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(Int64))
                        {
                            valObj = Int64.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(long))
                        {
                            valObj = long.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(short))
                        {
                            valObj = short.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(byte))
                        {
                            valObj = byte.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(float))
                        {
                            valObj = float.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(double))
                        {
                            valObj = double.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(DateTime))
                        {
                            valObj = DateTime.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(bool))
                        {
                            valObj = bool.Parse(cell.StringCellValue.ToString());
                        }
                        else if (pi.PropertyType == typeof(Guid))
                        {
                            valObj = Guid.Parse(cell.StringCellValue.ToString());
                        }
                        else
                        {
                            valObj = null;
                        }
                        break;
                    case CellType.Boolean:
                        if (pi.PropertyType == typeof(string))
                        {
                            valObj = cell.BooleanCellValue.ToString();
                        }
                        else if (pi.PropertyType == typeof(bool))
                        {
                            valObj = cell.BooleanCellValue;
                        }else{
                            valObj=null;
                        }
                        break;
                    case CellType.Formula:
                        valObj=null;
                        break;
                    default:
                        break;
                }
                pi.SetValue(obj, valObj, null);
            }
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
