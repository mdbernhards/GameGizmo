namespace GameGizmo.Models
{
    public class ListOfStores
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Store>? results { get; set; }
    }

    public class Store
    {
        public int? id { get; set; }
        public string? name { get; set; }
        public string? domain { get; set; }
        public string? slug { get; set; }
        public int? games_count { get; set; }
        public string? image_background { get; set; }
        public bool? IsSelected { get; set; } = false;
    }

}
