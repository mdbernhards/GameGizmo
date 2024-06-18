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

namespace GameGizmo.Logic
{
    public class ApiLogic
    {
        private Uri path = new($"https://api.rawg.io/api/games?dates=2019-09-01,2019-09-30&ordering=-added&page=1&page_size=5");

        private HttpClient Client;

        public ApiLogic()
        {
            Client = new();
        }

        public async Task<Game> Search()
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, path);
            var response = await Client.SendAsync(requestMessage);

            var result = await response.Content.ReadAsAsync<Game>();
            return result;

        }
    }
}
