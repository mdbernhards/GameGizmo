using GameGizmo.Logic;
using GameGizmo.MVVM.Model;
using ObservableObject = GameGizmo.Core.ObservableObject;

namespace GameGizmo.MVVM.ViewModel
{
    internal class GameViewModel : ObservableObject
    {
        public ApiLogic ApiLogic { get; set; }

        public Result? Game{ get; set; }

        private Game? detailedGameResult { get; set; }

        public Game? DetailedGameResult
        {
            get { return detailedGameResult; }
            set
            {
                if (!string.Equals(detailedGameResult, value))
                {
                    detailedGameResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public GameViewModel(ApiLogic apiLogic)
        {
            ApiLogic = apiLogic;
        }


        public async void GetGameView(Result game)
        {
            if (game == null)
            {
                return;
            }

            Game = game;
            DetailedGameResult = await ApiLogic.GameQuery(game.id);
        }
    }
}
