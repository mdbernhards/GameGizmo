using GameGizmo.Logic.Interfaces;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;

namespace GameGizmo.Logic
{
    class ApiToViewMapper : IApiToViewMapper
    {
        public Game MapToGame(GameData gameData, List<GameScreenshot> screenshots)
        {
            var game = new Game()
            {
                Description = gameData.description,
                Name = gameData.name,
                Id = gameData.id,
                BackgroundImage = gameData.background_image,
                Metacritic = gameData.metacritic,
                Platforms = SetPlatforms(gameData.platforms),
                Rating = gameData.rating,
                Website = gameData.website,
                Released = gameData.released,
                ScreenshotCount = screenshots.Count,
                GameScreenshots = screenshots,
            };

            return game;
        }

        public List<Game> MapToGame(List<GameData> gameData)
        {
            List<Game> gameList = [];
            gameList.AddRange(gameData.Select(item => MapToGame(item, [])));

            return gameList;
        }

        public Developer MapToDeveloper(DeveloperData developerData, ListOfGames games)
        {
            var developer = new Developer()
            {
                Description = developerData.description,
                GamesCount = developerData.games_count,
                Name = developerData.name,
                Id = developerData.id,
                ImageBackground = developerData.image_background,
                Games = games.results,
                GameListCount = games.count,
            };

            return developer;
        }

        public List<Developer> MapToDeveloper(List<DeveloperData> developerData)
        {
            List<Developer> developerList = [];
            developerList.AddRange(developerData.Select(item => MapToDeveloper(item, new())));

            return developerList;
        }

        private string SetPlatforms(List<GamePlatforms>? platforms)
        {
            if (platforms == null)
            {
                return string.Empty;
            }

            var platformString = string.Empty;
            foreach (GamePlatforms platform in platforms)
            {
                platformString = platformString != string.Empty ? platformString + ", " : platformString;
                platformString += platform.platform?.name;
            }

            return platformString;
        }
    }
}
