using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameGizmo.Enums;
using GameGizmo.Logic.Interfaces;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;

namespace GameGizmo.MVVM.ViewModel
{
    internal class SearchResultsViewModel : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; }

        public IApiToViewMapper ApiToViewMapper { get; set; }

        private Search search = new();
        public Search Search
        {
            get { return search; }
            set
            {
                search = value;
                OnPropertyChanged();
            }
        }

        private MenuType ListType = MenuType.TopGamesOfAllTime;

        private int? ListCount = 1;

        public SearchResultsViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper)
        {
            ApiLogic = apiLogic;
            ApiToViewMapper = apiToViewMapper;

            GetFilters();
            SetUpRelayCommands();
        }

        public async void GetGameList(MenuType listType, string? searchText = null, bool IsNewSearch = true)
        {
            if (IsNewSearch)
            {
                Search.Filters.PageNumber = 1;
                GetFilters();
            }

            ApiParameters parameters = CreateApiSearchParameters(searchText);

            Search.LoadingData.IsLoading = true;
            Search.IsGameListVisible = true;

            ListType = listType;
            Search.Filters.Parameters = parameters;

            ListOfGames? results = listType switch
            {
                MenuType.TopGamesOfAllTime => await ApiLogic.GetHighestRatedGames(parameters),
                MenuType.UpcomingGames => await ApiLogic.GetNewestGames(parameters),
                MenuType.HottestGames => await ApiLogic.GetHottestGames(parameters),
                MenuType.Search => await ApiLogic.GetSearch(parameters),
                _ => await ApiLogic.GetHighestRatedGames(parameters),
            };

            AddNewGames(results);
            Search.LoadingData.IsLoading = false;
        }

        public async void GetDeveloperList()
        {
            Search.LoadingData.IsLoading = true;
            Search.IsDeveloperListVisible = true;

            ListOfDevelopers? results = await ApiLogic.ListOfDevelopersQuery(Search.Filters.PageNumber, Search.Filters.PageSize);

            AddNewDevelopers(results);
            Search.LoadingData.IsLoading = false;
        }

        private void AddNewGames(ListOfGames? games)
        {
            if (games == null || games.results == null)
            {
                throw new ArgumentNullException(nameof(games) + ", " + nameof(games.results));
            }

            ListCount = games.count;
            var tempGameData = ApiToViewMapper.MapToGame(games.results);

            Search.GameList.Clear();
            foreach (var item in tempGameData)
            {
                Search.GameList.Add(item);
            }
        }

        private void AddNewDevelopers(ListOfDevelopers? developers)
        {
            if (developers == null || developers.results == null)
            {
                throw new ArgumentNullException(nameof(developers) + ", " + nameof(developers.results));
            }

            ListCount = developers.count;
            var tempDevData = ApiToViewMapper.MapToDeveloper(developers.results);

            Search.DeveloperList.Clear();
            foreach (var item in tempDevData)
            {
                Search.DeveloperList.Add(item);
            }
        }

        private ApiParameters CreateApiSearchParameters(string? searchQuerry)
        {

            ApiParameters parameters = new()
            {
                pageSize = Search.Filters.PageSize,
                pageNumber = Search.Filters.PageNumber,
                searchQuery = searchQuerry,
                PlatformIds = Search.Filters.ListOfPlatforms?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
                StoreIds = Search.Filters.ListOfStores?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
                GenresIds = Search.Filters.ListOfGenres?.Where(x => x.IsSelected == true).Select(x => x.id).ToList(),
                dates = FormatDateParameter(),
                MetacriticScore = FormatMetacriticParameter(),
                ordering = Search.Filters.SortOrder.Key,
            };

            return parameters;
        }

        private string FormatDateParameter()
        {
            if (Search.Filters.ReleaseRangeFrom == null && Search.Filters.ReleaseRangeTo == null)
            {
                throw new ArgumentNullException(nameof(Search.Filters.ReleaseRangeFrom) + ", " + nameof(Search.Filters.ReleaseRangeTo));
            }    

            string? dates;

            dates = Search.Filters.ReleaseRangeFrom != null ? Search.Filters.ReleaseRangeFrom.Value.ToString("yyyy-MM-dd") + "," : "1950-01-01,";
            dates += Search.Filters.ReleaseRangeTo != null ? Search.Filters.ReleaseRangeTo.Value.ToString("yyyy-MM-dd") : DateTime.Today.ToString("yyyy-MM-dd");

            return dates;
        }

        private string FormatMetacriticParameter()
        {
            if (Search.Filters.MetacriticScoreFrom == null && Search.Filters.MetacriticScoreTo == null)
            {
                throw new ArgumentNullException(nameof(Search.Filters.MetacriticScoreFrom) + ", " + nameof(Search.Filters.MetacriticScoreTo));
            }

            string? scoreRange;

            scoreRange = Search.Filters.MetacriticScoreFrom != null && Search.Filters.MetacriticScoreFrom != string.Empty ? Math.Clamp(Int32.Parse(Search.Filters.MetacriticScoreFrom),1,100) + "," : "0";
            scoreRange += Search.Filters.MetacriticScoreTo != null && Search.Filters.MetacriticScoreTo != string.Empty ? Math.Clamp(Int32.Parse(Search.Filters.MetacriticScoreTo), 1, 100) : "100";

            return scoreRange;
        }

        private int? GetMaxPageNumber()
        {
            return (ListCount / Search.Filters.PageSize);
        }

        private void GetListPage()
        {
            if (Search.IsGameListVisible)
            {
                GetGameList(ListType, Search.Filters.Parameters?.searchQuery, false);
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

            Search.Filters.ListOfPlatforms = platforms?.results ?? [];
            Search.Filters.ListOfStores = stores?.results ?? [];
            Search.Filters.ListOfGenres = genres?.results ?? [];
        }

        private void SetUpRelayCommands()
        {
            Search.NextPageViewCommand = new RelayCommand<object>(x =>
            {
                if (GetMaxPageNumber() >= Search.Filters.PageNumber)
                {
                    Search.Filters.PageNumber++;
                }

                GetListPage();
            });

            Search.PreviousPageViewCommand = new RelayCommand<object>(x =>
            {
                if (Search.Filters.PageNumber > 1)
                {
                    Search.Filters.PageNumber--;
                }

                GetListPage();
            });

            Search.LastPageViewCommand = new RelayCommand<object>(x =>
            {
                Search.Filters.PageNumber = GetMaxPageNumber() + 1;
                GetListPage();
            });

            Search.FirstPageViewCommand = new RelayCommand<object>(x =>
            {
                Search.Filters.PageNumber = 1;
                GetListPage();
            });

            Search.ApplyFiltersViewCommand = new RelayCommand<object>(x =>
            {
                GetListPage();
            });
        }
    }
}
