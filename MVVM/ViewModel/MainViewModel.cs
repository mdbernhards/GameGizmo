﻿using GameGizmo.Core;
using GameGizmo.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameGizmo.MVVM.ViewModel
{
    internal class MainViewModel : ObservableObject
    {
        public HomeViewModel? Home { get; set; }

        public SearchResultsViewModel SearchResults { get; set; }

        public RelayCommand HomeViewCommand { get; set; }

        public RelayCommand SearchResultsViewCommand { get; set; }

        private object? _currentView;

        public object? CurrentView
        {
            get { return _currentView; }
            set 
            { 
                _currentView = value;
                OnProperrtyChanged();
            }
        }


        public MainViewModel()
        {
            Home = new HomeViewModel();
            SearchResults = new SearchResultsViewModel();
            CurrentView = Home;

            HomeViewCommand = new RelayCommand(x =>
            {
                CurrentView = Home;
            });

            SearchResultsViewCommand = new RelayCommand(x =>
            {
                CurrentView = SearchResults;
            });
        }
    }
}