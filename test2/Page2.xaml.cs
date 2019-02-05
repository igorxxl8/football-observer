namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2 : System.Windows.Controls.Page
    {

        public Page2(League league)
        {
            InitializeComponent();
            DataContext = league;
        }
    }
}
