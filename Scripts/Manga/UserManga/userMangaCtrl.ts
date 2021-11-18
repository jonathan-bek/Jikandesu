/// <reference path="../mangaDataService.ts" />
/// <reference path="../Interfaces/IMangaDataService.ts" />
/// <reference path="../Interfaces/IMangaPage.ts" />

namespace Manga {
    "use strict";
    import IMangaPage = Manga.IMangaPage;
    import IMangaDataService = Manga.IMangaDataService;
    export class UserMangaCtrl implements ng.IController {
        static inject = ["$scope", "$window", "mangaDataService"];
        constructor(
            private readonly $scope: ng.IScope,
            private readonly $window: ng.IWindowService,
            private readonly mangaDataService: IMangaDataService,
        ) {}

        mangaPages: IMangaPage[] = [];

        $onInit() {
            this.getUserManga();
        }

        redirectToManga(page: IMangaPage): void {
            var mangaUrl = page.Url;
            mangaUrl = mangaUrl.replace("https://", "");
            mangaUrl = mangaUrl.replace("http://", "");
            var redirect = this.$window.location.origin + "/Manga/Manga/Page?searchUrl=" + mangaUrl;
            window.location.href = redirect;
        }

        private getUserManga() {
            this.mangaDataService.getUserManga()
                .then((data) => {
                    this.mangaPages = data as IMangaPage[];
                });
        }
    }
}