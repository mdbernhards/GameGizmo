namespace GameGizmo.MVVM.Model
{
    public class EsrbRating
    {
        public int? id { get; set; }
        public string? slug { get; set; }
        public string? name { get; set; }
    }

    public class GamePlatforms
    {
        public Platform? platform { get; set; }
        public string? released_at { get; set; }
        public Requirements? requirements { get; set; }
    }

    public class Requirements
    {
        public string? minimum { get; set; }
        public string? recommended { get; set; }
    }

    public class Result
    {
        public int? id { get; set; }
        public string? slug { get; set; }
        public string? name { get; set; }
        public string? released { get; set; }
        public bool? tba { get; set; }
        public string? background_image { get; set; }
        public float? rating { get; set; }
        public float? rating_top { get; set; }
        public int? ratings_count { get; set; }
        public string? reviews_text_count { get; set; }
        public int? added { get; set; }
        public float? metacritic { get; set; }
        public float? playtime { get; set; }
        public int? suggestions_count { get; set; }
        public DateTime? updated { get; set; }
        public EsrbRating? esrb_rating { get; set; }
        public List<GamePlatforms>? platforms { get; set; }
    }

    public class ListOfGames
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Result>? results { get; set; }
    }
}
