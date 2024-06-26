using GameGizmo.MVVM.Model;
using GameGizmo.MVVM.ViewModel;
using Newtonsoft.Json.Linq;
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
        
        // taken from https://stackoverflow.com/questions/1268552/how-do-i-get-a-textbox-to-only-accept-numeric-input-in-wpf read more in the README.MD
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
