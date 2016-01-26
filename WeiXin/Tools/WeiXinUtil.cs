
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Xml;
using Tools;
using WeiXin.WeiXin.Cert;
using WeiXin.WeiXin.Receive;
using WeiXin.WeiXin.Send;

namespace WeiXin.Tools
{
    public class WeiXinUtil
    {
        public static string AppId { get; set; }
        public static string Appsecret { get; set; }

        /// <summary>
        /// 验证微信签名
        /// </summary>
        /// * 将token、timestamp、nonce三个参数进行字典序排序
        /// * 将三个参数字符串拼接成一个字符串进行sha1加密
        /// * 开发者获得加密后的字符串可与signature对比，标识该请求来源于微信。
        /// <returns></returns>
        public static string Signature(string token, string timestamp, string nonce)
        {
            if (token == null)
            {
                token = "";
            }
            if (timestamp == null)
            {
                timestamp = "";
            }
            if (nonce == null)
            {
                nonce = "";
            }

            string[] arrTmp = { token, timestamp, nonce };
            Array.Sort(arrTmp);     //字典排序
            string retVal = string.Join("", arrTmp);
            retVal = FormsAuthentication.HashPasswordForStoringInConfigFile(retVal, "SHA1");
            retVal = retVal.ToLower();
            return retVal;
        }

        public static DateTime SecondToDateTime(int val)
        {
            //格林威治时间
            DateTime dateTime = new DateTime(1970, 1, 1, 8, 0, 0);
            return dateTime.AddSeconds(val);
        }

        public static int DateTimeToSecond(DateTime dt)//创建时间戳
        {
            //格林威治时间
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);//格林威治时间1970，1，1，0，0，0
            return (int)(dt - dateStart).TotalSeconds;
        }

        /// <summary>
        /// XML字符串转成接收消息对象
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static ReceiveMsg XmlToReceiveMsg(string msg)
        {
            ReceiveMsg retVal = null;
            //封装请求类  
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(msg);
            XmlElement rootElement = doc.DocumentElement;
            //MsgType  
            XmlNode MsgType = rootElement.SelectSingleNode("MsgType");

            switch (MsgType.InnerText)
            {
                case ReceiveMsg.TYPE_TEXT: //文本消息
                    retVal = new ReceiveTextMsg() { MsgId = rootElement.SelectSingleNode("MsgId").InnerText, Content = rootElement.SelectSingleNode("Content").InnerText };
                    break;
                case ReceiveMsg.TYPE_IMAGE: //图片  
                    retVal = new ReceiveImageMsg() { MsgId = rootElement.SelectSingleNode("MsgId").InnerText, PicUrl = rootElement.SelectSingleNode("PicUrl").InnerText, MediaId = rootElement.SelectSingleNode("MediaId").InnerText };
                    break;
                case ReceiveMsg.TYPE_VOICE:
                    retVal = new ReceiveVoiceMsg() { MsgId = rootElement.SelectSingleNode("MsgId").InnerText, Format = rootElement.SelectSingleNode("Format").InnerText, MediaId = rootElement.SelectSingleNode("MediaId").InnerText };
                    break;
                case ReceiveMsg.TYPE_LOCATION: //位置
                    break;
                case ReceiveMsg.TYPE_LINK: //链接  
                    break;
                case ReceiveMsg.TYPE_EVENT: //事件推送 支持V4.5+  
                    retVal = new ReceiveEventMsg() { Event = rootElement.SelectSingleNode("Event").InnerText, EventKey = rootElement.SelectSingleNode("EventKey").InnerText };
                    break;
            }

            //接收的值--->接收消息类(也称为消息推送)  
            retVal.ToUserName = rootElement.SelectSingleNode("ToUserName").InnerText;
            retVal.FromUserName = rootElement.SelectSingleNode("FromUserName").InnerText;

            int ct = int.Parse(rootElement.SelectSingleNode("CreateTime").InnerText);
            retVal.CreateTime = SecondToDateTime(ct);

            //            retVal.MsgType = MsgType.InnerText;

            return retVal;
        }



        //加载字符串消息
        public static string PostSendMsgJson(string url, string msg)
        {
            return HttpUtil.HttpPost(
                url,
                msg
                );
        }
        public static string PostSendMsgJson(string url, ISendMsg msg)
        {
            string postStr = "";
            if (msg != null)
            {
                postStr = msg.ToJson();
            }
            return PostSendMsgJson(
                url,
                postStr
                );
        }

        public static string PostSendMsgJson(string url, string accessToken, ISendMsg msg)
        {
            return PostSendMsgJson(
                url + "?access_token=" + accessToken,
                msg
                );
        }


        public static string GetSendMsgJson(string url, string param)
        {
            return HttpUtil.HttpGet(
                url,
                param
                );
        }

        public static string GetSendMsgJson(string url, string accessToken, string param)
        {
            return GetSendMsgJson(
                url + "?access_token=" + accessToken,
                param
                );
        }

        public static Token GetAccessToken(string url)
        {
            string getStr = HttpUtil.HttpGet(url, "grant_type=client_credential&appid=" + AppId + "&secret=" + Appsecret);
            return JsonUtil.parse<Token>(getStr);
        }

        public static JsApiTicket GetJsApiTicket(string url,string accessToken)
        {
            string getStr = HttpUtil.HttpGet(url, "access_token=" + accessToken + "&type=jsapi");
            return JsonUtil.parse<JsApiTicket>(getStr);
        }

        public static Oauth2Token GetOauth2Token(string url, string code)
        {
            string getStr = HttpUtil.HttpGet(url, "appid=" + AppId + "&secret=" + Appsecret + "&code="+code+"&grant_type=authorization_code");
            return JsonUtil.parse<Oauth2Token>(getStr);
        }
    }
}