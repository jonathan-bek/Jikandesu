namespace JdAnime {
    export interface IMangaPage {
        Title: string;
        ImageUrl: string;
        Id: string;
        MangaChapters: IMangaChapter[];
    }

    export interface IMangaChapter {
        ChapterName: string;
        ChapterUrl: string;
    }
}
