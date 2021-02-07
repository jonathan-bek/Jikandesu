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
            private testCtrl: string
        ) {
            this.testCtrl = "TEST CTRL";
            this.$scope.testCtrlScope = "TEST CTRL SCOPE";
        }


        getInt(){
            return 1;
        }
    }
}