using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для EditClubWindow.xaml
    /// </summary>
    public partial class EditClubWindow : Window
    {
        public EditClubWindow(Club club)
        {
            InitializeComponent();
            Loaded += EditClubWindow_Loaded;
            Club = club;  
        }
        
        public Club Club { get; }

        private void EditClubWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Ed.DataContext = Club;
            LeagueBox.Items.Add(Club.League.Name);
            LeagueBox.SelectedIndex = 0;
            if (Club.League.Name != Base.freeLeague.Name) LeagueBox.Items.Add(Base.freeLeague.Name);
            foreach(var item in Base.Leagues)
            {
                if (Club.League.Name != item.Name) LeagueBox.Items.Add(item.Name);
            }
            listfreeplay.ItemsSource = Base.freeClub.Players;
        }

        private void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            LogoBut.IsEnabled = !LogoBut.IsEnabled;
            OKBut.IsEnabled = !OKBut.IsEnabled;
            NameText.IsEnabled = !NameText.IsEnabled;
            CoachText.IsEnabled = !CoachText.IsEnabled;
            EditBut.IsEnabled = !EditBut.IsEnabled;
            AddFreeBut.IsEnabled = !AddFreeBut.IsEnabled;
            RemBut.IsEnabled = !RemBut.IsEnabled;
            LeagueBox.IsEnabled = !LeagueBox.IsEnabled;
            listfreeplay.IsEnabled = !listfreeplay.IsEnabled;
        }

        private void LogoBut_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "Все форматы|*.jpg;*.jpeg;*.gif;*.png;*.ico;*.bmp|*.JPG|*.jpg|*.JPEG|*.jpeg|*.GIF|*.gif|*.PNG|*.png|*.ICO|*.ico|*.BMP|*.bmp", InitialDirectory = Directory.GetCurrentDirectory() + "\\Content" };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                LogoChange.Text = filename;
            }
        }

        private void RemBut_Click(object sender, RoutedEventArgs e)
        {
            if(listplay.Items.Count!=0)
            {
                List<Player> list = new List<Player>(Club.Players);
                Base.freeClub.Players.Add(list[listplay.SelectedIndex]);
                Club.Players[listplay.SelectedIndex].Club = Base.freeClub;
                int ind = list.IndexOf(Club.Players[listplay.SelectedIndex]);
                list.RemoveAt(ind);
                Club.Players = list;
                listplay.ItemsSource = Club.Players;
            }
            listplay.SelectedIndex = 0;
        }

        private void AddFreeBut_Click(object sender, RoutedEventArgs e)
        {
            if (listfreeplay.Items.Count != 0)
            {
                List<Player> list = new List<Player>(Base.freeClub.Players);
                List<Player> list1;
                if (Club.Players==null) list1 = new List<Player>();
                else list1 = new List<Player>(Club.Players);
                list1.Add(list[listfreeplay.SelectedIndex]);
                Club.Players = list1;
                Base.freeClub.Players[listfreeplay.SelectedIndex].Club = Club;
                int ind = list.IndexOf(Base.freeClub.Players[listfreeplay.SelectedIndex]);
                list.RemoveAt(ind);
                Base.freeClub.Players = list;
                listfreeplay.ItemsSource = Base.freeClub.Players;
            }
            listfreeplay.SelectedIndex = 0;
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            List<Club> listlol = new List<Club>();
            foreach (var item in Base.Clubs)
            {
                listlol.Add(item);
            }
            listlol.Remove(Club);
            bool er = false;
            if (Club.ErrorName != null)
            {
                MessageBox.Show(Club.ErrorName);
                er = true;
            }
            else
            {
                foreach (var item in listlol)
                {
                    if (NameText.Text == item.Name)
                    {
                        MessageBox.Show("Клуб с таким названием уже существует, введите другое");
                        er = true;
                        break;
                    }
                }
            }
            if (Club.ErrorCo != null)
            {
                MessageBox.Show(Club.ErrorCo);
                er = true;
            }
            if (er)
            {
                return;
            }
            if(LeagueBox.Text!= "Вне лиги")
            {
                if(Club.League.Name == "Вне лиги")
                {
                    List<Club> list = new List<Club>(Base.freeLeague.Clubs);
                    int ind = list.IndexOf(Club);
                    list.RemoveAt(ind);
                    Base.freeLeague.Clubs = list;
                }
                foreach(var temp in Base.Leagues)
                {
                    if(Club.League.Name == temp.Name)
                    {
                        List<Club> list = new List<Club>(temp.Clubs);
                        int ind = list.IndexOf(Club);
                        list.RemoveAt(ind);
                        temp.Clubs = list;
                        break;
                    }
                }
                int index = 0;
                foreach(var item in Base.Leagues)
                {
                    if(item.Name == LeagueBox.Text)
                    {
                        Club.League = item;
                        if (Base.Leagues[index].Clubs == null)
                        {
                            List<Club> list = new List<Club> { Club };
                            Base.Leagues[index].Clubs = list;
                        }
                        else Base.Leagues[index].Clubs.Add(Club);
                    }
                    index++;
                }
            }
            else
            {
                if (Club.League != Base.freeLeague)
                {
                    foreach (var temp in Base.Leagues)
                    {
                        if (Club.League.Name == temp.Name)
                        {
                            List<Club> list = new List<Club>(temp.Clubs);
                            int ind = list.IndexOf(Club);
                            list.RemoveAt(ind);
                            temp.Clubs = list;
                            break;
                        }
                    }
                    Club.League = Base.freeLeague;
                    if (Base.freeLeague.Clubs == null)
                    {
                        List<Club> list = new List<Club> { Club };
                        Base.freeLeague.Clubs = list;
                    }
                    else Base.freeLeague.Clubs.Add(Club);
                }
            }
            Club.LogoPath = LogoChange.Text;
            OnEdit_Click(sender, e);
            MessageBox.Show("Клуб изменен!");
            OnEdit.IsChecked = false;
        }

        private void OKBut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void RemBut_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(Base.freeClub.Players!=null)
            listfreeplay.ItemsSource = new ObservableCollection<Player>(Base.freeClub.Players);
        }

        private void AddFreeBut_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(Club.Players!=null)
            listplay.ItemsSource = new ObservableCollection<Player>(Club.Players);
        }
    }
}
