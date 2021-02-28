namespace JdAnime {
    export interface IAnime {
        mal_id: number,
        url: string,
        title: string,
        image_url: string,
        synopsis: string,
        type: string,
        airing_start: Date | null,
        episodes: number | null,
        members: number,
        Genres: IGenre[],
        source: string,
        score: number | null,
        continuing: boolean
    }
}
