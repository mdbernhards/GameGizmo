using GameGizmo.HelperModels;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System.Net.Http;

namespace GameGizmo.Logic
{
    public class ApiLogic
    {
        private readonly string Key = $"a84e3588362f461a95f7e1d63069c9b5";

        private Uri path = new($"https://api.rawg.io/api/games?dates=2019-09-01,2019-09-30&ordering=-added&page=1&page_size=5&key=");

        private string GameBaseUri = $"https://api.rawg.io/api/games?key=";

        private HttpClient Client;

        public ApiLogic()
        {
            GameBaseUri += Key;
            path = new Uri(path, Key);
            Client = new();
        }

        public async Task<ListOfGames?> Test()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await Client.SendAsync(requestMessage);

            var result = await response.Content.ReadAsAsync<ListOfGames?>();
            return result;
        }

        public async Task<ListOfGames?> GetHighestRatedGames(int? pageNumber, int? pageSize)
        {
            var parameters = new ApiParameters
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                ordering = "-metacritic"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetNewestGames(int? pageNumber, int? pageSize)
        {
            var parameters = new ApiParameters
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                ordering = "added"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetHottestGames(int? pageNumber, int? pageSize)
        {
            var parameters = new ApiParameters
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
                dates = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "," + DateTime.Now.ToString("yyyy-MM-dd"),
                ordering = "-added"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetSimpleSearch(ApiParameters parameters)
        {
            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> ListOfGamesQuery(ApiParameters parameters)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(CreateUri(parameters));

            if (!response.IsSuccessStatusCode)
            {
                return new ListOfGames();
            }

            return await response.Content.ReadAsAsync<ListOfGames?>();
        }

        public async Task<Game?> GameQuery(int? gameId)
        {
            string query = $"https://api.rawg.io/api/games/" + gameId + $"?key=" + Key;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return new Game();
            }

            return await response.Content.ReadAsAsync<Game?>();
        }

        public async Task<Developer?> DeveloperQuery(int? developerId)
        {
            string query = $"https://api.rawg.io/api/developers/" + developerId + $"?key=" + Key;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return new Developer();
            }

            return await response.Content.ReadAsAsync<Developer?>();
        }

        public async Task<ListOfDevelopers?> ListOfDevelopersQuery(int? pageNumber, int? pageSize)
        {
            var parameters = new ApiParameters
            {
                pageNumber = pageNumber,
                pageSize = pageSize,
            };

            string query = $"https://api.rawg.io/api/developers" + $"?key=" + Key;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query + CreateUriParameters(parameters)));

            if (!response.IsSuccessStatusCode)
            {
                return new ListOfDevelopers();
            }

            return await response.Content.ReadAsAsync<ListOfDevelopers?>();
        }

        public async Task<ListOfPlatforms?> ListOfPlatformsQuery()
        {
            string query = $"https://api.rawg.io/api/platforms" + $"?key=" + Key;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return new ListOfPlatforms();
            }

            return await response.Content.ReadAsAsync<ListOfPlatforms?>();
        }

        public async Task<ListOfStores?> ListOfStoresQuery()
        {
            string query = $"https://api.rawg.io/api/stores" + $"?key=" + Key;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return new ListOfStores();
            }

            return await response.Content.ReadAsAsync<ListOfStores?>();
        }

        public async Task<ListOfGenres?> ListOfGenresQuery()
        {
            string query = $"https://api.rawg.io/api/genres" + $"?key=" + Key;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return new ListOfGenres();
            }

            return await response.Content.ReadAsAsync<ListOfGenres?>();
        }

        private Uri CreateUri(ApiParameters parameters)
        {
            return new Uri(GameBaseUri + CreateUriParameters(parameters));
        }

        private string CreateUriParameters(ApiParameters parameters)
        {
            string query = string.Empty;

            if (parameters.pageNumber != null)
            {
                query += "&page=" + parameters.pageNumber.ToString();
            }

            if (parameters.pageSize != null)
            {
                query += "&page_size=" + parameters.pageSize.ToString();
            }

            if (parameters.searchQuery != null)
            {
                query += "&search=" + parameters.searchQuery.ToString();
            }

            if (parameters.dates != null && parameters.dates != string.Empty)
            {
                query += "&dates=" + parameters.dates.ToString();
            }

            if (parameters.ordering != null)
            {
                query += "&ordering=" + parameters.ordering.ToString();
            }
            else
            {
                query += "&ordering=-added";
            }

            if (parameters.PlatformIds != null && parameters.PlatformIds.Count > 0)
            {
                query += "&platforms=" + GetParametersFromList(parameters.PlatformIds);
            }

            if (parameters.StoreIds != null && parameters.StoreIds.Count > 0)
            {
                query += "&stores=" + GetParametersFromList(parameters.StoreIds);
            }

            if (parameters.GenresIds != null && parameters.GenresIds.Count > 0)
            {
                query += "&genres=" + GetParametersFromList(parameters.GenresIds);
            }

            if (parameters.MetacriticScore != null && parameters.MetacriticScore != string.Empty)
            {
                query += "&metacritic=" + parameters.MetacriticScore.ToString();
            }

            return query.Normalize();
        }

        private static string? GetParametersFromList(List<int?> parameters)
        {
            string? ids = null;

            foreach (var item in parameters)
            {
                ids = ids == null ? ids : ids += ",";
                ids += item;
            }

            return ids;
        }
    }
}
