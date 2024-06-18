using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui.Controls;
using Wpf.Ui.Extensions;

namespace GameGizmo.Logic
{
    public class ApiLogic
    {
        private Uri path = new($"https://api.rawg.io/api/games?dates=2019-09-01,2019-09-30&ordering=-added&page=1&page_size=5&key=a84e3588362f461a95f7e1d63069c9b5");

        private string GameBaseUri = $"https://api.rawg.io/api/games?key=a84e3588362f461a95f7e1d63069c9b5";

        private HttpClient Client;

        public ApiLogic()
        {
            Client = new();
        }

        public async Task<Game> Test()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await Client.SendAsync(requestMessage);

            var result = await response.Content.ReadAsAsync<Game>();
            return result;
        }

        public async Task<Game> GetHighestRatedGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 30,
                ordering = "-metacritic"
            };

            return await GameQuery(parameters);
        }

        public async Task<Game> GetNewestGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 30,
                ordering = "added"
            };

            return await GameQuery(parameters);
        }

        public async Task<Game> GetHottestGames()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 30,
                dates = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd") + "," + DateTime.Now.ToString("yyyy-MM-dd"),
                ordering = "-added"
            };

            return await GameQuery(parameters);
        }

        public async Task<Game> GameQuery(ApiGameParameters parameters)
        {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, CreateUri(parameters));
                var response = await Client.SendAsync(requestMessage);

                var result = await response.Content.ReadAsAsync<Game>();
                return result;
        }

        private Uri CreateUri(ApiGameParameters parameters)
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

            return new Uri((GameBaseUri + query.Normalize()));
        }
    }
}
