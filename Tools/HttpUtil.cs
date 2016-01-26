using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace Tools
{
    public class HttpUtil
    {
        public static string HttpPost(string Url, string postDataStr)
        {
            if (postDataStr == null)
            {
                postDataStr = "";
            }

            Encoding myEncoding = System.Text.Encoding.Default;

//            string param = HttpUtility.UrlEncode(postDataStr, myEncoding);
            byte[] bytParam = Encoding.UTF8.GetBytes(postDataStr);  

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
//            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            request.ContentLength = bytParam.Length;
//            request.CookieContainer = cookie;
            Stream myRequestStream = request.GetRequestStream();
//            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding(System.Text.Encoding.Default.BodyName));
//            myStreamWriter.Write(bytParam, 0, bytParam);
//            myStreamWriter.Close();
            myRequestStream.Write(bytParam,0,bytParam.Length);
            myRequestStream.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

//            response.Cookies = cookie.GetCookies(response.ResponseUri);
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        public static string HttpGet(string Url, string postDataStr)
        {
            if (postDataStr == null)
            {
                postDataStr = "";
            }

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }
    }
}