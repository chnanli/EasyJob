using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Security;
using WeiXin.Tools;
using WeiXin.WeiXin.Cert;
using WeiXin.WeiXin.Send;

namespace EasyJob.WeiXinCtl
{
    public class WeiXinPublic
    {
        public enum Scope
        {
            snsapi_base,
            snsapi_userinfo
        }

        public class JsApiValidInfo
        {
            public string Signature { get; set; }
            public string Timestamp { get; set; }
            public string Nonce { get; set; }
        }

        private const int GetAccessTokenSecond = 6000;

        private bool isCreateMenu = false;

        public string AccessTokenUrl { get; set; }
        public string JsApiTicketUrl { get; set; }
        public string MenuCreateUrl { get; set; }
        public string MenuDelUrl { get; set; }
        public string AppId { get; set; }
        public string Appsecret { get; set; }
        public Token AccessToken { get; set; }
        public JsApiTicket JsApiTicket { get; set; }

        public string Oauth2Url { get; set; }
        public string Oauth2TokenUrl { get; set; }

        public WeiXinPublic(
            string accessTokenUrl, 
            string jsApiTicketUrl, 
            string menuCreateUrl, 
            string menuDelUrl, 
            string oauth2Url,
            string oauth2TokenUrl,
            string appId, 
            string appsecret)
        {
            AccessTokenUrl = accessTokenUrl;
            JsApiTicketUrl = jsApiTicketUrl;
            MenuCreateUrl = menuCreateUrl;
            MenuDelUrl = menuDelUrl;
            Oauth2Url = oauth2Url;
            Oauth2TokenUrl = oauth2TokenUrl;

            AppId = appId;
            Appsecret = appsecret;

            WeiXinUtil.AppId = appId;
            WeiXinUtil.Appsecret = appsecret;

            Thread getAccessToken = new Thread(GetAccessToken);
            getAccessToken.Start();
        }


        private void GetAccessToken()
        {
            DateTime refDateTime = DateTime.Now.AddSeconds(-GetAccessTokenSecond);
            while (true)
            {
                try
                {
                    TimeSpan span = (TimeSpan)(DateTime.Now - refDateTime);
                    //如果大于6000秒
                    if (span.TotalSeconds >= GetAccessTokenSecond)
                    {
                        
                        //获取Token
                        AccessToken = WeiXinUtil.GetAccessToken(AccessTokenUrl);

                        if (AccessToken.access_token != null)
                        {
                            JsApiTicket=WeiXinUtil.GetJsApiTicket(JsApiTicketUrl, AccessToken.access_token);
                            if (JsApiTicket.ticket != null)
                            {
                                refDateTime = DateTime.Now;
                            }
                            if (isCreateMenu == false)
                            {
                                CreateMenu();
                            }
                        }
                    }
                    Thread.Sleep(2000);
                }
                catch (Exception e)
                {

                }
            }
        }


        public JsApiValidInfo GetJsApiValidInfo(string url)
        {
            JsApiValidInfo retVal = null;
            if (JsApiTicket != null && JsApiTicket.ticket != null)
            {
                string timestamp = WeiXinUtil.DateTimeToSecond(DateTime.Now).ToString();
                string jsapi_ticket = JsApiTicket.ticket;
                string noncestr = "Wm3WZYTPz0wzccnW";

                string str = "jsapi_ticket=" + jsapi_ticket + "&noncestr=" + noncestr + "&timestamp=" + timestamp + "&url=" + url;
                string signature = FormsAuthentication.HashPasswordForStoringInConfigFile(str, "SHA1").ToLower();

                retVal = new JsApiValidInfo();
                retVal.Signature = signature;
                retVal.Timestamp = timestamp;
                retVal.Nonce = noncestr;
            }
            return retVal;
        }

        /// <summary>
        /// Url转码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string UrlEncode(string url)
        {
            return HttpUtility.UrlEncode(url);
        }

        public string GetOauth2Url(string url, Scope scope, string state)
        {
            url = UrlEncode(url);
            string retVal = Oauth2Url + "?appid=" + AppId + "&redirect_uri=" + url + "&response_type=code&scope="+scope.ToString()+"&state="+state+"#wechat_redirect";
            return retVal;
        }

        private void CreateMenu()
        {
            string baseUrl = "http://myhelloworld.vicp.net/";
            //删除菜单
            WeiXinUtil.GetSendMsgJson(
                MenuDelUrl,
                AccessToken.access_token,
                "");

            //创建菜单
            SendMenuMsg menuBar = new SendMenuMsg();

            SendMenuMsg.ViewMenu viewMenu = null;
            SendMenuMsg.MainMenu mainMenu = null;

            viewMenu = new SendMenuMsg.ViewMenu();
            viewMenu.name = "订单";
            viewMenu.url = GetOauth2Url(baseUrl + "EasyJob/RepairOrder?id=123", Scope.snsapi_base, "test");
            menuBar.button.Add(viewMenu);

            mainMenu = new SendMenuMsg.MainMenu();
            mainMenu.name = "我的Easy";
            mainMenu.sub_button = new List<Object>();
            menuBar.button.Add(mainMenu);

            viewMenu = new SendMenuMsg.ViewMenu();
            viewMenu.name = "我的资料";
            viewMenu.url = GetOauth2Url(baseUrl + "EasyJob/RepairOrder?id=123", Scope.snsapi_userinfo, "test");
            mainMenu.sub_button.Add(viewMenu);

            viewMenu = new SendMenuMsg.ViewMenu();
            viewMenu.name = "师傅登录";
            viewMenu.url = baseUrl + "WeiXin/Order";
            mainMenu.sub_button.Add(viewMenu);

            viewMenu = new SendMenuMsg.ViewMenu();
            viewMenu.name = "推荐码";
            viewMenu.url = baseUrl + "WeiXin/Order";
            mainMenu.sub_button.Add(viewMenu);

            viewMenu = new SendMenuMsg.ViewMenu();
            viewMenu.name = "订单记录";
            viewMenu.url = baseUrl + "WeiXin/Order";
            mainMenu.sub_button.Add(viewMenu);

            string json = menuBar.ToJson();
            string retMenuInfo = WeiXinUtil.PostSendMsgJson(
                MenuCreateUrl,
                AccessToken.access_token,
                menuBar
                );
            isCreateMenu = true;
        }
    }
}