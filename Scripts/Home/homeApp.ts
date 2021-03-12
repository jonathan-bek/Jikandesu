﻿/// <reference path="animeHomeCtrl.ts" />
/// <reference path="animeDataService.ts" />

module Home {
    "use strict";

    const app = angular.module("homeApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    //Include controllers
    app.controller("animeHomeCtrl", JdAnime.AnimeHomeCtrl);
    app.service("animeDataService", JdAnime.AnimeDataService)
}
