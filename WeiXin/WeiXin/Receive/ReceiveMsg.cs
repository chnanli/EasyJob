
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Receive
{
    public class ReceiveMsg
    {
        public const string TYPE_TEXT = "text";
        public const string TYPE_IMAGE = "image";
        public const string TYPE_VOICE = "voice";
        public const string TYPE_LOCATION = "location";
        public const string TYPE_LINK = "link";
        public const string TYPE_EVENT = "event";

        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public DateTime CreateTime { get; set; }
        public virtual string MsgType { get; set; }

        public virtual string ToXml()
        {
            string retVal = "";

            try
            {
                retVal += "<xml>";

                retVal += "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>";
                retVal += "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>";
                retVal += "<CreateTime>" + WeiXinUtil.DateTimeToSecond(CreateTime) + "</CreateTime>";
                retVal += "<MsgType><![CDATA[" + MsgType + "]]></MsgType>";
            }
            finally
            {
                retVal += "</xml>";
            }

            return retVal;
        }

    }
}