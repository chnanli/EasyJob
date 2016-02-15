using EasyJob.Pojo.Pojo;
using EasyJob.Pojo.Pojo.Bases;
using EasyJob.Tools;
using NHibernate;
using NHibernate.Criterion;
using OfficeUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools;

namespace EasyJob.Controllers.Api
{
    public class FileController : ApiPowerController
    {
        private static string UploadFile = "UploadFile";
        private TbBaseOper<File> fileOper = null;
        
        public FileController()
            : base()
        {
            fileOper = new TbBaseOper<File>(HibernateOper, typeof(File));
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload()
        {
            IList<File> retVal = new List<File>();

            string url = Server.MapPath("/") + @"Files\";

            System.IO.Stream uploadStream = null;
            System.IO.FileStream fs = null;
            //文件上传，一次上传1M的数据，防止出现大文件无法上传
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count;i++ )
            {
                File file = new File();
                try
                {
                    HttpPostedFileBase postFileBase = files[i];
                    uploadStream = postFileBase.InputStream;
                    int bufferLen = 1024;
                    byte[] buffer = new byte[bufferLen];
                    int contentLen = 0;

                    file.Name = System.IO.Path.GetFileName(postFileBase.FileName);
                    file.ContentType = postFileBase.ContentType;
                    file.RealPath = url + Guid.NewGuid().ToString();
                    fs = new System.IO.FileStream(file.RealPath, System.IO.FileMode.Create, System.IO.FileAccess.ReadWrite);

                    while ((contentLen = uploadStream.Read(buffer, 0, bufferLen)) != 0)
                    {
                        fs.Write(buffer, 0, bufferLen);
                        fs.Flush();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (null != fs)
                    {
                        fs.Close();
                    }
                    if (null != uploadStream)
                    {
                        uploadStream.Close();
                    }
                }

                file.Size = GetFileSize(file.RealPath);
                file.Md5 = Md5Util.GetMD5HashFromFile(file.RealPath);

                //查找是否存在过相同MD5值的文件，如果是则不保存到数据库
                IList<File> searchFiles=fileOper.Get(
                    delegate(object sender, ICriteria criteria)
                    {
                        criteria.Add(Restrictions.Eq("Md5", file.Md5));
                    }
                    );

                if (searchFiles.Count > 0)
                {
                    //删除上传的文件并把数据库相同文件信息返回
                    DelFile(file.RealPath);
                    file = searchFiles[0];
                }
                else
                {
                    //保存文件到数据库
                    fileOper.Add(file);
                }

                retVal.Add(file);
            }

            return Json(retVal);
        }

        /// <summary>  
        /// 获取文件大小  
        /// </summary>  
        /// <param name="filePath"></param>  
        /// <returns></returns>  
        public static long GetFileSize(string filePath)
        {
            long lSize = 0;
            if (System.IO.File.Exists(filePath))
                lSize = new System.IO.FileInfo(filePath).Length;
            return lSize;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static bool DelFile(string filePath)
        {
            bool retVal = false;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
                retVal = true;
            }
                
            return retVal;
        }

        public ActionResult DownLoad(string id)
        {
            File file = fileOper.Get(new Guid(id));
            //返回文件
            return File(file.RealPath, file.ContentType,file.Name);
        }

        /// <summary>
        /// 下载缩略图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult DownLoadThumbnail(string id, int width, int height)
        {
            File file = fileOper.Get(new Guid(id));

            string path = Server.MapPath("/") + @"Files\Thumbnail\";
            string name=System.IO.Path.GetFileName(file.RealPath);
            string fullPath=path+name+"_"+width+"_"+height;

            //如果缩略图不存在
            if (!System.IO.File.Exists(fullPath))
            {
                ImageUtil.MakeThumbnail(file.RealPath, fullPath, width, height, "W");
            }

            string fileName = System.IO.Path.GetFileNameWithoutExtension(file.Name)
                + "_" + width + "_" + height
                +System.IO.Path.GetExtension(file.Name);

            //返回文件
            return File(fullPath, file.ContentType, fileName);
        }

        public ActionResult DownLoadDept()
        {
            string fileName = Server.MapPath("/") + @"Files\Temp\"+"1.xlsx";

            IList<ExcelField> fields=new List<ExcelField>();

            ExcelField ef=null;

            ef=new ExcelField("Code","代码");
            fields.Add(ef);
            ef = new ExcelField("Name", "名字");
            fields.Add(ef);
            ef = new ExcelField("TestExcel", "测试Excel");
            fields.Add(ef);

            TbBaseOper<Department> departmentOper = new TbBaseOper<Department>(HibernateOper, typeof(Department));
            IList<Department> list=departmentOper.Get();

            //ExportExcel ee = new ExportExcel(fileName, "abc", fields);
            ExportExcel ee = new ExportExcel(fileName,typeof(Department));
            ee.Save(list);

            //返回文件
            return File(fileName, "xlsx", "1.xlsx");
        }

        public ActionResult ImportTest()
        {
            IList<ExcelField> fields=new List<ExcelField>();

            ExcelField ef=null;

            ef=new ExcelField("Code","代码");
            fields.Add(ef);
            ef = new ExcelField("Name", "名字");
            fields.Add(ef);
            ef = new ExcelField("TestExcel", "测试Excel");
            fields.Add(ef);

            string url=Server.MapPath("/") + @"Files\Temp\"+"1.xlsx";
            //ImportExcel ie = new ImportExcel(url,"abc",fields);
            ImportExcel ie = new ImportExcel(url,typeof(Department));

            IList<Department> vals = ie.List<Department>();

            foreach (Department dic in vals)
            {
                string code = dic.Code;
                string name = dic.Name;
                string testExcel = dic.TestExcel;
            }
            return null;
        }

        //public void DownLoad(string id)
        //{
        //    File file=fileOper.Get(new Guid(id));
        //    if (file != null)
        //    {
        //        Response.ClearHeaders();
        //        Response.Clear();
        //        Response.Expires = 0;
        //        Response.Buffer = true;
        //        Response.AddHeader("Accept-Language", "zh-tw");
        //        System.IO.FileStream files = new System.IO.FileStream(file.RealPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
        //        byte[] byteFile = null;
        //        if (files.Length == 0)
        //        {
        //            byteFile = new byte[1];
        //        }
        //        else
        //        {
        //            byteFile = new byte[files.Length];
        //        }
        //        files.Read(byteFile, 0, (int)byteFile.Length);
        //        files.Close();

        //        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name, System.Text.Encoding.UTF8));
        //        Response.ContentType = "application/octet-stream;charset=utf-8";
        //        Response.BinaryWrite(byteFile);
        //        Response.End();
        //    }
        //}

        public ActionResult Add(File file)
        {
            return Json(fileOper.Add(file));
        }

        public ActionResult Update(File file)
        {
            return Json(fileOper.Update(file));
        }

        public ActionResult Del(File file)
        {
            return Json(fileOper.Del(file));
        }

        public ActionResult Get(int pageSize, int pageNum)
        {
            return Json(fileOper.Get(Get_OnCriteria, pageSize, pageNum));
        }

        public ActionResult GetPageCount(int pageSize)
        {
            return Json(fileOper.GetPageCount(GetPageCount_OnCriteria, pageSize));
        }

        public void Get_OnCriteria(object sender, ICriteria criteria)
        {
            criteria.AddOrder(Order.Asc("Code"));
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }
        public void GetPageCount_OnCriteria(object sender, ICriteria criteria)
        {
            //            ICriterion criterion = Restrictions.Eq("Name", "测试123");
            //            criteria.Add(criterion);
        }
    }
}
