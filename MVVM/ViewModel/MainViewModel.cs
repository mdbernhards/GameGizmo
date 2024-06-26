using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Enums;
using GameGizmo.Logic;
using GameGizmo.Logic.Interfaces;
using GameGizmo.MVVM.Model;
using System.Windows.Controls;

namespace GameGizmo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; }

        public IApiToViewMapper ApiToViewMapper { get; set; }

        public Main Main { get; set; } = new();

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
            WeakReferenceMessenger.Default.Register<Grid>(this, SetUpClickablePanels);

            ApiLogic = new ApiLogic();
            ApiToViewMapper = new ApiToViewMapper();

            Main.Home = new HomeViewModel(ApiLogic, ApiToViewMapper);
            Main.SearchResults = new SearchResultsViewModel(ApiLogic, ApiToViewMapper);
            Main.Game = new GameViewModel(ApiLogic, ApiToViewMapper);
            Main.Developer = new DeveloperViewModel(ApiLogic, ApiToViewMapper);

            SetUpRelayCommands();

            CurrentView = Main.Home;
        }

        private void SetGameView(object recipient, Game game)
        {
            SetGameView(game.Id);
        }

        private void SetGameView(int? id)
        {
            Main.Game?.GetGameView(id);
            CurrentView = Main.Game;
        }

        private void SetDeveloperView(object recipient, Developer developer)
        {
            Main.Developer?.GetDeveloperView(developer.Id);
            CurrentView = Main.Developer;
        }

        private void SetUpClickablePanels(object recipient, Grid grid)
        {
            if (grid.Tag is int id)
            {
                SetGameView(id);
            }
            else
            {
                if ((string)grid.Tag == "TopGames")
                {
                    Main.TopGamesViewCommand?.Execute(null);
                }
                else if ((string)grid.Tag == "UpcomingGames")
                {
                    Main.NewestGamesViewCommand?.Execute(null);
                }
                else
                {
                    throw new ArgumentException(nameof(grid.Tag));
                }
            }
        }

        private void SetUpRelayCommands()
        {
            Main.HomeViewCommand = new RelayCommand<object>(x =>
            {
                CurrentView = Main.Home;
                Main.Home?.SetUpPopularGameButtons();
            });

            Main.SearchResultsViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(MenuType.Search, Main.SearchText);
                CurrentView = Main.SearchResults;
            });

            Main.TopGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(MenuType.TopGamesOfAllTime);
                CurrentView = Main.SearchResults;
            });

            Main.NewestGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(MenuType.UpcomingGames);
                CurrentView = Main.SearchResults;
            });

            Main.HottestGamesViewCommand = new RelayCommand<object>(x =>
            {
                Main.SearchResults?.GetGameList(MenuType.HottestGames);
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
