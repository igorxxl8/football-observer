using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для EditLeagueWindow.xaml
    /// </summary>
    public partial class EditLeagueWindow : Window
    {
        private League League { get; }

        public EditLeagueWindow(League league)
        {
            InitializeComponent();
            League = league;
            Loaded += EditLeagueWindow_Loaded;
        }

        private void EditLeagueWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Ed.DataContext = League;
            listaddclub.ItemsSource = Base.freeLeague.Clubs;
        }

        private void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            LogoBut.IsEnabled = !LogoBut.IsEnabled;
            NameText.IsEnabled = !NameText.IsEnabled;
            CountryText.IsEnabled = !CountryText.IsEnabled;
            OKBut.IsEnabled = !OKBut.IsEnabled;
            RemBut.IsEnabled = !RemBut.IsEnabled;
            AddBut.IsEnabled = !AddBut.IsEnabled;
            EditBut.IsEnabled = !EditBut.IsEnabled;
            listaddclub.IsEnabled = !listaddclub.IsEnabled;
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
            if(listclub.Items.Count !=0)
            {
                List<Club> list = new List<Club>(League.Clubs);
                Base.freeLeague.Clubs.Add(list[listclub.SelectedIndex]);
                League.Clubs[listclub.SelectedIndex].League = Base.freeLeague;
                int ind = list.IndexOf(League.Clubs[listclub.SelectedIndex]);
                list.RemoveAt(ind);
                League.Clubs = list;
                listclub.ItemsSource = League.Clubs;
            }
            listclub.SelectedIndex = 0;
        }

        private void AddBut_Click(object sender, RoutedEventArgs e)
        {
            if (listaddclub.Items.Count != 0)
            {
                List<Club> list = new List<Club>(Base.freeLeague.Clubs);
                List<Club> list1;
                if (League.Clubs == null) list1 = new List<Club>();
                else list1 = new List<Club>(League.Clubs);
                list1.Add(list[listaddclub.SelectedIndex]);
                League.Clubs = list1;
                Base.freeLeague.Clubs[listaddclub.SelectedIndex].League = League;
                int ind = list.IndexOf(Base.freeLeague.Clubs[listaddclub.SelectedIndex]);
                list.RemoveAt(ind);
                Base.freeLeague.Clubs = list;
                listaddclub.ItemsSource = Base.freeLeague.Clubs;
            }
            listaddclub.SelectedIndex = 0;
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            List<League> listlol = new List<League>();
            foreach(var item in Base.Leagues)
            {
                listlol.Add(item);
            }
            listlol.Remove(League);
            bool er = false;
            if (League.ErrorName != null)
            {
                MessageBox.Show(League.ErrorName);
                er = true;
            }
            else
            {
                foreach(var item in listlol)
                {
                    if(NameText.Text == item.Name)
                    {
                        MessageBox.Show("Лига с таким названием уже существует, введите другое!");
                        er = true;
                        break;
                    }
                }
            }
            if (League.ErrorCo != null)
            {
                MessageBox.Show(League.ErrorCo);
                er = true;
            }
            if (er)
            {
                return;
            }
            League.LogoPath = LogoChange.Text;
            OnEdit_Click(sender, e);
            MessageBox.Show("Лига изменена!");
            OnEdit.IsChecked = false;
        }

        private void OKBut_Click(object sender, RoutedEventArgs e) => DialogResult = true;

        private void RemBut_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (Base.freeLeague.Clubs != null)
                listaddclub.ItemsSource = new ObservableCollection<Club>(Base.freeLeague.Clubs);
        }

        private void AddBut_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if(League.Clubs!=null) listclub.ItemsSource = new ObservableCollection<Club>(League.Clubs);
        }
    }
}