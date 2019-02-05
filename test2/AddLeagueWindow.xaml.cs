using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для AddLeagueWindow.xaml
    /// </summary>
    public partial class AddLeagueWindow : Window
    {
        public AddLeagueWindow()
        {
            InitializeComponent();
            Loaded += AddLeagueWindow_Loaded;
        }

        private void AddLeagueWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Base.freeLeague.Clubs != null)
            {
                foreach (var item in Base.freeLeague.Clubs)
                {
                    CheckBox checkBox = new CheckBox { Content = item.Name };
                    ClubsBox.Items.Add(checkBox);
                }
                ClubsBox.SelectionChanged += ClubsBox_SelectionChanged;
            }
        }

        private void ClubsBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ClubsBox.SelectedIndex != 1)
                ClubsBox.SelectedIndex = 0;
        }

        private void AddLeagueButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in Base.Leagues)
            {
                if (item.Name == NameText.Text)
                {
                    MessageBox.Show("Ошибка. Лига с таким названием уже существует!\nВведите другое название!");
                    NameText.Clear();
                    return;
                }
            }
            if (string.IsNullOrEmpty(CoText.Text) || string.IsNullOrEmpty(NameText.Text) || string.IsNullOrWhiteSpace(CoText.Text) || string.IsNullOrWhiteSpace(NameText.Text))
            {
                MessageBox.Show("Некорректный ввод. Пустая строка или введен пробел!");
                NameText.Clear();
                CoText.Clear();
                return;
            }
            League league = new League(NameText.Text, CoText.Text, _clubs: null, _logoPath: LogoText.Text);
            if (ClubsBox.SelectedIndex != 1)
            {
                var list = new List<Club>();
                for (int i = 2, j = ClubsBox.Items.Count; i < j; i++)
                {
                    CheckBox o = (CheckBox)ClubsBox.Items.GetItemAt(i);
                    if (o.IsChecked == true)
                    {
                        foreach (var it in Base.Clubs)
                        {
                            if (it.Name == (string)o.Content)
                            {
                                it.League = league;
                                Base.freeLeague.Clubs.Remove(it);
                                list.Add(it);
                                break;
                            }
                        }
                    }
                }
                league.Clubs = list;
                Base.Leagues.Add(league);
            }
            else Base.Leagues.Add(league);
            MessageBox.Show("Лига добавлена");
            DialogResult = true;
        }

        private void AddLogoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "Все форматы|*.jpg;*.jpeg;*.gif;*.png;*.ico;*.bmp|*.JPG|*.jpg|*.JPEG|*.jpeg|*.GIF|*.gif|*.PNG|*.png|*.ICO|*.ico|*.BMP|*.bmp", InitialDirectory = "E:\\Учеба_3_семестр\\ИСП\\test2\\test2\\Content\\Изображения" };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                LogoText.Text = filename;
            }
        }
    }
}
