/// <reference path="../mangaDataService.ts" />

namespace Manga {
    "use strict";
    export class UserMangaCtrl implements ng.IController {
        static inject = ["mangaDataService"];

        constructor(
            private readonly $scope: ng.IScope,
            private readonly mangaDataService: IMangaDataService,
        ) {
            this.mangaPages = [];
        }

        mangaPages: IMangaPage[];

        $onInit() {
            this.getUserManga();
        }

        private getUserManga() {
            this.mangaDataService.getUserManga()
                .then((data) => {
                    this.mangaPages = data as IMangaPage[];
                });
        }
    }
}