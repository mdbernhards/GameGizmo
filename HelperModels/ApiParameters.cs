namespace GameGizmo.Models
{
    public class ApiParameters
    {
        public string? id { get; set; }

        public int? pageNumber { get; set; }

        public int? pageSize { get; set; }

        public string? searchQuery { get; set; }

        public string? dates {  get; set; }

        public string? ordering { get; set; }

        public List<int?>? PlatformIds { get; set; }

        public List<int?>? StoreIds { get; set; }

        public List<int?>? GenresIds { get; set; }

        public string? MetacriticScore { get; set; }

        public ApiParameters() { }
    }
}
