namespace JdAnime {
    export interface IMangaPage {
        Url: string;
        Title: string;
        ImageUrl: string;
        MangaChapters: IMangaChapter[];
    }

    export interface IMangaChapter {
        ChapterName: string;
        ChapterUrl: string;
        ChapterUploadDate: Date;
        ChapterViews: number;
        ChapterUploadedDateDifference: any; //timespan
        ChapterUploadDateString: string;
    }
}
