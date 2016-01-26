
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Send
{
    public class SendTextMsg : SendMsg
    {
        public SendTextMsg()
        {
            text=new Text();
        }

        public override string msgtype
        {
            get { return SendMsg.TYPE_TEXT; }
            set{}
        }

        public class Text
        {
            public string content { get; set; }
        }
        public Text text { get; set; }

        public override string ToJson()
        {
            return JsonUtil.stringify(this);
        }
    }
}