using GameGizmo.Models;

namespace GameGizmo.Logic.Interfaces
{
    interface IApiLogic
    {
        Task<ListOfGames?> GetHighestRatedGames(ApiParameters parameters);

        Task<ListOfGames?> GetNewestGames(ApiParameters parameters);

        Task<ListOfGames?> GetHottestGames(ApiParameters parameters);

        Task<ListOfGames?> GetSearch(ApiParameters parameters);

        Task<GameData?> GameQuery(int? gameId);

        Task<ListOfGameScreenshots?> GameScreenshotQuery(int? gameId);

        Task<DeveloperData?> DeveloperQuery(int? developerId);

        Task<ListOfDevelopers?> ListOfDevelopersQuery(int? pageNumber, int? pageSize);

        Task<ListOfPlatforms?> ListOfPlatformsQuery();

        Task<ListOfStores?> ListOfStoresQuery();

        Task<ListOfGenres?> ListOfGenresQuery();
    }
}
