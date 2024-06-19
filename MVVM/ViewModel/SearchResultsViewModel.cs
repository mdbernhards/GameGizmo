using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Core;
using GameGizmo.Logic;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace GameGizmo.MVVM.ViewModel
{
    internal class SearchResultsViewModel : ObservableObject
    {
        public ApiLogic ApiLogic { get; set; }

        public ObservableCollection<Model.Result> GameList { get; set; } = [];

        public SearchResultsViewModel(ApiLogic apiLogic)
        {
            ApiLogic = apiLogic;
        }

        public async void Test()
        {
            var parameters = new ApiGameParameters
            {
                pageNumber = 1,
                pageSize = 30,
                ordering = "-metacritic"
            };

            var result = await ApiLogic.ListOfGamesQuery(parameters);

            foreach (var item in result.results)
            {
                GameList.Add(item);
            }
        }

        private Result selectedGame;

        public Result SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (!string.Equals(selectedGame, value))
                {
                    selectedGame = value;
                    OnPropertyChanged(nameof(selectedGame));
                    WeakReferenceMessenger.Default.Send(selectedGame);
                }
            }
        }

        public async void GetGameList(GameListType listType, ApiGameParameters? parameters = null)
        {
            ListOfGames? results = listType switch
            {
                GameListType.TopGamesOfAllTime => await ApiLogic.GetHighestRatedGames(),
                GameListType.NewestGames => await ApiLogic.GetNewestGames(),
                GameListType.HottestGames => await ApiLogic.GetHottestGames(),
                GameListType.SimpleSearch => await ApiLogic.GetSimpleSearch(parameters?.searchQuery),
                _ => await ApiLogic.Test(),
            };

            AddNewGames(results);
        }

        private void AddNewGames(ListOfGames? games)
        {
            if (games == null)
            {
                return;
            }    

            GameList.Clear();
            foreach (var item in games?.results)
            {
                GameList.Add(item);
            }
        }
    }
}
