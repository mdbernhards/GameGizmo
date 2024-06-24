namespace GameGizmo.Models
{
    public class ListOfDevelopers
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<DeveloperData>? results { get; set; }
    }
}
