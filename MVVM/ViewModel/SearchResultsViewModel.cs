using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Core;
using GameGizmo.Logic;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using GameGizmo.MVVM.View;
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

        public RelayCommand NextPageViewCommand { get; set; }

        public RelayCommand LastPageViewCommand { get; set; }

        public RelayCommand FirstPageViewCommand { get; set; }

        public RelayCommand PreviousPageViewCommand { get; set; }

        private GameListType ListType = GameListType.TopGamesOfAllTime;
        private ApiGameParameters? Parameters = new ApiGameParameters();

        private int? pageNumber = 1;
        public int? PageNumber
        {
            get => pageNumber;
            set
            {
                pageNumber = value;
                OnPropertyChanged();
            }
        }

        private int? pageSize = 16;
        public int? PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value;
                OnPropertyChanged();
            }
        }

        private int? ListCount = 1;

        public SearchResultsViewModel(ApiLogic apiLogic)
        {
            ApiLogic = apiLogic;

            NextPageViewCommand = new RelayCommand(x =>
            {
                if (GetMaxPageNumber() >= pageNumber)
                {
                    PageNumber++;
                }

                GetList();
            });

            PreviousPageViewCommand = new RelayCommand(x =>
            {
                if (pageNumber > 1)
                {
                    PageNumber--;
                }

                GetList();
            });

            LastPageViewCommand = new RelayCommand(x =>
            {
                PageNumber = GetMaxPageNumber();
                GetList();
            });

            FirstPageViewCommand = new RelayCommand(x =>
            {
                PageNumber = 1;
                GetList();
            });
        }

        private void GetList()
        {
            if (IsGameListVisible)
            {
                GetGameList(ListType, Parameters, false);
            }
            else
            {
                GetDeveloperList();
            }
        }

        private int? GetMaxPageNumber()
        {
            return (ListCount / pageSize) + 1;
        }

        public async void GetGameList(GameListType listType, ApiGameParameters? parameters = null, bool IsNewSearch = true)
        {
            IsGameListVisible = true;

            PageNumber = IsNewSearch ? 1 : PageNumber;
            ListType = listType;
            Parameters = parameters;

            ListOfGames? results = listType switch
            {
                GameListType.TopGamesOfAllTime => await ApiLogic.GetHighestRatedGames(PageNumber, PageSize),
                GameListType.NewestGames => await ApiLogic.GetNewestGames(PageNumber, PageSize),
                GameListType.HottestGames => await ApiLogic.GetHottestGames(PageNumber, PageSize),
                GameListType.SimpleSearch => await ApiLogic.GetSimpleSearch(parameters?.searchQuery, PageNumber, PageSize),
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

            ListCount = games.count;

            GameList.Clear();
            foreach (var item in games.results)
            {
                GameList.Add(item);
            }
        }

        public async void GetDeveloperList()
        {
            IsDeveloperListVisible = true;

            ListOfDevelopers? results = await ApiLogic.ListOfDevelopersQuery(PageNumber, PageSize);

            AddNewDevelopers(results);
        }

        private void AddNewDevelopers(ListOfDevelopers? developers)
        {
            if (developers == null || developers.results == null)
            {
                return;
            }

            ListCount = developers.count;

            DeveloperList.Clear();
            foreach (var item in developers.results)
            {
                DeveloperList.Add(item);
            }
        }
    }
}
