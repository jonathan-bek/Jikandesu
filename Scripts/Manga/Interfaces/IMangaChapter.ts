namespace Manga {
    export interface IMangaChapter {
        ChapterName: string;
        ChapterUrl: string;
        ChapterUploadDate: Date;
        ChapterViews: number;
        ChapterUploadedDateDifference: any; //timespan
        ChapterUploadDateString: string;
    }
}