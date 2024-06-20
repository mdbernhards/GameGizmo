namespace GameGizmo.MVVM.Model
{
    public class ListOfDevelopers
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<Developer>? results { get; set; }
    }
}
