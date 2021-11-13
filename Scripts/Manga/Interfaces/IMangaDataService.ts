namespace Manga {
    export interface IMangaDataService {
        getMangaPage(url: string): Promise<IMangaPage | void>;
        saveMangaPage(mangaPage: IMangaPage): any;
    }
}