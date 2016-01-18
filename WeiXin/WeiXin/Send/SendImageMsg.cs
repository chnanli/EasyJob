
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Send
{
    public class SendImageMsg : SendMsg
    {
        public SendImageMsg()
        {
            image = new Image();
        }

        public override string msgtype
        {
            get { return SendMsg.TYPE_IMAGE; }
            set{}
        }

        public class Image
        {
            public string media_id { get; set; }
        }
        public Image image { get; set; }

        public override string ToJson()
        {
            return JsonUtil.stringify(this);
        }
    }
}