using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;

namespace WeiXin.Tools
{
    public class JsonUtil
    {
        public static T parse<T>(string jsonString)
        {
            return parse<T>(jsonString, Encoding.UTF8);
        }
        public static T parse<T>(string jsonString, Encoding encoding)
        {
            using (var ms = new MemoryStream(encoding.GetBytes(jsonString)))
            {
                return (T)new DataContractJsonSerializer(typeof(T)).ReadObject(ms);
            }
        }

        public static string stringify(object jsonObject)
        {
            return stringify(jsonObject, Encoding.UTF8);
        }
        public static string stringify(object jsonObject, Encoding encoding)
        {
            using (var ms = new MemoryStream())
            {
                new DataContractJsonSerializer(jsonObject.GetType()).WriteObject(ms, jsonObject);
                return encoding.GetString(ms.ToArray());
            }
        }
    }
}