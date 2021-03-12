namespace JdAnime {
    export interface ISearchResult {
        ID: number;
        Url: string;
        Title: string;
        ImageUrl: string;
        Type: string;
        Synopsis: string;
        Members: number
        Score: number | null;
        StartDate: string;
        EndDate: string;
        Airing: boolean//anime only
        Episodes: number //anime only
        Rated: string //anime only
        Publishing: boolean //manga only
        Chapters: number | null//manga only
        Volumes: number | null//manga only
    }
}
