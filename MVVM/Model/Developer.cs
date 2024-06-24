using GameGizmo.Models;

namespace GameGizmo.MVVM.Model
{
    internal class Developer
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? GamesCount { get; set; }

        public string? ImageBackground { get; set; }

        public string? Description { get; set; }

        public List<GameData>? Games { get; set; }

        public int? GameListCount { get; set; }

        public LoadingData LoadingData { get; set; } = new LoadingData();
    }
}
