using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace GameGizmo.Models
{
    internal class LoadingData : ObservableObject
    {
        private bool isLoading = false;

        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(LoadingVisibility));
                OnPropertyChanged(nameof(ContentVisibility));
            }
        }

        public Visibility LoadingVisibility
        {
            get { return IsLoading ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility ContentVisibility
        {
            get { return !IsLoading ? Visibility.Visible : Visibility.Collapsed; }
        }
    }
}
