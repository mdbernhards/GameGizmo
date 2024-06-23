using GameGizmo.Logic;
using System.Windows;
using System.Windows.Controls;

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
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}