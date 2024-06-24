using CommunityToolkit.Mvvm.ComponentModel;
using GameGizmo.Logic.Interfaces;
using GameGizmo.MVVM.Model;
using System.Text.RegularExpressions;

namespace GameGizmo.MVVM.ViewModel
{
    internal class GameViewModel(IApiLogic apiLogic, IApiToViewMapper apiToViewMapper) : ObservableObject
    {
        public IApiLogic ApiLogic { get; set; } = apiLogic;

        public IApiToViewMapper ApiToViewMapper { get; set; } = apiToViewMapper;

        private Game? game = new();
        public Game? Game
        {
            get { return game; }
            set
            {
                if (!Equals(game, value))
                {
                    game = value;

                    if (game != null)
                    {
                        if (game.Description == null || game.Description == string.Empty)
                        {
                            game.Description = "No description found!";
                        }
                        else
                        {
                            game.Description = Regex.Replace(game.Description, @"<[^>]*>", String.Empty);
                        }

                        OnPropertyChanged();
                    }
                }
            }
        }

        public async void GetGameView(int? gameId)
        {
            if (gameId == null || Game == null)
            {
                return;
            }

            Game.LoadingData.IsLoading = true;
            var tempGame = await ApiLogic.GameQuery(gameId);
            var tempScreenshots = await ApiLogic.GameScreenshotQuery(gameId);

            Game = ApiToViewMapper.MapToGame(tempGame ?? new(), tempScreenshots?.results ?? []);

            Game.LoadingData.IsLoading = false;
        }
    }
}
