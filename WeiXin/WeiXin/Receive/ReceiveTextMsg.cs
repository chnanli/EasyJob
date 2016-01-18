
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Receive
{
    public class ReceiveTextMsg:ReceiveMsg
    {
        public override string MsgType
        {
            get
            {
                return TYPE_TEXT;
            }
            set
            {

            }
        }

        public string MsgId { get; set; }
        public string Content { get; set; }

        public override string ToXml()
        {
            string retVal = "";

            try
            {
                retVal += "<xml>";

                retVal += "<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>";
                retVal += "<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>";
                retVal += "<CreateTime>" + WeiXinUtil.DateTimeToSecond(CreateTime) + "</CreateTime>";
                retVal += "<MsgType><![CDATA[" + MsgType + "]]></MsgType>";
                retVal += "<Content><![CDATA[" + Content + "]]></Content>";
            }
            finally
            {
                retVal += "</xml>";
            }

            return retVal;
        }

    }
}