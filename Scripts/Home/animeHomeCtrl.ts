/// <reference path="animeDataService.ts" />

namespace JdAnime {
    "use strict";
    export class AnimeHomeCtrl implements ng.IController, IAnimeHomeCtrl {
        static $inject = ["$scope", "$http", "animeDataService"];

        constructor(
            private readonly $scope: ng.IScope,
            private readonly $http: ng.IHttpService,
            private readonly animeDataService: IAnimeDataService,
            private displayText: string, //for testing
            public animeSeason: ISeason,
            public searchText: string
        ) {
            this.displayText = "TEST CTRL";
            this.searchText = "";
        }

        searchAllMediaByName(): void {
            var filterCollection: ISearchFilter[] = [];
            filterCollection.push(
                { SearchCategory: SearchCategoryEnum.Anime, Name: this.searchText },
                { SearchCategory: SearchCategoryEnum.Manga, Name: this.searchText }
            );
            this.animeDataService.loadSearchResults(filterCollection)
                .then((data: any | void) => {
                    this.displayText = data as string;
                },
                    msg => console.log("ERROR:", msg));
        }

        getScrapedMangaImageUrls(): void {
            this.animeDataService.loadScrapedMangaImageUrls()
                .then((data: any | void) => {
                    this.displayText = data as string;
                },
                    msg => console.log("ERROR:", msg));
        }

        getCurrentSeasonAnime(): void {
            this.animeDataService.loadCurrentSeasonAnime()
                .then((data: ISeason | void) => {
                    this.animeSeason = data as ISeason;
                },
                    msg => console.log("ERROR:", msg));
        }

        getSeasonalAnime(year: number, season: string): void {
            this.animeDataService.loadSeasonalAnime(year, season)
                .then((data: any) => {
                    this.displayText = data;
                },
                    msg => console.log("ERROR:", msg));
        }

        getAnimeStats(): void {
            var url = "/Home/HomeApi/GetAnimeStats/1";
            this.$http.get(url)
                .then((data: any) => {
                    this.displayText = data.data;
                },
                    msg => console.log("ERROR:", msg));
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
