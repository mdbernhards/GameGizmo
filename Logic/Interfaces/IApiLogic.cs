using GameGizmo.HelperModels;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;

namespace GameGizmo.Logic.Interfaces
{
    interface IApiLogic
    {
        Task<ListOfGames?> GetHighestRatedGames(ApiParameters parameters);

        Task<ListOfGames?> GetNewestGames(ApiParameters parameters);

        Task<ListOfGames?> GetHottestGames(ApiParameters parameters);

        Task<ListOfGames?> GetSearch(ApiParameters parameters);

        Task<Game?> GameQuery(int? gameId);

        Task<Developer?> DeveloperQuery(int? developerId);

        Task<ListOfDevelopers?> ListOfDevelopersQuery(int? pageNumber, int? pageSize);

        Task<ListOfPlatforms?> ListOfPlatformsQuery();

        Task<ListOfStores?> ListOfStoresQuery();

        Task<ListOfGenres?> ListOfGenresQuery();
    }
}
