using GameGizmo.Core;
using GameGizmo.Logic;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGizmo.MVVM.ViewModel
{
    internal class SearchResultsViewModel : ObservableObject
    {
        public ApiLogic ApiLogic { get; set; }

        public ObservableCollection<Model.Result> GameList { get; set; } = [];

        public SearchResultsViewModel()
        {
            ApiLogic = new ApiLogic();
        }

        public async void Test()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 30,
                ordering = "-metacritic"
            };

            var result = await ApiLogic.GameQuery(parameters);

            foreach (var item in result.results)
            {
                GameList.Add(item);
            }
        }

        public async void GetGameList(GameListType listType, ApiGameParameters? parameters = null)
        {
            Game? results = null;

            switch (listType)
            {
                case GameListType.TopGamesOfAllTime:
                    results = await ApiLogic.GetHighestRatedGames();
                    break;
                case GameListType.NewestGames:
                    results = await ApiLogic.GetNewestGames();
                    break;
                case GameListType.HottestGames:
                    results = await ApiLogic.GetHottestGames();
                    break;
                default:
                    results = await ApiLogic.Test();
                    break;
            }

            AddNewGames(results);
        }

        private void AddNewGames(Game? games)
        {
            GameList.Clear();
            foreach (var item in games.results)
            {
                GameList.Add(item);
            }
        }
    }
}
