"use strict";
var Home;
(function (Home) {
    "use strict";
    var HomeCtrl = (function () {
        function HomeCtrl($scope, $http, displayText) {
            this.$scope = $scope;
            this.$http = $http;
            this.displayText = displayText;
            this.displayText = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }
        HomeCtrl.prototype.getAnimeStats = function () {
            var _this = this;
            var url = "/Home/HomeApi/GetAnimeStats/1";
            this.$http.get(url)
                .then(function (data) {
                _this.displayText = data.data;
            }, function (msg) { return console.log("ERROR:", msg); });
        };
        HomeCtrl.prototype.getInfoFromDb = function () {
            var _this = this;
            var url = "/Home/HomeApi/GetInfoFromDb";
            this.$http.get(url)
                .then(function (data) {
                _this.displayText = data.data;
            }, function (msg) { return console.log("ERROR:", msg); });
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