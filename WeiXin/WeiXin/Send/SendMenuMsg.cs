
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tools;
using WeiXin.Tools;

namespace WeiXin.WeiXin.Send
{
    public class SendMenuMsg : SendMsg
    {
        public SendMenuMsg()
        {
            button = new List<object>();
        }

        public class MainMenu : ISendMsg
        {
            public string name { get; set; }
            public IList sub_button { get; set; }

            public string ToJson()
            {
                string retVal = "{";

                try
                {
                    retVal += "\"name\":\"" + name+"\",";
                    retVal += "\"sub_button\":[";
                    if (sub_button != null)
                    {
                        for (int i = 0; i < sub_button.Count; i++)
                        {
                            ISendMsg obj = (ISendMsg)sub_button[i];
                            if (obj != null)
                            {
                                retVal += obj.ToJson();
                                if (i < sub_button.Count - 1)
                                {
                                    retVal += ",";
                                }
                            }
                        }
                    }
                    retVal += "]";
                }
                finally
                {
                    retVal += "}";
                }
                

                return retVal;
            }
        }

        public class ViewMenu : ISendMsg
        {
            public string type
            {
                get { return "view"; }
                set{}
            }

            public string name { get; set; }
            public string url { get; set; }

            public string ToJson()
            {
                return JsonUtil.stringify(this);
            }
        }
        public class ClickMenu : ISendMsg
        {
            public string type
            {
                get { return "click"; }
                set { }
            }
            public string name { get; set; }
            public string key { get; set; }

            public string ToJson()
            {
                return JsonUtil.stringify(this);
            }
        }

        public IList button { get; set; }

        public override string ToJson()
        {
            string retVal = "{";

            try
            {
                retVal += "\"button\":[";
                if (button != null)
                {
                    for (int i = 0; i < button.Count; i++)
                    {
                        ISendMsg obj = (ISendMsg)button[i];
                        if (obj != null)
                        {
                            retVal += obj.ToJson();
                            if (i < button.Count - 1)
                            {
                                retVal += ",";
                            }
                        }
                    }
                }
                retVal += "]";
            }
            finally
            {
                retVal += "}";
            }


            return retVal;
        }
    }
}