using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.Logic.Interfaces;
using GameGizmo.Models;
using GameGizmo.MVVM.Model;

namespace GameGizmo.MVVM.ViewModel
{
    internal class HomeViewModel : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; }

        public IApiToViewMapper ApiToViewMapper { get; set; }

        private Home? home = new();
        public Home? Home
        {
            get { return home; }
            set
            {
                if (!Equals(home, value))
                {
                    home = value;
                    OnPropertyChanged();
                }
            }
        }

        public HomeViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper)
        {
            ApiLogic = apiLogic;
            ApiToViewMapper = apiToViewMapper;

            SetUpPopularGameButtons();
        }

        public async void SetUpPopularGameButtons()
        {
            if (Home == null)
            {
                throw new ArgumentNullException(nameof(Home));
            }

            Home.LoadingData.IsLoading = true;

            var gameList = await ApiLogic.GetSearch(new ApiParameters { dates = "2020-01-01," + DateTime.Now.ToString("yyyy-MM-dd"), ordering = "-newest", pageSize = 40 });
            var random = new Random();

            while (gameList?.results?.Count > 4)
            {
                var index = random.Next(gameList.results?.Count ?? 0);
                gameList.results?.RemoveAt(index);
            }

            if (gameList?.results?.Count == 4)
            {
                Home.GameList = ApiToViewMapper.MapToGame(gameList?.results ?? []);
            }

            Home.LoadingData.IsLoading = false;
        }
    }
}
