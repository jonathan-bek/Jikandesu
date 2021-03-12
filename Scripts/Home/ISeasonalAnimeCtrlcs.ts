namespace JdAnime {
    export interface IAnimeHomeCtrl {
        animeSeason: ISeason;
        searchText: string;
        searchAllMediaByName: () => void;
        getCurrentSeasonAnime: () => void;
        getSeasonalAnime: (year: number, season: string) => void;
        getAnimeStats: () => void;
        testDb: () => void;
    }
}
