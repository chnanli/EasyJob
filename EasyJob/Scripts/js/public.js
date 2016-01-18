var EmpModFuncs;

function GetEmpModFuncs() {
    $.ajax({
        type: "GET",
        url: "ServiceEmployee/EmpModFunc",
        contentType: "application/json",
        dataType: "json",
        data: {},
        success: function (data) {
            EmpModFuncs = data;
        }
    });
}

function IsHavePower(serviceBase, funcName) {
    var retVal = false;
    for (i = 0; i < EmpModFuncs.length; i++) {
        var EmpModFunc = EmpModFuncs[i];
        if (EmpModFunc.ModFunc.Cls == serviceBase ) {
            var find=EmpModFunc.FuncNames.indexOf("|"+funcName+"|");
            if (find > -1) {
                retVal = true;
                break;
            }
        }
    }
    return retVal;
}

//获取员工接口权限
GetEmpModFuncs();