namespace JdAnime {
    export interface IAnimeDataService {
        loadCurrentSeasonAnime(): Promise<ISeason | void>;
        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void>;
    }
}
