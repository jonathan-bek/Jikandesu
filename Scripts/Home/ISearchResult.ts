namespace JdAnime {
    export interface ISearchResult {
        mal_id: number;
        url: string;
        title: string;
        image_url: string;
        type: string;
        synopsis: string;
        members: number
        score: number | null;
        start_date: string;
        end_date: string;
        airing: boolean//anime only
        episodes: number //anime only
        rated: string //anime only
        publishing: boolean //manga only
        chapters: number | null//manga only
        volumes: number | null//manga only
    }
}
