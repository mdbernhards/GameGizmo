using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.HelperModels;
using GameGizmo.Logic;
using GameGizmo.MVVM.Model;
using System.Text.RegularExpressions;

namespace GameGizmo.MVVM.ViewModel
{
    internal class DeveloperViewModel(ApiLogic apiLogic) : ObservableObject
    {
        private Developer? developer;
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
                        if (developer.description == null || developer.description == string.Empty)
                        {
                            developer.description = "No description found!";
                        }
                        else
                        {
                            Regex rgx = new Regex("<p>|</p>");
                            developer.description = Regex.Replace(developer.description, @"<[^>]*>", String.Empty);
                        }


                        OnPropertyChanged();
                    }
                }
            }
        }

        public ApiLogic ApiLogic { get; set; } = apiLogic;

        public LoadingData LoadingData { get; set; } = new LoadingData();

        public async void GetDeveloperView(int? developerId)
        {
            if (developerId == null)
            {
                return;
            }

            LoadingData.IsLoading = true;
            Developer = await ApiLogic.DeveloperQuery(developerId);

            LoadingData.IsLoading = false;
        }
    }
}
