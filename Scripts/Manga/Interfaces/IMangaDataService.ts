namespace Manga {
    export interface IMangaDataService {
        getUserManga(): Promise<IMangaPage[] | void>;
        getMangaPage(url: string): Promise<IMangaPage | void>;
        saveMangaPage(mangaPage: IMangaPage): Promise<void>;
    }
}