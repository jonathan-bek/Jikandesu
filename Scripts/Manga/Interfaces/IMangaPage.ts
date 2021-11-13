/// <reference path="IMangaChapter.ts" />
namespace Manga {
    export interface IMangaPage {
        Url: string;
        Title: string;
        ImageUrl: string;
        MangaChapters: IMangaChapter[];
    }
}
