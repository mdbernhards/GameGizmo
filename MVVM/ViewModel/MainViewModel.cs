using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Enums;
using GameGizmo.Logic;
using GameGizmo.Logic.Interfaces;
using GameGizmo.MVVM.Model;

namespace GameGizmo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; }

        public IApiToViewMapper ApiToViewMapper { get; set; }

        public Main Main { get; set; } = new Main();

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
            WeakReferenceMessenger.Default.Register<Game>(this, SetGameView);
            WeakReferenceMessenger.Default.Register<Developer>(this, SetDeveloperView);

            ApiLogic = new ApiLogic();
            ApiToViewMapper = new ApiToViewMapper();

            Main.Home = new HomeViewModel(ApiLogic, ApiToViewMapper);
            Main.SearchResults = new SearchResultsViewModel(ApiLogic, ApiToViewMapper);
            Main.Game = new GameViewModel(ApiLogic, ApiToViewMapper);
            Main.Developer = new DeveloperViewModel(ApiLogic, ApiToViewMapper);

            SetUpRelayCommands();

            CurrentView = Main.Home;
        }

        public void SetGameView(object recipient, Game game)
        {
            Main.Game?.GetGameView(game.Id);
            CurrentView = Main.Game;
        }

        public void SetDeveloperView(object recipient, Developer developer)
        {
            Main.Developer?.GetDeveloperView(developer.Id);
            CurrentView = Main.Developer;
        }

        private void SetUpRelayCommands()
        {
            Main.HomeViewCommand = new RelayCommand<object>(x =>
            {
                CurrentView = Main.Home;
            });

            Main.SearchResultsViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(GameListType.Search, Main.SearchText);
                CurrentView = Main.SearchResults;
            });

            Main.TopGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(GameListType.TopGamesOfAllTime);
                CurrentView = Main.SearchResults;
            });

            Main.NewestGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(GameListType.NewestGames);
                CurrentView = Main.SearchResults;
            });

            Main.HottestGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(GameListType.HottestGames);
                CurrentView = Main.SearchResults;
            });

            Main.ListOfDevelopersViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetDeveloperList();
                CurrentView = Main.SearchResults;
            });
        }
    }
}
