module Manga {
    "use strict";
    const app = angular.module("mangaApp", []);
    //Allow $location to work
    app.config(["$locationProvider", ($locationProvider: ng.ILocationProvider) => {
        $locationProvider.html5Mode({ enabled: true, requireBase: false });
    }]);

    //app.controller("animeHomeCtrl", JdAnime.AnimeHomeCtrl)
    //    .directive("jdRow", JdRowDirective.JdRowDirective.factory());

    app.service("mangaDataService", Manga.MangaDataService)
}
