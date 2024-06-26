using CommunityToolkit.Mvvm.Messaging;
using System.Windows.Controls;
using System.Windows.Input;

namespace GameGizmo.MVVM.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void Panel_MouseClick(object sender, MouseButtonEventArgs e)
        {
            var item = ((Grid)sender);

            WeakReferenceMessenger.Default.Send(item);
        }
    }
}
