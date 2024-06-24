using GameGizmo.Models;

namespace GameGizmo.MVVM.Model
{
    internal class Game
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? Metacritic { get; set; }

        public string? Released { get; set; }

        public string? BackgroundImage { get; set; } = "http://example.com";

        public int ScreenshotCount { get; set; } = 0;

        public string? Website { get; set; } = "http://example.com";

        public float? Rating { get; set; }

        public string? Platforms { get; set; }

        public List<GameScreenshot>? GameScreenshots { get; set; }

        public LoadingData LoadingData { get; set; } = new LoadingData();

        /*
        private List<GameScreenshot>? gameScreenshots;
        public List<GameScreenshot>? GameScreenshots
        {
            get { return gameScreenshots; }
            set
            {
                if (!Equals(gameScreenshots, value))
                {
                    gameScreenshots = value;

                    OnPropertyChanged();
                }
            }
        }*/
    }
}
