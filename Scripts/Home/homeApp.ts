/// <reference path="homeCtrl.ts" />
/// <reference path="homeDataService.ts" />

module Home {
    "use strict";

    const app = angular.module("homeApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    //Include controllers
    app.controller("homeCtrl", HomeCtrl);
    app.service("homeDataService", HomeDataService)
}