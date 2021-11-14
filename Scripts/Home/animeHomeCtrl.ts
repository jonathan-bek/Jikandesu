/// <reference path="../Manga/Interfaces/IMangaPage.ts" />
/// <reference path="../Manga/Interfaces/IMangaDataService.ts" />

namespace JdAnime {
    "use strict";
    import IMangaPage = Manga.IMangaPage;
    import IMangaDataService = Manga.IMangaDataService;
    export class AnimeHomeCtrl implements ng.IController, IAnimeHomeCtrl {
        static $inject = ["$scope", "mangaDataService"];
        constructor(
            private readonly $scope: ng.IScope,
            private readonly mangaDataService: IMangaDataService,
        ) {
            this.mangaUrl = "";
        }

        mangaUrl: string
        mangaPage: IMangaPage | undefined;

        getMangaPage(): void {
            this.mangaDataService.getMangaPage(this.mangaUrl)
                .then((data: IMangaPage | void) => {
                    this.mangaPage = data as IMangaPage;
                });
        }

        saveMangaPage(): void {
            this.mangaDataService.saveMangaPage(this.mangaPage as IMangaPage);
        }
    }
}
