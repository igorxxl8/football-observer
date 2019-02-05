using System.Windows;
using FootballManager;

namespace ThirdPlugin
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class PluginWindow : Window
    {
        public PluginWindow()
        {
            InitializeComponent();
            Loaded += PluginWindow_Loaded;
        }

        private void PluginWindow_Loaded(object sender, RoutedEventArgs e) => DataContext = new LeagueViewModel();
    }
}
