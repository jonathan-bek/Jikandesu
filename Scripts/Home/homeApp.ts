/// <reference path="animeHomeCtrl.ts" />
/// <reference path="animeDataService.ts" />
/// <reference path="../Directives/JdRowItem/JdRowItem.ts" />

module Home {
    "use strict";

    const app = angular.module("homeApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    app.controller("animeHomeCtrl", JdAnime.AnimeHomeCtrl)
        .directive("jdRowItem", JdRowItemDirective.JdRowItemDirective.factory());

    app.service("animeDataService", JdAnime.AnimeDataService)
}
