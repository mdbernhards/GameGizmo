using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.Models;

namespace GameGizmo.MVVM.Model
{
    internal class Home : ObservableObject
    {
        private List<Game> gameList { get; set; } = [ new(), new(), new(), new()];

        public List<Game> GameList
        {
            get { return gameList; }
            set
            {
                if (!Equals(gameList, value))
                {
                    gameList = value;
                    OnPropertyChanged();
                }
            }
        }

        public LoadingData LoadingData { get; set; } = new();
    }
}
