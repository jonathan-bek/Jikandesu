/// <reference path="mangaCtrl.ts" />
/// <reference path="mangaDataService.ts" />

module Manga {
    "use strict";

    const app = angular.module("mangaApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    app.controller("mangaCtrl", Manga.MangaCtrl)
    app.service("mangaDataService", Manga.MangaDataService)
}
