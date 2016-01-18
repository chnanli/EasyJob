
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Receive
{
    public class ReceiveVoiceMsg : ReceiveMsg
    {
        public override string MsgType
        {
            get
            {
                return TYPE_VOICE;
            }
            set
            {

            }
        }

        public string MsgId { get; set; }
        public string Format { get; set; }
        public string MediaId { get; set; }

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
                retVal += "<Voice><MediaId><![CDATA[" + MediaId + "]]></MediaId></Voice>";
            }
            finally
            {
                retVal += "</xml>";
            }

            return retVal;
        }
    }
}