function GetPageCount(url, pageId, showId, showFunc, method, pageSize, pageNum) {
    var pageCount = 0;
//    $("#" + pageId).html("loading...");
    $.ajax({
        type: method,
        url: url+'?pageSize=' + pageSize,
        contentType: "application/json",
        dataType: "json",
        data: {},
        success: function (d) {
            pageCount = d;
            $("#" + pageId).html(GetPageControl(showId, showFunc, pageSize, pageCount, pageNum,5));
        }
    });
    return pageCount;
}

function GetOnClick(funcName,pageSize,pageNum) {
    return funcName + "(" + pageSize + ", " + pageNum + ");";
}

function GetOnClickValById(funcName, pageSize, id) {
    return funcName + "(" + pageSize + ",$('#"+id+"').val()*1);";
}

function GetPageControl(showId, showFunc, pageSize, pageCount, pageNum,maxBtnNum) {
    var onclick;
    var page = "<ul class='pagination'>";

    if (pageNum > 1) {
        onclick = GetOnClick(showFunc, pageSize, pageNum - 1);
    } else {
        onclick = "";
    }
    page += "<li><a onclick=\"" + onclick + "\">&laquo;</a></li>"; //上一页

    try {
        var modNum = maxBtnNum % 2;
        var tempBtnNum = maxBtnNum - modNum;//减去余数方便下面计算为整数
        var centerNum = tempBtnNum / 2;

        var leftNum=1;
        var rightNum=1;
        //如果分页数小于最多按钮数
        if (pageCount < maxBtnNum) {
            leftNum = 1;
            rightNum = pageCount;
        } else {
            //如果当前页小于中间+余数
            if (pageNum < centerNum + modNum) {
                leftNum = 1;
                rightNum = maxBtnNum;
            }
            //如果当前页大于最大页数-中间
            else if (pageNum>pageCount-centerNum) {
                leftNum = pageCount-maxBtnNum+1;
                rightNum = pageCount;
            }
            else {
                leftNum = pageNum - centerNum - modNum+1;
                rightNum = pageNum + centerNum;
            }
        }
        
        for (var i = leftNum; i <= rightNum; i++) {
            onclick = GetOnClick(showFunc, pageSize, i);
            if (i == pageNum) {
                page += "<li class='active'><a>" + i + "</a></li>";
            } else {
                page += "<li><a onclick=\"" + onclick + "\">" + i + "</a></li>";
            }
        }
    } finally {
        if (pageNum < pageCount) {
            onclick = GetOnClick(showFunc, pageSize, pageNum + 1);
        } else {
            onclick = "";
        }
        page += "<li><a onclick=\"" + onclick + "\">&raquo;</a></li>"; //下一页

        onclick = GetOnClickValById(showFunc, pageSize, 'inputPageNum');
        page += "<li> 共" + pageCount + "页 <input size='2' id='inputPageNum' type='text' value='" + pageNum + "' ></input><button id='btnGoPageNum' type='button' class='btn btn-primary btn-sm' onclick=\"" + onclick + "\">GO</button></li>";
        page += "</ul>";

//        onclick = GetOnClickValById(showFunc, pageSize, 'pageNum');
//        page += "</br><span>共" + pageCount + "页,<input size='2' id='pageNum' type='text' value='" + pageNum + "' ></input><button type='button' class='btn btn-primary btn-sm' onclick=\"" + onclick + "\">GO</button></span>";
    }
    return page;
}