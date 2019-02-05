using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для AddClubWindow.xaml
    /// </summary>
    public partial class AddClubWindow : Window
    {
        public AddClubWindow()
        {
            InitializeComponent();
            Loaded += AddClubWindow_Loaded;
        }

        private void AddClubWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var item in Base.Players)
            {
                if (item.Club == Base.freeClub)
                {
                    CheckBox checkBox = new CheckBox { Content = item.Name };
                    PlayerCmBox.Items.Add(checkBox);
                    PlayerCmBox.SelectionChanged += PlayerChoice_SelectionChanged;
                }
            }
            foreach (var item in Base.Leagues)
            {
                AddLeBox.Items.Add(item.Name);
            }
        }

        private void PlayerChoice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(PlayerCmBox.SelectedIndex != 1) PlayerCmBox.SelectedIndex = 0;
        }

        private void AddClubButton_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            foreach(var item in Base.Clubs)
            {
                if(item.Name == NameText.Text)
                {
                    MessageBox.Show("Ошибка. Клуб с таким названием уже существует!\nВведите другое название!");
                    error = true;
                    NameText.Clear();
                }
            }
            if (string.IsNullOrWhiteSpace(NameText.Text) || string.IsNullOrEmpty(NameText.Text))
            {
                MessageBox.Show("Некорректный ввод. Пустая строка названия клуба или введен пробел!");
                error = true;
                NameText.Clear();
            }
            if (string.IsNullOrEmpty(CoachText.Text) || string.IsNullOrWhiteSpace(CoachText.Text) )
            {
                MessageBox.Show("Некорректный ввод. Пустая строка тренера или введен пробел!");
                error = true;
                CoachText.Clear();
            }
            if (error) { return;  }
            Club club = new Club(NameText.Text, CoachText.Text, null, null,LogoIm, LogoText.Text);
            if(AddLeBox.Text != "Вне лиги")
            {
                int index = 0;
                foreach(var item in Base.Leagues)
                {
                    if(item.Name == AddLeBox.Text)
                    {
                        club.League = item;
                        if (Base.Leagues[index].Clubs == null)
                        {
                            List<Club> list = new List<Club> { club };
                            Base.Leagues[index].Clubs = list;
                        }
                        else Base.Leagues[index].Clubs.Add(club);
                        break;
                    }
                    index++;
                }
                if (index == Base.Leagues.Count) 
                {
                    MessageBox.Show("Такая лига не найдена. \nВыберите из списка сушествующих или Вне лиги, обозначающее что клуб не входит в лигу!");
                    AddLeText.Text = "Вне лиги";
                    AddLeBox.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                club.League = Base.freeLeague;
                if (Base.freeLeague.Clubs == null)
                {
                    List<Club> list = new List<Club> { club };
                    Base.freeLeague.Clubs = list;
                }
                else Base.freeLeague.Clubs.Add(club);
            }
            if (PlayerCmBox.SelectedIndex!=1)
            {
                var list = new List<Player>();
                for (int i = 2, j = PlayerCmBox.Items.Count; i < j; i++)
                {
                    CheckBox o = (CheckBox)PlayerCmBox.Items.GetItemAt(i);
                    if (o.IsChecked == true)
                    {
                        foreach (var it in Base.Players)
                        {
                            if (it.Name == (string)o.Content)
                            {
                                it.Club = club;
                                Base.freeClub.Players.Remove(it);
                                list.Add(it);
                                break;
                            }
                        }
                    }
                }
                club.Players = list;
                Base.Clubs.Add(club);
            }
            else Base.Clubs.Add(club);
            MessageBox.Show(messageBoxText: "Клуб добавлен");
            DialogResult = true;
        }

        private void AddLogoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "Все форматы|*.jpg;*.jpeg;*.gif;*.png;*.ico;*.bmp|*.JPG|*.jpg|*.JPEG|*.jpeg|*.GIF|*.gif|*.PNG|*.png|*.ICO|*.ico|*.BMP|*.bmp", InitialDirectory= "E:\\Учеба_3_семестр\\ИСП\\test2\\test2\\Content\\Изображения"};
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                LogoText.Text = filename;
            }
        }
    }
}
