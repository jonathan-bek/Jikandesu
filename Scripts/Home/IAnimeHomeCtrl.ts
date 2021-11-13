namespace JdAnime {
    export interface IAnimeHomeCtrl {
        searchText: string;
        displayText: string;
        mangaUrl: string;
        getMangaPage: () => void;
    }
}
