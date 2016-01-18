using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Send
{
    public class SendMsg:ISendMsg
    {
        public const string TYPE_TEXT = "text";
        public const string TYPE_IMAGE = "image";
        public const string TYPE_VOICE = "voice";
        public const string TYPE_LOCATION = "location";
        public const string TYPE_LINK = "link";
        public const string TYPE_EVENT = "event";

        public string touser { get; set; }
        public virtual string msgtype { get; set; }

        public virtual string ToJson()
        {
            return JsonUtil.stringify(this);
        }
    }
}
