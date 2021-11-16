/// <reference path="mangaPageCtrl.ts" />
/// <reference path="../mangaDataService.ts" />

module Manga {
    "use strict";

    const app = angular.module("mangaPageApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    app.controller("mangaPageCtrl", Manga.MangaPageCtrl);
    //.directive("jdRow", JdRowDirective.JdRowDirective.factory());

    app.service("mangaDataService", Manga.MangaDataService);
}
