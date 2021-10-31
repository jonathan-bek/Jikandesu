/// <reference path="animeDataService.ts" />

namespace JdAnime {
    "use strict";
    export class AnimeHomeCtrl implements ng.IController, IAnimeHomeCtrl {
        static $inject = ["$scope", "$http", "animeDataService"];

        mangaPage: IMangaPage | undefined;

        constructor(
            private readonly $scope: ng.IScope,
            private readonly $http: ng.IHttpService,
            private readonly animeDataService: IAnimeDataService,

            public searchText: string,
            public displayText: string, //for testing
            public mangaUrl: string
        ) {
            this.searchText = "";
            this.displayText = "Display Text";
            this.mangaUrl = "";
        }

        private pageSize: number = 5;

        getMangaPage(): void {
            this.animeDataService.getMangaPage(this.mangaUrl)
                .then((data: IMangaPage | void) => {
                    this.mangaPage = data as IMangaPage;
                })
        }

        saveMangaPage(): void {
            this.animeDataService.saveMangaPage(this.mangaPage as IMangaPage);
        }

        testDb(): void {
            this.animeDataService.testDb()
                .then((data: any) => {
                    this.displayText = data;
                },
                    msg => console.log("ERROR:", msg));
        }
    }
}
