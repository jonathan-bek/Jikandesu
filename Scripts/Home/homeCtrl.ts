module Home {
    "use strict";

    export interface IHomeCtrlScope extends ng.IScope {
        homeCtrl: HomeCtrl;
        testCtrlScope: string;
    }

    export class HomeCtrl implements ng.IController {
        static $inject = ["$scope", "$http"];

        constructor(
            private readonly $scope: IHomeCtrlScope,
            private readonly $http: ng.IHttpService,
            private displayText: string
        ) {
            this.displayText = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }

        getAnimeStatsFromApi(): Promise<any> {
            return this.$http.get("/Home/HomeApi/GetAnimeStats/1")
                .then(res => res
                    , msg => console.log(msg)) as Promise<any>;
        }

        getAnimeStats(): void {
            this.getAnimeStatsFromApi().then((data: any) => {
                this.displayText = data.data;
            })
        }
    }
}
