namespace GameGizmo.Models
{
    public class ListOfPlatforms
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Platform>? results { get; set; }
    }

    public class Platform
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? slug { get; set; }
        public int? games_count { get; set; }
        public string? image_background { get; set; }
        public bool? IsSelected { get; set; } = false;
    }
}
