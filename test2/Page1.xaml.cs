using System.Collections.Generic;
using System.Windows;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : System.Windows.Controls.Page
    {
        public Page1(Player player)
        {
            InitializeComponent();
            DataContext = player;
        }
    }
}
