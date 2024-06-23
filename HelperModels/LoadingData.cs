using GameGizmo.Core;
using System.Windows;

namespace GameGizmo.HelperModels
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
