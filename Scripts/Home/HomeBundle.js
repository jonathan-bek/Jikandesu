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
        HomeCtrl.prototype.getAnimeStatsFromApi = function () {
            return this.$http.get("/Home/HomeApi/GetAnimeStats/1")
                .then(function (res) { return res; }, function (msg) { return console.log(msg); });
        };
        HomeCtrl.prototype.getAnimeStats = function () {
            var _this = this;
            this.getAnimeStatsFromApi().then(function (data) {
                _this.displayText = data.data;
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