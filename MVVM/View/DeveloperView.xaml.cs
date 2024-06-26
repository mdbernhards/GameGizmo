using GameGizmo.MVVM.Model;
using GameGizmo.MVVM.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameGizmo.MVVM.View
{
    /// <summary>
    /// Interaction logic for DeveloperView.xaml
    /// </summary>
    public partial class DeveloperView : UserControl
    {
        public DeveloperView()
        {
            InitializeComponent();
        }

        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((ListViewItem)sender).Content;

            if (item is Game game)
            {
                if (DataContext is DeveloperViewModel vm)
                {
                    vm.Developer.SelectedGame = game;
                }
            }
        }
    }
}
