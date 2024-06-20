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

        public async Task<ListOfGames?> GetHighestRatedGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 15,
                ordering = "-metacritic"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetNewestGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 15,
                ordering = "added"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetHottestGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 15,
                dates = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "," + DateTime.Now.ToString("yyyy-MM-dd"),
                ordering = "-added"
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> GetSimpleSearch(string? searchQuerry)
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 15,
                ordering = "-added",
                searchQuery = searchQuerry
            };

            return await ListOfGamesQuery(parameters);
        }

        public async Task<ListOfGames?> ListOfGamesQuery(ApiGameParameters parameters)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(CreateUri(parameters));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsAsync<ListOfGames?>();
            return result;
        }

        public async Task<Game?> GameQuery(int? gameId)
        {
            string query = $"https://api.rawg.io/api/games/" + gameId + $"?key=" + Key;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsAsync<Game?>();
            return result;
        }

        public async Task<Developer?> DeveloperQuery(int? developerId)
        {
            string query = $"https://api.rawg.io/api/developers/" + developerId + $"?key=" + Key;
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsAsync<Developer?>();
            return result;
        }

        public async Task<ListOfDevelopers?> ListOfDevelopersQuery()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 15,
            };

            string query = $"https://api.rawg.io/api/developers" + $"?key=" + Key;

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(new Uri(query + CreateUriParameters(parameters)));

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var result = await response.Content.ReadAsAsync<ListOfDevelopers?>();
            return result;
        }

        private Uri CreateUri(ApiGameParameters parameters)
        {
            return new Uri(GameBaseUri + CreateUriParameters(parameters));
        }

        private string CreateUriParameters(ApiGameParameters parameters)
        {
            string query = string.Empty;

            if (parameters.pageNumber != null)
            {
                query += ("&page=" + parameters.pageNumber.ToString());
            }

            if (parameters.pageSize != null)
            {
                query += ("&page_size=" + parameters.pageSize.ToString());
            }

            if (parameters.searchQuery != null)
            {
                query += ("&search=" + parameters.searchQuery.ToString());
            }

            if (parameters.dates != null)
            {
                query += ("&dates=" + parameters.dates.ToString());
            }

            if (parameters.ordering != null)
            {
                query += ("&ordering=" + parameters.ordering.ToString());
            }

            return query.Normalize();
        }
    }
}
