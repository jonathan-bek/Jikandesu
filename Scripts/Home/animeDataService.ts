namespace JdAnime {
    "use strict";
    //import here
    export class AnimeDataService implements IAnimeDataService {
        static $inject = ["$http"];

        constructor(
            private readonly $http: ng.IHttpService
        ) { }

        getMangaPage(mangaUrl: string): Promise<IMangaPage | void> {
            var url = "/Home/HomeApi/GetMangaPage";
            var postObj = { mangaUrl: mangaUrl };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    return res.data as Promise<IMangaPage>;
                }, msg => console.log("ERROR:", msg));
        }

        saveMangaPage(mangaPage: IMangaPage): any {
            var url = "/Home/HomeApi/SaveMangaPage";
            var postObj = { mangaPageStr: JSON.stringify(mangaPage) };
            return this.$http.post(url, postObj)
                .then((res: any) => {
                    alert(res.data);
                }, msg => alert("Error: You must be signed in to use this feature."));
        }

        testDb(): Promise<any> {
            var url = `/Home/HomeApi/GetInfoFromDb`;
            return this.$http.get(url)
                .then((res) => {
                    return res.data as Promise<any>;
                }, msg => console.log("ERROR:", msg));
        }
    }
}
