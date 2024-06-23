namespace GameGizmo.HelperModels
{
    public class ListOfGenres
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Genres>? results { get; set; }
    }

    public class Genres
    {
        public int?  id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public int? games_count { get; set; }
        public string? image_background { get; set; }
        public bool? IsSelected { get; set; } = false;
    }
}
