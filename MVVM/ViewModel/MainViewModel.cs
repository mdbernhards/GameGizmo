using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Core;
using GameGizmo.Enums;
using GameGizmo.Logic;
using GameGizmo.Logic.Interfaces;
using GameGizmo.MVVM.Model;

namespace GameGizmo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public DeveloperViewModel Developer { get; set; }

        public GameViewModel Game { get; set; }

        public HomeViewModel? Home { get; set; }

        public SearchResultsViewModel SearchResults { get; set; }

        public IApiLogic ApiLogic { get; set; }

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
            WeakReferenceMessenger.Default.Register<Developer>(this, SetDeveloperView);
            ApiLogic = new ApiLogic();

            Home = new HomeViewModel();
            SearchResults = new SearchResultsViewModel(ApiLogic);
            Game = new GameViewModel(ApiLogic);
            Developer = new DeveloperViewModel(ApiLogic);

            CurrentView = Home;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = Home;
            });

            SearchResultsViewCommand = new RelayCommand(x =>
            {
                SearchResults.GetGameList(GameListType.Search, SearchText);
                CurrentView = SearchResults;
            });

            TopGamesViewCommand = new RelayCommand(x =>
            {
                SearchResults.GetGameList(GameListType.TopGamesOfAllTime);
                CurrentView = SearchResults;
            });

            NewestGamesViewCommand = new RelayCommand(x =>
            {
                SearchResults.GetGameList(GameListType.NewestGames);
                CurrentView = SearchResults;
            });

            HottestGamesViewCommand = new RelayCommand(x =>
            {
                SearchResults.GetGameList(GameListType.HottestGames);
                CurrentView = SearchResults;
            });

            ListOfDevelopersViewCommand = new RelayCommand(x =>
            {
                SearchResults.GetDeveloperList();
                CurrentView = SearchResults;
            });
        }

        public void SetGameView(object recipient, Result game)
        {
            Game.GetGameView(game.id);
            CurrentView = Game;
        }

        public void SetDeveloperView(object recipient, Developer developer)
        {
            Developer.GetDeveloperView(developer.id);
            CurrentView = Developer;
        }
    }
}
