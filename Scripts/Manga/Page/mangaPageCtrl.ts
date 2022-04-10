/// <reference path="../Interfaces/IMangaPage.ts" />
/// <reference path="../Interfaces/IMangaDataService.ts" />

namespace Manga {
    "use strict";
    import IMangaPage = Manga.IMangaPage;
    import IMangaDataService = Manga.IMangaDataService;
    export class MangaPageCtrl implements ng.IController, IMangaPageCtrl {
        static $inject = ["$scope", "$window", "mangaDataService", "$uibModal"];
        constructor(
            private readonly $scope: ng.IScope,
            private readonly $window: ng.IWindowService,
            private readonly mangaDataService: IMangaDataService,
            private readonly $uibModal: any,
        ) { }

        mangaPage: IMangaPage | undefined;

        $onInit() {
            this.getMangaPage(this.getMangaUrl());
        }
        openModal(): void {
            var modalInstance = this.$uibModal.open({
                template:
                    '<div class="modal-header">' +
                    ' <h3 class="modal-title" id="modal-title">Modal Title</h3>' +
                    '</div>' +
                    '<div class="modal-body" id="modal-body">' +
                    'Modal Content here..!' +
                    '</div>' +
                    '<div class="modal-footer">' +
                    ' <button class="btn btn-primary" type="button" ng-click="ctrl.test()">OK</button>' +
                    '<button class="btn btn-warning" type="button" ng-click="cancel()">Cancel</button>' +
                    '</div>',
                //templateUrl: '../../Scripts/Manga/Page/testhtml'
                controller: 'mangaPageCtrl',
                controllerAs: 'ctrl',
                resolve: {
                    //tbd
                }
            });
            modalInstance.result.then(function (result: any) {
            }, function () {
            });
        }

        test(): void {
            console.log('lol')
        }

        getMangaPage(url: string): void {
            this.mangaDataService.getMangaPage(url)
                .then((data: IMangaPage | void) => {
                    this.mangaPage = data as IMangaPage;
                });
        }

        saveMangaPage(): void {
            this.mangaDataService.saveMangaPage(this.mangaPage as IMangaPage);
        }

        private getMangaUrl(): string {
            var mangaUrl = this.$window.location.search.replace("?searchUrl=", "");
            if (mangaUrl.indexOf("http") < 0) {
                mangaUrl = "https://" + mangaUrl;
            }
            return mangaUrl;
        }
    }
}
