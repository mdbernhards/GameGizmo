namespace GameGizmo.Models
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

    public class ListOfGames
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<GameData>? results { get; set; }
    }
}
