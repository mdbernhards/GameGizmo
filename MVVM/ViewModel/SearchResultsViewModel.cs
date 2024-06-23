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

        private Developer? selectedDeveloper;
        public Developer? SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set
            {
                if (!string.Equals(selectedDeveloper, value))
                {
                    selectedDeveloper = value;
                    OnPropertyChanged(nameof(selectedDeveloper));
                    WeakReferenceMessenger.Default.Send(selectedDeveloper ?? new Developer());
                }
            }
        }

        public ApiLogic ApiLogic { get; set; }

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

        public Visibility GameListVisibility
        {
            get { return IsGameListVisible ? Visibility.Visible : Visibility.Collapsed; }
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

        private int? ListCount = 1;

        public Filters Filters { get; set; } = new Filters();

        public SearchResultsViewModel(ApiLogic apiLogic)
        {
            ApiLogic = apiLogic;

            GetFilters();

            NextPageViewCommand = new RelayCommand(x =>
            {
                if (GetMaxPageNumber() >= Filters.PageNumber)
                {
                    Filters.PageNumber++;
                }

                GetList();
            });

            PreviousPageViewCommand = new RelayCommand(x =>
            {
                if (Filters.PageNumber > 1)
                {
                    Filters.PageNumber--;
                }

                GetList();
            });

            LastPageViewCommand = new RelayCommand(x =>
            {
                Filters.PageNumber = GetMaxPageNumber();
                GetList();
            });

            FirstPageViewCommand = new RelayCommand(x =>
            {
                Filters.PageNumber = 1;
                GetList();
            });
        }

        public async void GetGameList(GameListType listType, string? searchText = null, bool IsNewSearch = true)
        {
            ApiParameters parameters = CreateApiSearchParameters(searchText);

            IsGameListVisible = true;

            Filters.PageNumber = IsNewSearch ? 1 : Filters.PageNumber;
            ListType = listType;
            Filters.Parameters = parameters;

            ListOfGames? results = listType switch
            {
                GameListType.TopGamesOfAllTime => await ApiLogic.GetHighestRatedGames(Filters.PageNumber, Filters.PageSize),
                GameListType.NewestGames => await ApiLogic.GetNewestGames(Filters.PageNumber, Filters.PageSize),
                GameListType.HottestGames => await ApiLogic.GetHottestGames(Filters.PageNumber, Filters.PageSize),
                GameListType.Search => await ApiLogic.GetSimpleSearch(parameters),
                _ => await ApiLogic.Test(),
            };

            AddNewGames(results);
        }

        public async void GetDeveloperList()
        {
            IsDeveloperListVisible = true;

            ListOfDevelopers? results = await ApiLogic.ListOfDevelopersQuery(Filters.PageNumber, Filters.PageSize);

            AddNewDevelopers(results);
        }

        private ApiParameters CreateApiSearchParameters(string? searchQuerry)
        {
            ApiParameters parameters = new()
            {
                pageSize = Filters.PageSize,
                pageNumber = Filters.PageNumber,
                searchQuery = searchQuerry,
                PlatformIds = Filters.ListOfPlatforms?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
                StoreIds = Filters.ListOfStores?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
                GenresIds = Filters.ListOfGenres?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
            };


            return parameters;
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

        private int? GetMaxPageNumber()
        {
            return (ListCount / Filters.PageSize) + 1;
        }

        private void GetList()
        {
            if (IsGameListVisible)
            {
                GetGameList(ListType, Filters.Parameters?.searchQuery, false);
            }
            else
            {
                GetDeveloperList();
            }
        }

        private async void GetFilters()
        {
            var platforms = await ApiLogic.ListOfPlatformsQuery();
            var stores = await ApiLogic.ListOfStoresQuery();
            var genres = await ApiLogic.ListOfGenresQuery();

            Filters.ListOfPlatforms = platforms?.results ?? [];
            Filters.ListOfStores = stores?.results ?? [];
            Filters.ListOfGenres = genres?.results ?? [];
        }
    }
}
