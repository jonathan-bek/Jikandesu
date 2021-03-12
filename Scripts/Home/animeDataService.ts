﻿namespace JdAnime {
    "use strict";
    //import here
    export class AnimeDataService implements IAnimeDataService {
        static $inject = ["$http"];

        constructor(
            private readonly $http: ng.IHttpService
        ) { }

        loadSearchResults(filterCollection: ISearchFilter[]): Promise<any | void> {
            var url = `/Home/HomeApi/LoadSearchResults`;
            return this.$http.post(url, filterCollection)
                .then((data) => {
                    return data.data as Promise<any>;
                }, msg => console.log("ERROR:", msg));
        }

        loadScrapedMangaImageUrls(): Promise<any | void> {
            var url = `/Home/HomeApi/LoadScrapedMangaImageUrls`;
            return this.$http.get(url)
                .then((data) => {
                    return data.data as Promise<any>;
                }, msg => console.log("ERROR:", msg));
        }

        loadCurrentSeasonAnime(): Promise<ISeason | void> {
            var url = `/Home/HomeApi/LoadCurrentSeasonAnime`;
            return this.$http.get(url)
                .then((data) => {
                    return data.data as Promise<ISeason>;
                }, msg => console.log("ERROR:", msg));
        }

        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void> {
            var url = `/Home/HomeApi/LoadSeasonalAnime`;
            var postObj = { year, season };
            return this.$http.post(url, postObj)
                .then((data) => {
                    return data.data as Promise<ISeason>;
                }, msg => console.log("ERROR:", msg));
        }

        testDb(): Promise<any> {
            var url = `/Home/HomeApi/GetInfoFromDb`;
            return this.$http.get(url)
                .then((data) => {
                    return data.data as Promise<any>;
                }, msg => console.log("ERROR:", msg));
        }
    }
}