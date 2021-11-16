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
                }, msg => alert("Error: You must be signed in to use this feature."));
        }

        getMangaPage(mangaUrl: string): Promise<IMangaPage | void> {
            var url = "/Manga/MangaApi/GetMangaPage";
            var postObj = { mangaUrl: mangaUrl };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    return res.data as Promise<IMangaPage>;
                }, msg => console.log("ERROR:", msg));
        }

        saveMangaPage(mangaPage: IMangaPage): Promise<void> {
            var url =  this.$window.location.origin + "/Manga/MangaApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    alert(res.data);
                }, msg => alert("Error: You must be signed in to use this feature."));
        }
    }
}