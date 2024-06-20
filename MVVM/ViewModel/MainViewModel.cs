using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Core;
using GameGizmo.Logic;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System.Windows;
using System.Windows.Input;

namespace GameGizmo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public ApiLogic ApiLogic { get; set; }

        public HomeViewModel? Home { get; set; }

        public SearchResultsViewModel SearchResults { get; set; }

        public GameViewModel Game { get; set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand SearchResultsViewCommand { get; set; }

        public RelayCommand TopGamesViewCommand { get; set; }

        public RelayCommand NewestGamesViewCommand { get; set; }

        public RelayCommand HottestGamesViewCommand { get; set; }

        public RelayCommand ListOfDevelopersViewCommand { get; set; }

        private string searchText = string.Empty;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                if (!string.Equals(searchText, value))
                {
                    searchText = value;
                    OnPropertyChanged();
                }
            }
        }

        private object? _currentView;

        public object? CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            WeakReferenceMessenger.Default.Register<Result>(this, SetGameView);
            ApiLogic = new ApiLogic();

            Home = new HomeViewModel();
            SearchResults = new SearchResultsViewModel(ApiLogic);
            Game = new GameViewModel(ApiLogic);

            CurrentView = Home;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = Home;
            });

            SearchResultsViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
                SearchResults.GetGameList(GameListType.SimpleSearch, new ApiGameParameters { searchQuery = SearchText});
            });

            TopGamesViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
                SearchResults.GetGameList(GameListType.TopGamesOfAllTime);
            });

            NewestGamesViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
                SearchResults.GetGameList(GameListType.NewestGames);
            });

            HottestGamesViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
                SearchResults.GetGameList(GameListType.HottestGames);
            });

            ListOfDevelopersViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
                SearchResults.GetDeveloperList();
            });
        }

        public void SetGameView(object recipient, Result game)
        {
            CurrentView = Game;
            Game.GetGameView(game);
        }
    }
}
