using GameGizmo.MVVM.Model;
using GameGizmo.MVVM.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameGizmo.MVVM.View
{
    /// <summary>
    /// Interaction logic for SearchResults.xaml
    /// </summary>
    public partial class SearchResults : UserControl
    {
        public SearchResults()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (Result)((ListViewItem)sender).Content;

            if (DataContext is SearchResultsViewModel vm)
            {
                vm.SelectedGame = item;
            }
        }
    }
}
