/// <reference path="animeHomeCtrl.ts" />
/// <reference path="../Manga/mangaDataService.ts" />
/// <reference path="../Directives/JdRow/JdRow.ts" />

module Home {
    "use strict";

    const app = angular.module("homeApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    app.controller("animeHomeCtrl", JdAnime.AnimeHomeCtrl);
        //.directive("jdRow", JdRowDirective.JdRowDirective.factory());

    app.service("mangaDataService", Manga.MangaDataService);
}
