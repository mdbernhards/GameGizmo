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
            var item = ((ListViewItem)sender).Content;

            if (item is Result game)
            {
                if (DataContext is SearchResultsViewModel vm)
                {
                    vm.SelectedGame = game;
                }
            }
            else if (item is Developer developer)
            {
                if (DataContext is SearchResultsViewModel vm)
                {
                    vm.SelectedDeveloper = developer;
                }
            }

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
