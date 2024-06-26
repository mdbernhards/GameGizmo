using GameGizmo.Logic;
using System.Windows;

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
            ApiLogic = new();
        }
    }
}