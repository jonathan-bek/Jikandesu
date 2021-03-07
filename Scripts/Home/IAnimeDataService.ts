namespace JdAnime {
    export interface IAnimeDataService {
        loadSearchResults(filterCollection: ISearchFilter[]): Promise<any | void>;
        loadScrapedMangaImageUrls(): Promise<any | void>;
        loadCurrentSeasonAnime(): Promise<ISeason | void>;
        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void>;
        testDb(): Promise<any>;
    }
}
