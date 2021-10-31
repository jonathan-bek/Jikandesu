namespace JdAnime {
    export interface IAnimeDataService {
        getMangaPage(url: string): Promise<IMangaPage | void>;
        saveMangaPage(mangaPage: IMangaPage): any;
        testDb(): Promise<any>;
    }
}
