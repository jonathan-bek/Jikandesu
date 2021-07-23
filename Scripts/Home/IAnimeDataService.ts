namespace JdAnime {
    export interface IAnimeDataService {
        getMangaPage(url: string): Promise<IMangaPage | void>;
        saveMangaPage(mangaPage: IMangaPage): any;
        loadSearchResults(filterCollection: ISearchFilter[]): Promise<any | void>;
        loadCurrentSeasonAnime(): Promise<ISeason | void>;
        loadSeasonalAnime(year: number, season: string): Promise<ISeason | void>;
        testDb(): Promise<any>;
    }
}
