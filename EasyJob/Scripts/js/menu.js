var lastClickMenu; //上一次点击的菜单

function loadMenu(mainId,id) {
    $.ajax({
        type: "GET",
        url: "ServiceMod",
        contentType: "application/json",
        dataType: "json",
        data: {},
        success: function (menus) {
            showMenu(mainId,id, menus);

            for (i = 0; i < menus.length; i++) {
                var menu = menus[i];
                if (menu.Type == 0) {
                    clickMenu(mainId,menu.Name, menu.Href);
                    break;
                }
            }

        }
    });
}

function clickMenu(mainId,id, href) {
	//恢复上一次点的按钮样式
	if(lastClickMenu!=""){
		$("#"+lastClickMenu).attr("class","");
	}
	
	//设置当前按钮的样式
	$("#"+id).attr("class","active");
	lastClickMenu=id;

	if (href != "") {
	    //加载连接
	    clickSubMenu(mainId,id, href);
	}
}

function clickSubMenu(mainId,id, href) {
    $("#" + mainId).html("loading...");
	$.ajax({
        type: "GET",
        url: href,
        contentType: "application/html",
        dataType: "html",
        data: {},
        success: function (d) {
            $("#"+mainId).html(d);  
        }
    });
}

function showMenu(mainId,id, menus) {
//    var Menustr = '[{"Name":"Index","Text":"首页","Type":"0","Href":"pages/index.html"},{"Name":"SystemManager","Text":"系统管理","Type":"1","Href":"systemSetting","Menus":[{"Name":"UserManager","Text":"用户管理","Type":"0","Href":"pages/userManager.html"},{"Name":"MenuManager","Text":"菜单管理","Type":"0","Href":"pages/menuManager.html"}]}]';
	
//	var menus=$.parseJSON(Menustr);
	
	var menusHtml='<ul id="main-nav" class="nav nav-tabs nav-stacked" style="">';
	for(i=0;i<menus.length;i++){
		var menu=menus[i];
		var clickMenu;

		//如果没有子菜单
		if(menu.Type==0){
		    clickMenu = "onClick='clickMenu(\"" + mainId + "\",\"" + menu.Name + "\",\"" + menu.Href + "\")'";

		    menusHtml += '<li id="' + menu.Name + '"><a ' + clickMenu + ' ><i class="'+menu.Icon+'"></i>' + menu.Text + '</a></li>';
		}
		//如果有子菜单
		else if(menu.Type==1){
		    clickMenu = "onClick='clickMenu(\"" + mainId + "\",\"" + menu.Name + "\",\"\")'";

		    menusHtml += '<li id="' + menu.Name + '">';
		    menusHtml += '	<a href="#' + menu.Name + "_href" + '" ' + clickMenu + ' class="nav-header collapsed" data-toggle="collapse">';
	        menusHtml+='		<i class="'+menu.Icon+'"></i>系统管理';
	        menusHtml+='		<span class="pull-right glyphicon glyphicon-chevron-down"></span>';
	        menusHtml+='	</a>';
	        
	        if(menu.Menus.length>0){
	            menusHtml += '<ul id="' + menu.Name + "_href" + '" class="nav nav-list collapse secondmenu" style="height: 0px;">';
	        	
	        	for(j=0;j<menu.Menus.length;j++){
	        	    var m = menu.Menus[j];

	        	    clickMenu = "onClick='clickSubMenu(\"" + mainId + "\",\"" + m.Name + "\",\"" + m.Href + "\")'";
	        		
	        		menusHtml+='<li><a '+clickMenu+'><i class="'+m.Icon+'"></i>'+m.Text+'</a></li>';
	        	}
	        	
	        	menusHtml+='</ul>';
	        }               
	        menusHtml+='</li>';
		}
	}
	menusHtml+='</ul>';
	
	$("#"+id).html(menusHtml);
}
