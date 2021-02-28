namespace JdAnime {
    "use strict";
    //import here
    export class AnimeDataService implements IAnimeDataService {
        static $inject = ["$http"];

        constructor(
            private readonly $http: ng.IHttpService
        ) { }

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
    }
}
