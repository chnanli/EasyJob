using EasyJob.Controllers.Api;
using EasyJob.WeiXinCtl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Tools;
using WeiXin.Tools;
using WeiXin.WeiXin.Receive;
using WeiXin.WeiXin.Send;

namespace EasyJob.Controllers.WeiXin
{
    public class WeiXinController : BaseController
    {
        const string Token = "Firefly";  //你的token

        private const string TokenUrl = "https://api.weixin.qq.com/cgi-bin/token";
        private const string JsApiTicketUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket";
        private const string CustomSendUrl = "https://api.weixin.qq.com/cgi-bin/message/custom/send";//客服发送URL
        private const string MenuCreateUrl = "https://api.weixin.qq.com/cgi-bin/menu/create";
        private const string MenuDelUrl = "https://api.weixin.qq.com/cgi-bin/menu/delete";
        private const string Oauth2Url = "https://open.weixin.qq.com/connect/oauth2/authorize";
        private const string Oauth2TokenUrl = "https://api.weixin.qq.com/sns/oauth2/access_token";

        public static WeiXinPublic weiXinPublic = new WeiXinPublic(
            TokenUrl,
            JsApiTicketUrl,
            MenuCreateUrl,
            MenuDelUrl,
            Oauth2Url,
            Oauth2TokenUrl,
            "wx902209cd6a17ebf6",
            "d4624c36b6795d1d99dcf0547af5443d"
            );

        [HttpGet]
        public ActionResult Index(string signature, string timestamp, string nonce, string echostr)
        {
            if (signature == null)
            {
                signature = "";
            }
            //验证微信签名
            if (signature.Equals(WeiXinUtil.Signature(Token, timestamp, nonce)))
            {
                return Content(echostr);
            }
            else
            {
                return Content("");
            }
        }

        [HttpPost]
        public ActionResult Index()
        {
            Stream requestStream = System.Web.HttpContext.Current.Request.InputStream;
            string requestStr = StreamUtil.StreamToStr(requestStream);

            //由POST过来的字符串转成微信消息对象
            ReceiveMsg msg = WeiXinUtil.XmlToReceiveMsg(requestStr);
            //处理消息
            Digest(msg);

            return Content("success");
        }

        private void Digest(ReceiveMsg msg)
        {
            SendTextMsg textMsg = null;
            switch (msg.MsgType)
            {
                case ReceiveMsg.TYPE_TEXT:
                    textMsg = new SendTextMsg()
                    {
                        touser = msg.FromUserName
                    };
                    textMsg.text.content = ((ReceiveTextMsg)msg).Content;

                    WeiXinUtil.PostSendMsgJson(CustomSendUrl, weiXinPublic.AccessToken.access_token, textMsg);
                    //WeiXinMsgSend.PostMsgJson(weiXinPublic.AccessToken.access_token, tmj);
                    break;
                case ReceiveMsg.TYPE_EVENT:
                    textMsg = new SendTextMsg()
                    {
                        touser = msg.FromUserName
                    };
                    textMsg.text.content = ((ReceiveEventMsg)msg).Event;

                    WeiXinUtil.PostSendMsgJson(CustomSendUrl, weiXinPublic.AccessToken.access_token, textMsg);
                    break;
            }
        }
    }
}
