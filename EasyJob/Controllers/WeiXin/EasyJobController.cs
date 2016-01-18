using EasyJob.Controllers.Api;
using EasyJob.WeiXinCtl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeiXin.Tools;
using WeiXin.WeiXin.Cert;

namespace EasyJob.Controllers.WeiXin
{
    public class EasyJobController : BaseController
    {
        private WeiXinPublic weiXinPublic;
        public EasyJobController()
        {
            weiXinPublic = WeiXinController.weiXinPublic;
        }
        /// <summary>
        /// 维修订单
        /// </summary>
        /// <returns></returns>
        public ActionResult RepairOrder(string code,string id)
        {
            //获取微信用户的OpenId
            //Oauth2Token token = WeiXinUtil.GetOauth2Token(weiXinPublic.Oauth2TokenUrl, code);
            //ViewBag.OpenId = token.openid;
            ViewBag.OpenId = "odS9wwl5R6yhoEJoWWkSK0LLPdIM";

            //调用微信JS接口的验证信息
            //string url = GetUrl();
            //WeiXinPublic.JsApiValidInfo javi = weiXinPublic.GetJsApiValidInfo(
            //    url
            //    );

            //ViewBag.JsApiValidInfo = javi;

            return View();
        }
    }
}
