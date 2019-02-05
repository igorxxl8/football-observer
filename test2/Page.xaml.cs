using System.Collections.Generic;
using System.Windows;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для ShowPages.xaml
    /// </summary>
    public partial class Page : System.Windows.Controls.Page
    {
        public Page(Club club)
        {
            InitializeComponent();
            DataContext = club;
        }
    }
}
