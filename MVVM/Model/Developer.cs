﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Models;
using System.Collections.ObjectModel;

namespace GameGizmo.MVVM.Model
{
    internal class Developer : ObservableObject
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public int? GamesCount { get; set; }

        public string? ImageBackground { get; set; } = "http://example.com";

        public string? Description { get; set; }

        public int? GameListCount { get; set; }

        private ObservableCollection<Game> gameList { get; set; } = [];

        public ObservableCollection<Game> GameList
        {
            get => gameList;
            set
            {
                gameList = value;
                OnPropertyChanged();
            }
        }

        public LoadingData LoadingData { get; set; } = new();

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

        private Game? selectedGame;
        public Game? SelectedGame
        {
            get { return selectedGame; }
            set
            {
                if (!Equals(selectedGame, value))
                {
                    selectedGame = value;
                    OnPropertyChanged(nameof(selectedGame));
                    WeakReferenceMessenger.Default.Send(selectedGame ?? new Game());
                }
            }
        }
    }
}
