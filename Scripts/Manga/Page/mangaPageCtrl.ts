/// <reference path="../Interfaces/IMangaPage.ts" />
/// <reference path="../Interfaces/IMangaDataService.ts" />

namespace Manga {
    "use strict";
    import IMangaPage = Manga.IMangaPage;
    import IMangaDataService = Manga.IMangaDataService;
    export class MangaPageCtrl implements ng.IController, IMangaPageCtrl {
        static $inject = ["$scope", "$window", "mangaDataService"];
        constructor(
            private readonly $scope: ng.IScope,
            private readonly $window: ng.IWindowService,
            private readonly mangaDataService: IMangaDataService,
        ) { }

        mangaPage: IMangaPage | undefined;

        $onInit() {
            this.getMangaPage(this.getMangaUrl());
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
