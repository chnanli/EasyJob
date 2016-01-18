using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Receive
{
    public class ReceiveEventMsg:ReceiveMsg
    {
        public override string MsgType
        {
            get
            {
                return TYPE_EVENT;
            }
            set
            {

            }
        }

        public string Event { get; set; }
        public string EventKey { get; set; }

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
                retVal += "<Event><![CDATA[" + Event + "]]></Event>";
            }
            finally
            {
                retVal += "</xml>";
            }

            return retVal;
        }
    }
}