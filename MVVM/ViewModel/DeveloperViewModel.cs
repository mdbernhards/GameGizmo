using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.Logic.Interfaces;
using GameGizmo.MVVM.Model;
using System.Text.RegularExpressions;

namespace GameGizmo.MVVM.ViewModel
{
    internal class DeveloperViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper) : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; } = apiLogic;

        public IApiToViewMapper ApiToViewMapper { get; set; } = apiToViewMapper;

        private Developer? developer = new();
        public Developer? Developer
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
                return;
            }

            Developer.LoadingData.IsLoading = true;

            var tempDevData = await ApiLogic.DeveloperQuery(developerId);
            var tempGameData = await ApiLogic.GetSearch(new Models.ApiParameters { DeveloperIds = [developerId] });
            Developer = ApiToViewMapper.MapToDeveloper(tempDevData ?? new Models.DeveloperData(), tempGameData ?? new Models.ListOfGames());

            Developer.LoadingData.IsLoading = false;
        }
    }
}
