"use strict";
var Home;
(function (Home) {
    "use strict";
    var HomeCtrl = (function () {
        function HomeCtrl($scope, $http, testCtrl) {
            this.$scope = $scope;
            this.$http = $http;
            this.testCtrl = testCtrl;
            this.testCtrl = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }
        HomeCtrl.prototype.getInt = function () {
            return this.$http.get("/Home/HomeApi/GetInt")
                .then(function (res) { return res; }, function (msg) { return console.log(msg); });
        };
        HomeCtrl.prototype.getIntFromDb = function () {
            var _this = this;
            this.getInt().then(function (data) {
                _this.testCtrl = data.data;
            });
        };
        HomeCtrl.$inject = ["$scope", "$http"];
        return HomeCtrl;
    }());
    Home.HomeCtrl = HomeCtrl;
})(Home || (Home = {}));
var Home;
(function (Home) {
    "use strict";
    var app = angular.module("homeApp", []);
    app.config(["$locationProvider", function ($locationProvider) {
            $locationProvider.html5Mode({ enabled: true, requireBase: false });
        }]);
    app.controller("homeCtrl", Home.HomeCtrl);
})(Home || (Home = {}));
//# sourceMappingURL=HomeBundle.js.map