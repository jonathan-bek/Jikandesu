/// <reference path="animeDataService.ts" />

namespace JdAnime {
    "use strict";
    export class AnimeHomeCtrl implements ng.IController, IAnimeHomeCtrl {
        static $inject = ["$scope", "$http", "animeDataService"];

        constructor(
            private readonly $scope: ng.IScope,
            private readonly $http: ng.IHttpService,
            private readonly animeDataService: IAnimeDataService,

            public animePageNumber: number,
            public pagedAnimeHeader: ISearchResult[],
            public animeHeader: ISearchResult[],


            public mangaPageNumber: number,
            public pagedMangaHeader: ISearchResult[],
            public mangaHeader: ISearchResult[],

            public animeSeason: ISeason,
            public searchText: string,
            public displayText: string //for testing
        ) {
            this.animePageNumber = 1;
            this.pagedAnimeHeader = [];
            this.animeHeader = [];

            this.mangaPageNumber = 1;
            this.pagedMangaHeader = [];
            this.mangaHeader = [];

            this.searchText = "";
            this.displayText = "Display Text";
        }

        private pageSize: number = 5;

        searchAllMediaByName(): void {
            var filterCollection: ISearchFilter[] = [];
            filterCollection.push(
                { SearchCategory: SearchCategoryEnum.Anime, Name: this.searchText },
                { SearchCategory: SearchCategoryEnum.Manga, Name: this.searchText }
            );
            this.animeDataService.loadSearchResults(filterCollection)
                .then((data: any | void) => {
                    this.animeHeader = data.AnimeResult;
                    this.pagedAnimeHeader = this.setPaginatedData(this.animeHeader,
                        this.animePageNumber) as ISearchResult[];
                    this.mangaHeader = data.MangaResult;
                    this.pagedMangaHeader = this.setPaginatedData(this.mangaHeader,
                        this.mangaPageNumber);
                },
                    msg => console.log("ERROR:", msg));
        }

        private setPaginatedData(all: any[], pageNumber: number): any[] {
            var end = pageNumber * this.pageSize;
            var start = end - this.pageSize;
            return all.slice(start, end);
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
