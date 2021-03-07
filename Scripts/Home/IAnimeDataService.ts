namespace JdAnime {
    export interface IAnimeDataService {
        loadScrapedMangaImageUrls(): Promise<any | void>;
        loadCurrentSeasonAnime(): Promise<ISeason | void>;
        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void>;
        testDb(): Promise<any>;
    }
}
