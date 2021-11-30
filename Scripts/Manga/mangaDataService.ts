namespace Manga {
    "use strict";
    export class MangaDataService implements IMangaDataService {
        static $inject = ["$http", "$window"];
        constructor(
            private readonly $http: ng.IHttpService,
            private readonly $window: ng.IWindowService
        ) { }

        getUserManga(): Promise<IMangaPage[] | void> {
            var url = "/Manga/MangaApi/GetUserManga";
            return this.$http.get(url)
                .then((res: any) => {
                    return res.data as Promise<IMangaPage[]>;
                }, msg => {
                    this.handleError(msg)
                });
        }

        getMangaPage(mangaUrl: string): Promise<IMangaPage | void> {
            var url = "/Manga/MangaApi/GetMangaPage";
            var postObj = { mangaUrl: mangaUrl };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    return res.data as Promise<IMangaPage>;
                }, msg => {
                    this.handleError(msg)
                });
        }

        saveMangaPage(mangaPage: IMangaPage): Promise<void> {
            var url = "/Manga/MangaApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    alert(res.data);
                }, msg => {
                    this.handleError(msg)
                });
        }

        private handleError(res: any): void {
            var msg = "";
            if (res.status == -1) {
                msg = "Network error: You must be signed in to use this feature.";
            } else {
                var errorMsgs = res.data as string[];
                msg = errorMsgs.join();
            }
            alert(msg);
        }
    }
}