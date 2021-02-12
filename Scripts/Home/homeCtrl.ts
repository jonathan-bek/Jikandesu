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
