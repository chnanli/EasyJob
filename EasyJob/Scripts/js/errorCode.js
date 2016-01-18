var errorMsgs = [
{ "code": "UserNameOrPwdError", "text": "用户名或者密码错误！" },
{ "code": "IsNoLogin", "text": "没有登录！" },
{ "code": "AddrCodeError", "text": "地址码错误！" },
{ "code": "AddrCodeIsNotFound", "text": "地址码不存在！" },
{ "code": "WorkTypeDetailIsNotFound", "text": "维修类型不存在！" },
{ "code": "WorkIsNotFound", "text": "工单不存在！" },
{ "code": "EmployeeIsNotFound", "text": "员工不存在！" }
];

//由错误代码获取错误信息
function GetErrorMsg(code) {
    for (i = 0; i < errorMsgs.length; i++) {
        var em = errorMsgs[i];
        if (code == em.code) {
            return em.text;
        }
    }
    return code;
}