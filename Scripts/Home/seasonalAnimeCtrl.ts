/// <reference path="animeDataService.ts" />

namespace JdAnime {
    "use strict";

    export interface ISeasonalAnimeCtrl {
        animeSeason: ISeason;
        getCurrentSeasonAnime: () => void;
        getSeasonalAnime: (year: number, season: string) => void;
        getAnimeStats: () => void;
    }

    export class SeasonalAnimeCtrl implements ng.IController, ISeasonalAnimeCtrl {
        static $inject = ["$scope", "$http", "animeDataService"];

        constructor(
            private readonly $scope: ng.IScope,
            private readonly $http: ng.IHttpService,
            private readonly homeDataService: IAnimeDataService,
            private displayText: string, //for testing
            public animeSeason: ISeason
        ) {
            this.displayText = "TEST CTRL";
        }

        getCurrentSeasonAnime(): void {
            this.homeDataService.loadCurrentSeasonAnime()
                .then((data: ISeason | void) => {
                    this.animeSeason = data as ISeason;
                },
                    msg => console.log("ERROR:", msg))
        }

        getSeasonalAnime(year: number, season: string): void {
            this.homeDataService.loadSeasonalAnime(year, season)
                .then((data: any) => {
                    this.displayText = data;
                },
                    msg => console.log("ERROR:", msg))
        }

        getAnimeStats(): void {
            var url = "/Home/HomeApi/GetAnimeStats/1";
            this.$http.get(url)
                .then((data: any) => {
                    this.displayText = data.data;
                },
                    msg => console.log("ERROR:", msg));
        }
        //getInfoFromDb(): void {
        //    var url = "/Home/HomeApi/GetInfoFromDb";
        //    this.$http.get(url)
        //        .then((data: any) => {
        //            this.displayText = data.data;
        //        },
        //            msg => console.log("ERROR:", msg));
        //}
    }
}
