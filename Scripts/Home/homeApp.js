"use strict";
var Home;
(function (Home) {
    "use strict";
    var app = angular.module("homeApp", []);
    app.config(["$locationProvider", function ($locationProvider) {
            $locationProvider.html5Mode({ enabled: true, requireBase: false });
        }]);
    app.controller("homeCtrl", Home.HomeCtrl);
})(Home || (Home = {}));
//# sourceMappingURL=homeApp.js.map