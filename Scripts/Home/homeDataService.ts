module Home {
    "use strict";
    //import here
    export interface ISeason {
        season_name: string,
        season_year: number,
        anime: IAnime[]
    }
    export interface IAnime {
        mal_id: number,
        url: string,
        title: string,
        image_url: string,
        synopsis: string,
        type: string,
        airing_start: Date | null,
        episodes: number | null,
        members: number,
        Genres: IGenre[],
        source: string,
        score: number | null,
        continuing: boolean
    }
    export interface IGenre {
        mal_id: number,
        name: string,
        url: string,
        type: string,
    }
    export interface IHomeDataService {
        loadCurrentSeasonAnime(): Promise<ISeason | void>;
        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void>;
    }

    export class HomeDataService implements IHomeDataService {
        static $inject = ["$http"];

        constructor(
            private readonly $http: ng.IHttpService
        ) { }

        loadCurrentSeasonAnime(): Promise<ISeason | void> {
            var url = `/Home/HomeApi/LoadCurrentSeasonAnime`;
            return this.$http.get(url)
                .then((data) => {
                    return data.data as Promise<ISeason>;
                },
                    msg => console.log("ERROR:", msg));
        }

        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void> {
            var url = `/Home/HomeApi/LoadSeasonalAnime`;
            var postObj = { year, season };
            return this.$http.post(url, postObj)
                .then((data) => {
                    return data.data as Promise<ISeason>;
                },
                    msg => console.log("ERROR:", msg));
        }
    }
}
