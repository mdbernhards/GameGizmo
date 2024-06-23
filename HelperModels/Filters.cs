using GameGizmo.Core;
using GameGizmo.HelperModels;
using GameGizmo.Models;

namespace GameGizmo.MVVM.Model
{
    internal class Filters : ObservableObject
    {
        private List<Platform>? listOfPlatforms;
        public List<Platform>? ListOfPlatforms {
            get => listOfPlatforms;
            set
            {
                listOfPlatforms = value;
                OnPropertyChanged();
            }
        }

        private List<Store>? listOfStores;
        public List<Store>? ListOfStores
        {
            get => listOfStores;
            set
            {
                listOfStores = value;
                OnPropertyChanged();
            }
        }

        private List<Genres>? listOfGenres;
        public List<Genres>? ListOfGenres
        {
            get => listOfGenres;
            set
            {
                listOfGenres = value;
                OnPropertyChanged();
            }
        }

        public Filters() 
        {
            ListOfPlatforms = [];
            ListOfStores = [];
        }

        public ApiParameters? Parameters = new();

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

        private int? pageSize = 20;
        public int? PageSize
        {
            get => pageSize;
            set
            {
                pageSize = value;
                OnPropertyChanged();
            }
        }

        public DateTime? ReleaseRangeFrom { get; set; } = new DateTime(1980, 1, 1);

        public DateTime? ReleaseRangeTo { get; set; } = DateTime.Today;

        public string? MetacriticScoreFrom { get; set; } = "0";

        public string? MetacriticScoreTo { get; set; } = "100";

        public List<int>? PageSizeOptions { get; set; } = [10, 20, 30, 40];

        public KeyValuePair<string, string> SortOrder { get; set; }

        public Dictionary<string, string> SortOrderOptions { get; set; } = CreateDictionary();

        private static Dictionary<string, string> CreateDictionary()
        {
            var dictionary = new Dictionary<string, string>
            {
                { "name", "Name (A to Z)" },
                { "-name", "Name (Z to A)" },
                { "-released", "Newer" },
                { "released", "Older" },
                { "-added", "Most popular" },
                { "added", "Low popularity" },
                { "-updated", "Recently updated" },
                { "updated", "Rarely updated" },
                { "-rating", "Highest rated" },
                { "rating", "Lowest rated" },
                { "-metacritic", "Metacritic top" },
                { "metacritic", "Metacritic bottom" }
            };

            return dictionary;
        }
    }
}
