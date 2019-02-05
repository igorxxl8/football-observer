using System.Windows;
using FootballManager;

namespace SecondPlugin
{
    /// <summary>
    /// Логика взаимодействия для PluginWIndow.xaml
    /// </summary>
    public partial class PluginWindow : Window
    {
        public PluginWindow()
        {
            InitializeComponent();
            Loaded += PluginWIndow_Loaded;
        }

        private void PluginWIndow_Loaded(object sender, RoutedEventArgs e) => DataContext = new ClubViewModel();
    }
}
