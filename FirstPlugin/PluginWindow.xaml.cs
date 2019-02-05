using System.Windows;
using FootballManager;

namespace FirstPlugin
{
    /// <summary>
    /// Логика взаимодействия для PlaginWindow.xaml
    /// </summary>
    public partial class PlaginWindow : Window
    {
        public PlaginWindow()
        {
            InitializeComponent();
            Loaded += PlaginWindow_Loaded;
        }

        private void PlaginWindow_Loaded(object sender, RoutedEventArgs e) => DataContext = new PlayerViewModel();
    }
}
