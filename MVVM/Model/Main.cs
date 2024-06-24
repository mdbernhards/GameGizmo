using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GameGizmo.MVVM.ViewModel;

namespace GameGizmo.MVVM.Model
{
    internal class Main : ObservableObject
    {
        public DeveloperViewModel? Developer { get; set; }

        public GameViewModel? Game { get; set; }

        public HomeViewModel? Home { get; set; }

        public SearchResultsViewModel? SearchResults { get; set; }

        public RelayCommand<object>? HomeViewCommand { get; set; }

        public RelayCommand<object>? SearchResultsViewCommand { get; set; }

        public RelayCommand<object>? TopGamesViewCommand { get; set; }

        public RelayCommand<object>? NewestGamesViewCommand { get; set; }

        public RelayCommand<object>? HottestGamesViewCommand { get; set; }

        public RelayCommand<object>? ListOfDevelopersViewCommand { get; set; }

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
    }
}
