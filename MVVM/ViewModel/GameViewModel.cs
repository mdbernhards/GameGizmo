﻿using GameGizmo.Core;
using GameGizmo.HelperModels;
using GameGizmo.Logic;
using GameGizmo.MVVM.Model;
using System.Text.RegularExpressions;

namespace GameGizmo.MVVM.ViewModel
{
    internal class GameViewModel(ApiLogic apiLogic) : ObservableObject
    {
        private Game? game;
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
                        if (game.description == null || game.description == string.Empty)
                        {
                            game.description = "No description found!";
                        }
                        else
                        {
                            Regex rgx = new Regex("<p>|</p>");
                            game.description = Regex.Replace(game.description, @"<[^>]*>", String.Empty);
                        }

                        OnPropertyChanged();
                    }
                }
            }
        }
        public ApiLogic ApiLogic { get; set; } = apiLogic;

        public LoadingData LoadingData { get; set; } = new LoadingData();

        public async void GetGameView(int? gameId)
        {
            if (gameId == null)
            {
                return;
            }

            LoadingData.IsLoading = true;

            Game = await ApiLogic.GameQuery(gameId);

            LoadingData.IsLoading = false;
        }
    }
}
