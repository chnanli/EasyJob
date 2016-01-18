var lastClickMenu=-1; //上一次点击的菜单

var app = angular.module('indexApp', []);

app.controller('indexCtrl', function ($scope,$http) {
    $http.get("Admin/GetMenuBar").success(function (menuBar) {
        $scope.menuBar = menuBar;

        BUI.use('common/main', function () {
            new PageUtil.MainPage({
                modulesConfig: menuBar
            });
        });
    });
});
