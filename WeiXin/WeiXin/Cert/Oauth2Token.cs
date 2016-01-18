using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeiXin.WeiXin.Cert
{
    public class Oauth2Token
    {
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public string refresh_token { get; set; }
        public string openid { get; set; }
        public string scope { get; set; }

        /// <summary>
        /// 只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
        /// </summary>
        public string unionid { get; set; }
    }
}