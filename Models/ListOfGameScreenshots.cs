namespace GameGizmo.Models
{
    public class ListOfGameScreenshots
    {
        public int? count { get; set; }
        public string? next { get; set; }
        public string? previous { get; set; }
        public List<GameScreenshot>? results { get; set; }
    }
    public class GameScreenshot
    {
        public string? image { get; set; }
        public bool? hidden { get; set; }
    }
}
