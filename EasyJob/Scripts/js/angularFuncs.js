//angular HTTP GET请求
function HttpGet(http, url, param, success, error) {
    http.get(url+"?"+param).success(success).error(
    function (data, header, config, status) {
        if (data != null && data.Error != null) {
            data.ErrorMsg = GetErrorMsg(data.Error);
        }
        error(data, header, config, status);
    }
    );
}

//angular HTTP POST请求
function HttpPost(http, url, param, success, error) {
    http.post(url,param).success(success).error(
    function (data, header, config, status) {
        if (data != null && data.Error != null) {
            data.ErrorMsg = GetErrorMsg(data.Error);
        }
        error(data, header, config, status);
    }
    );
}

//小区查询函数
function HttpGetGarden(http, param, success, error) {
    HttpGet(http, "/Garden/Get", param, success, error);
}

//小区添加函数
function HttpAddGarden(http, data, success, error) {
    HttpPost(http, "/Garden/Add", data, success, error);
}

//小区删除函数
function HttpDelGarden(http, data, success, error) {
    HttpPost(http, "/Garden/Del", data, success, error);
}

//小区修改函数
function HttpUpdateGarden(http, data, success, error) {
    HttpPost(http, "/Garden/Update", data, success, error);
}

//客户查询函数
function HttpGetCustomer(http, param, success, error) {
    HttpGet(http, "/Customer/Get", param, success, error);
}

//客户添加函数
function HttpAddCustomer(http, data, success, error) {
    HttpPost(http, "/Customer/Add", data, success, error);
}

//客户删除函数
function HttpDelCustomer(http, data, success, error) {
    HttpPost(http, "/Customer/Del", data, success, error);
}

//客户修改函数
function HttpUpdateCustomer(http, data, success, error) {
    HttpPost(http, "/Customer/Update", data, success, error);
}

//客户地址查询函数
function HttpGetCustomerAddr(http, data, success, error) {
    HttpPost(http, "/CustomerAddr/Get", data, success, error);
}

//工单查询函数
function HttpGetWork(http, param, success, error) {
    HttpGet(http, "/Work/Get", param, success, error);
}

//工单添加函数
function HttpAddWork(http, data, success, error) {
    HttpPost(http, "/Work/Add", data, success, error);
}

//工单删除函数
function HttpUpdateWork(http, data, success, error) {
    HttpPost(http, "/Work/Update", data, success, error);
}

//工单删除函数
function HttpDelWork(http, data, success, error) {
    HttpPost(http, "/Work/Del", data, success, error);
}

//子工单查询函数
function HttpGetWorkSub(http, data, success, error) {
    HttpPost(http, "/Work/GetWorkSub", data, success, error);
}

//子工单维修详细查询函数
function HttpGetWorkDetail(http, data, success, error) {
    HttpPost(http, "/Work/GetWorkDetail", data, success, error);
}

//工单评价查询函数
function HttpGetCustomerScore(http, data, success, error) {
    HttpPost(http, "/Work/GetCustomerScore", data, success, error);
}

//维修代码查询函数
function HttpGetWorkCodeType(http, data, success, error) {
    HttpGet(http, "/WorkCodeType/Get", data, success, error);
}

//维修代码1查询函数
function HttpGetWorkCodeType1(http, data, success, error) {
    HttpGet(http, "/WorkCodeType/GetWorkCodeType1", data, success, error);
}

//维修代码2查询函数
function HttpGetWorkCodeType2(http, data, success, error) {
    HttpGet(http, "/WorkCodeType/GetWorkCodeType2", data, success, error);
}

//维修代码添加函数
function HttpAddWorkCodeType(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/Add", data, success, error);
}

//维修代码删除函数
function HttpDelWorkCodeType(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/Del", data, success, error);
}

//维修代码1修改函数
function HttpUpdateWorkCodeType(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/Update", data, success, error);
}

//维修代码1添加函数
function HttpAddWorkCodeType1(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/AddWorkCodeType1", data, success, error);
}

//维修代码1删除函数
function HttpDelWorkCodeType1(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/DelWorkCodeType1", data, success, error);
}

//维修代码1修改函数
function HttpUpdateWorkCodeType1(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/UpdateWorkCodeType1", data, success, error);
}

//维修代码2添加函数
function HttpAddWorkCodeType2(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/AddWorkCodeType2", data, success, error);
}

//维修代码2删除函数
function HttpDelWorkCodeType2(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/DelWorkCodeType2", data, success, error);
}

//维修代码2修改函数
function HttpUpdateWorkCodeType2(http, data, success, error) {
    HttpPost(http, "/WorkCodeType/UpdateWorkCodeType2", data, success, error);
}