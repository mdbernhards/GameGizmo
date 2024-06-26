using GameGizmo.MVVM.Model;
using GameGizmo.MVVM.ViewModel;
using System.Text.RegularExpressions;
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
            var listViewObject = ((ListViewItem)sender).Content;

            if (listViewObject is Game game)
            {
                if (DataContext is SearchResultsViewModel vm)
                {
                    vm.Search.SelectedGame = game;
                }
            }
            else if (listViewObject is Developer developer)
            {
                if (DataContext is SearchResultsViewModel vm)
                {
                    vm.Search.SelectedDeveloper = developer;
                }
            }
            else
            {
                throw new ArgumentException(nameof(listViewObject));
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
