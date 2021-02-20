/// <reference path="animeDataService.ts" />

module Home {
    "use strict";

    export interface ISeasonalAnimeCtrl extends ng.IScope {
        seasonalAnimeCtrl: SeasonalAnimeCtrl;
        testCtrlScope: string;
    }

    export class SeasonalAnimeCtrl implements ng.IController {
        static $inject = ["$scope", "$http", "animeDataService"];

        constructor(
            private readonly $scope: ISeasonalAnimeCtrl,
            private readonly $http: ng.IHttpService,
            private readonly homeDataService: IAnimeDataService,
            private displayText: string
        ) {
            this.displayText = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }

        getCurrentSeasonAnime(): void {
            this.homeDataService.loadCurrentSeasonAnime()
                .then((data: any) => {
                    this.displayText = data;
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
        getInfoFromDb(): void {
            var url = "/Home/HomeApi/GetInfoFromDb";
            this.$http.get(url)
                .then((data: any) => {
                    this.displayText = data.data;
                },
                    msg => console.log("ERROR:", msg));
        }
    }
}
