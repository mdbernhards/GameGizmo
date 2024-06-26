using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using GameGizmo.Models;
using System.Collections.ObjectModel;
using System.Windows;

namespace GameGizmo.MVVM.Model
{
    internal class Search : ObservableObject
    {
        public ObservableCollection<Game> GameList { get; set; } = [];

        public ObservableCollection<Developer> DeveloperList { get; set; } = [];

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

        private Developer? selectedDeveloper;
        public Developer? SelectedDeveloper
        {
            get { return selectedDeveloper; }
            set
            {
                if (!Equals(selectedDeveloper, value))
                {
                    selectedDeveloper = value;
                    OnPropertyChanged(nameof(selectedDeveloper));
                    WeakReferenceMessenger.Default.Send(selectedDeveloper ?? new Developer());
                }
            }
        }


        private bool isGameListVisible = false;
        public bool IsGameListVisible
        {
            get => isGameListVisible;
            set
            {
                isGameListVisible = value;
                OnPropertyChanged(nameof(DeveloperListVisibility));
                OnPropertyChanged(nameof(GameListVisibility));
            }
        }

        public bool IsDeveloperListVisible
        {
            get => !isGameListVisible;
            set
            {
                isGameListVisible = !value;
                OnPropertyChanged(nameof(DeveloperListVisibility));
                OnPropertyChanged(nameof(GameListVisibility));
            }
        }

        public Visibility GameListVisibility
        {
            get { return IsGameListVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility DeveloperListVisibility
        {
            get { return IsDeveloperListVisible ? Visibility.Visible : Visibility.Collapsed; }
        }

        public RelayCommand<object>? NextPageViewCommand { get; set; }

        public RelayCommand<object>? LastPageViewCommand { get; set; }

        public RelayCommand<object>? FirstPageViewCommand { get; set; }

        public RelayCommand<object>? PreviousPageViewCommand { get; set; }

        public RelayCommand<object>? ApplyFiltersViewCommand { get; set; }

        public Filters Filters { get; set; } = new();


        public LoadingData LoadingData { get; set; } = new();
    }
}
