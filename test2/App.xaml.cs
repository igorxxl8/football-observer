using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var splashScreen = new SplashScreenWindow();
            MainWindow = splashScreen;
            splashScreen.Show();
            Task.Run(() =>
            {
                Thread.Sleep(2000);
                Dispatcher.Invoke(() =>
                {
                    var mainWindow = new MainWindow();
                    MainWindow = mainWindow;
                    splashScreen.Close();
                    mainWindow.ShowDialog();
                });
            });
        }
    }
}
