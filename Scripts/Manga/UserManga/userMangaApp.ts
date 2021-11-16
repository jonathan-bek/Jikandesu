/// <reference path="userMangaCtrl.ts" />
/// <reference path="../mangaDataService.ts" />

module Manga {
    "use strict";

    const app = angular.module("userMangaApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    app.controller("userMangaCtrl", Manga.UserMangaCtrl)
    app.service("mangaDataService", Manga.MangaDataService)
}
