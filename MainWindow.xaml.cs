using GameGizmo.Logic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameGizmo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ApiLogic ApiLogic { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            ApiLogic = new ApiLogic();
            Test();
        }

        public async void Test()
        {
            var result = await ApiLogic.Search();
            
            foreach (var item in result.results)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(result);
            Console.WriteLine(result);
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}