using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Tools;

namespace EasyJob.ContractResolver
{
    public class CustomJsonResult : JsonResult
    {
        private string dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        private JsonSerializerSettings jsetting = null;//JSON序列化设置

        private LimitPropsContractResolver contractResolver = null;
        public LimitPropsContractResolver ContractResolver {
            get { return contractResolver; }
            set { 
                contractResolver = value;
                jsetting.ContractResolver = contractResolver;
            }
        }

        public CustomJsonResult()
        {
            //JSON序列化设置
            jsetting = new JsonSerializerSettings();
            jsetting.NullValueHandling = NullValueHandling.Ignore;

            //设置日期时间的格式，与DataTime类型的ToString格式相同
            IsoDateTimeConverter iso = new IsoDateTimeConverter();
            iso.DateTimeFormat = dateTimeFormat;

            jsetting.Converters.Add(iso);
        }


        public string DateTimeFormat{
            get { return dateTimeFormat; }
            set { dateTimeFormat = value; }
        }
        /// <summary>
        /// 格式化字符串
        /// </summary>
        public string FormateStr
        {
            get;
            set;
        }

        /// <summary>
        /// 重写执行视图
        /// </summary>
        /// <param name="context">上下文</param>
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            HttpResponseBase response = context.HttpContext.Response;

            if (string.IsNullOrEmpty(this.ContentType))
            {
                response.ContentType = "application/json";
            }
            else
            {
                response.ContentType = this.ContentType;
            }

            if (this.ContentEncoding != null)
            {
                response.ContentEncoding = this.ContentEncoding;
            }

            if (this.Data != null)
            {
                var jsonString = JsonConvert.SerializeObject(this.Data, Formatting.Indented, jsetting);
                //JavaScriptSerializer jss = new JavaScriptSerializer();
                //string jsonString = jss.Serialize(Data);
                //string p = @"\\/Date\((\d+)\)\\/";
                //string p = @"\\/Date\((\d+)\+\d+\)\\/";
                //MatchEvaluator matchEvaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);
                //Regex reg = new Regex(p);
                //jsonString = reg.Replace(jsonString, matchEvaluator);

                response.Write(jsonString);
            }
        }

        /// <summary>  
        /// 将Json序列化的时间由/Date(1294499956278)转为字符串 .
        /// </summary>  
        /// <param name="m">正则匹配</param>
        /// <returns>格式化后的字符串</returns>
        private string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString(FormateStr);
            return result;
        }
    }

    //public class CustomJsonResult : JsonResult
    //{
    //    /// <summary>
    //    /// 格式化字符串
    //    /// </summary>
    //    public string FormateStr
    //    {
    //        get;
    //        set;
    //    }

    //    /// <summary>
    //    /// 重写执行视图
    //    /// </summary>
    //    /// <param name="context">上下文</param>
    //    public override void ExecuteResult(ControllerContext context)
    //    {
    //        if (context == null)
    //        {
    //            throw new ArgumentNullException("context");
    //        }

    //        HttpResponseBase response = context.HttpContext.Response;

    //        if (string.IsNullOrEmpty(this.ContentType))
    //        {
    //            response.ContentType = "application/json";
    //        }
    //        else
    //        {
    //            response.ContentType = this.ContentType;
    //        }

    //        if (this.ContentEncoding != null)
    //        {
    //            response.ContentEncoding = this.ContentEncoding;
    //        }

    //        if (this.Data != null)
    //        {
    //            var jsonString = JsonUtil.stringify(this.Data);
    //            //JavaScriptSerializer jss = new JavaScriptSerializer();
    //            //string jsonString = jss.Serialize(Data);
    //            //string p = @"\\/Date\((\d+)\)\\/";
    //            string p = @"\\/Date\((\d+)\+\d+\)\\/";
    //            MatchEvaluator matchEvaluator = new MatchEvaluator(this.ConvertJsonDateToDateString);
    //            Regex reg = new Regex(p);
    //            jsonString = reg.Replace(jsonString, matchEvaluator);

    //            response.Write(jsonString);
    //        }
    //    }

    //    /// <summary>  
    //    /// 将Json序列化的时间由/Date(1294499956278)转为字符串 .
    //    /// </summary>  
    //    /// <param name="m">正则匹配</param>
    //    /// <returns>格式化后的字符串</returns>
    //    private string ConvertJsonDateToDateString(Match m)
    //    {
    //        string result = string.Empty;
    //        DateTime dt = new DateTime(1970, 1, 1);
    //        dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
    //        dt = dt.ToLocalTime();
    //        result = dt.ToString(FormateStr);
    //        return result;
    //    }
    //}
}