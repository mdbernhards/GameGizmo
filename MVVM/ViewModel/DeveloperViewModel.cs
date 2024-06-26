using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameGizmo.Logic.Interfaces;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;
using System.Text.RegularExpressions;

namespace GameGizmo.MVVM.ViewModel
{
    internal class DeveloperViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper) : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; } = apiLogic;

        public IApiToViewMapper ApiToViewMapper { get; set; } = apiToViewMapper;

        private Developer developer = new();
        public Developer Developer
        {
            get { return developer; }
            set
            {
                if (!string.Equals(developer, value))
                {
                    developer = value;

                    if (developer != null)
                    {
                        if (developer.Description == null || developer.Description == string.Empty)
                        {
                            developer.Description = "No description found!";
                        }
                        else
                        {
                            developer.Description = Regex.Replace(developer.Description, @"<[^>]*>", String.Empty);
                        }


                        OnPropertyChanged();
                    }
                }
            }
        }

        public async void GetDeveloperView(int? developerId)
        {
            if (developerId == null || Developer == null)
            {
                throw new ArgumentNullException(nameof(developerId) + ", " + nameof(Developer));
            }

            Developer.LoadingData.IsLoading = true;
            Developer.PageNumber = 1;

            var tempDevData = await ApiLogic.DeveloperQuery(developerId);
            var tempGameData = await GetGameData(developerId, Developer.PageNumber, Developer.PageSize);
            Developer = ApiToViewMapper.MapToDeveloper(tempDevData ?? new DeveloperData(), tempGameData ?? new ListOfGames());

            Developer.LoadingData.IsLoading = false;
        }

        private async Task<ListOfGames> GetGameData(int? developerId, int? page, int? PageSize)
        {
            return await ApiLogic.GetSearch(new ApiParameters { DeveloperIds = [developerId], pageNumber = page, pageSize = PageSize }) ?? new ListOfGames();
        }

        private void SetUpRelayCommands()
        {
            Developer.NextPageViewCommand = new RelayCommand<object>(x =>
            {
                if (GetMaxPageNumber() >= Developer.PageNumber)
                {
                    Developer.PageNumber++;
                }

                GetListPage();
            });

            Developer.PreviousPageViewCommand = new RelayCommand<object>(x =>
            {
                if (Developer.PageNumber > 1)
                {
                    Developer.PageNumber--;
                }

                GetListPage();
            });

            Developer.LastPageViewCommand = new RelayCommand<object>(x =>
            {
                Developer.PageNumber = GetMaxPageNumber();
                GetListPage();
            });

            Developer.FirstPageViewCommand = new RelayCommand<object>(x =>
            {
                Developer.PageNumber = 1;
                GetListPage();
            });
        }

        private int? GetMaxPageNumber()
        {
            return (Developer.GameListCount / Developer.PageSize) + 1;
        }

        private async void GetListPage()
        {
            var gameData = await GetGameData(Developer.Id, Developer.PageNumber, Developer.PageSize);

            if (gameData == null || gameData.results == null)
            {
                throw new ArgumentNullException(nameof(gameData) + ", " + nameof(gameData.results));
            }

            var tempGameDatalist = gameData.results;
            if (tempGameDatalist == null)
            {
                throw new ArgumentNullException(nameof(tempGameDatalist));
            }

            var gameList = ApiToViewMapper.MapToGame(tempGameDatalist);
            developer.GameList = [];


            for (int i = 0; i < gameList?.Count; i++)
            {
                var game = gameList[i];
                developer.GameList.Add(game);
            }
        }
    }
}
