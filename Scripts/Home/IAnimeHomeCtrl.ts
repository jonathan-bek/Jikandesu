namespace JdAnime {
    export interface IAnimeHomeCtrl {
        animePageNumber: number,
        pagedAnimeHeader: ISearchResult[],
        animeHeader: ISearchResult[],
        mangaHeader: ISearchResult[],
        animeSeason: ISeason;
        searchText: string;
        displayText: string;
        mangaUrl: string;
        searchAllMediaByName: () => void;
        getCurrentSeasonAnime: () => void;
        getSeasonalAnime: (year: number, season: string) => void;
        getAnimeStats: () => void;
        testDb: () => void;
        getMangaPage: () => void;
    }
}
