namespace GameGizmo.Models
{
    public class ApiGameParameters
    {
        public string? id { get; set; }

        public int? pageNumber { get; set; }

        public int? pageSize { get; set; }

        public string? searchQuery { get; set; }

        public string? dates {  get; set; }

        public string? ordering { get; set; }

        public ApiGameParameters() { }
    }
}
