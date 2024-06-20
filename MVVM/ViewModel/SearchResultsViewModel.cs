using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Core;
using GameGizmo.Logic;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace GameGizmo.MVVM.ViewModel
{
    internal class SearchResultsViewModel : ObservableObject
    {
        public ApiLogic ApiLogic { get; set; }

        public ObservableCollection<Result> GameList { get; set; } = [];

        public ObservableCollection<Developer> DeveloperList { get; set; } = [];

        private Result? selectedGame;

        public Result? SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (!string.Equals(selectedGame, value))
                {
                    selectedGame = value;
                    OnPropertyChanged(nameof(selectedGame));
                    WeakReferenceMessenger.Default.Send(selectedGame ?? new Result());
                }
            }
        }

        private bool isGameListVisible = false;

        public bool IsGameListVisible
        {
            get => isGameListVisible;
            set
            {
                isGameListVisible = value;
                OnPropertyChanged(nameof(DeveloperListVisibility));
                OnPropertyChanged(nameof(GameListVisibility));
            }
        }

        public Visibility GameListVisibility
        {
            get { return IsGameListVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public bool IsDeveloperListVisible
        {
            get => !isGameListVisible;
            set
            {
                isGameListVisible = !value;
                OnPropertyChanged(nameof(DeveloperListVisibility));
                OnPropertyChanged(nameof(GameListVisibility));
            }
        }

        public Visibility DeveloperListVisibility
        {
            get { return IsDeveloperListVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public SearchResultsViewModel(ApiLogic apiLogic)
        {
            ApiLogic = apiLogic;
        }

        public async void GetGameList(GameListType listType, ApiGameParameters? parameters = null)
        {
            IsGameListVisible = true;

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
            if (games == null || games.results == null)
            {
                return;
            }

            GameList.Clear();
            foreach (var item in games.results)
            {
                GameList.Add(item);
            }
        }

        public async void GetDeveloperList()
        {
            IsDeveloperListVisible = true;

            ListOfDevelopers? results = await ApiLogic.ListOfDevelopersQuery();

            AddNewDevelopers(results);
        }

        private void AddNewDevelopers(ListOfDevelopers? developers)
        {
            if (developers == null || developers.results == null)
            {
                return;
            }

            DeveloperList.Clear();
            foreach (var item in developers.results)
            {
                DeveloperList.Add(item);
            }
        }
    }
}
