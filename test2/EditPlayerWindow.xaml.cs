using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для EditPlayerWindow.xaml
    /// </summary>
    public partial class EditPlayerWindow : Window
    {
        public Player Player { get; }

        public EditPlayerWindow(Player player)
        {
            InitializeComponent();
            Loaded += EditPlayerWindow_Loaded;
            Player = player;
            AgeText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            NumText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            AssistText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            GoalsText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
        }

        void TextBox_IsDigit(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void EditPlayerWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Ed.DataContext = Player;
            ClubBox.Items.Add(Player.Club.Name);
            ClubBox.SelectedIndex = 0;
            if(Player.Club.Name!= Base.freeClub.Name) ClubBox.Items.Add(Base.freeClub.Name);
            foreach (var item in Base.Clubs)
            {
                if(Player.Club.Name!=item.Name) ClubBox.Items.Add(item.Name);
            }
            PosBox.Items.Add(Player.Position);
            if ("Вратарь" != Player.Position) PosBox.Items.Add("Вратарь");
            if ("Защитник" != Player.Position) PosBox.Items.Add("Защитник");
            if ("Нападающий" != Player.Position) PosBox.Items.Add("Нападающий");
            if ("Полузащитник" != Player.Position) PosBox.Items.Add("Полузащитник");
            PosBox.SelectedIndex = 0;
        }

        private void OnEdit_Click(object sender, RoutedEventArgs e)
        {
            OKBut.IsEnabled = !OKBut.IsEnabled;
            PhotoBut.IsEnabled = !PhotoBut.IsEnabled;
            EditBut.IsEnabled = !EditBut.IsEnabled;
            FioText.IsEnabled = !FioText.IsEnabled;
            PosBox.IsEnabled = !PosBox.IsEnabled;
            NumText.IsEnabled = !NumText.IsEnabled;
            AgeText.IsEnabled = !AgeText.IsEnabled;
            GoalsText.IsEnabled = !GoalsText.IsEnabled;
            AssistText.IsEnabled = !AssistText.IsEnabled;
            ClubBox.IsEnabled = !ClubBox.IsEnabled;
        }

        private void EditBut_Click(object sender, RoutedEventArgs e)
        {
            List<Player> listlol = new List<Player>();
            foreach(var item in Base.Players)
            {
                listlol.Add(item);
            }
            listlol.Remove(Player);
            bool er = false;
            if (Player.ErrorName != null)
            {
                MessageBox.Show(Player.ErrorName);
                er = true;
            }
            else
            {
                foreach(var item in listlol)
                {
                    if(FioText.Text==item.Name)
                    {
                        MessageBox.Show("Игрок с таким именем существует, введите другое");
                        er = true;
                        break;
                    }
                }
            }
            if (Player.ErrorAge != null)
            {
                MessageBox.Show(Player.ErrorAge);
                er = true;
            }
            if (Player.ErrorGo != null)
            {
                MessageBox.Show(Player.ErrorGo);
                er =true;
            }
            if (Player.ErrorAs != null)
            {
                MessageBox.Show(Player.ErrorAs);
                er = true;
            }
            if (Player.ErrorNum != null)
            {
                MessageBox.Show(Player.ErrorNum);
                er = true;
            }
            if(er)
            {
                return;
            }
            Player.Position = PosBox.Text;
            if (ClubBox.Text != "Свободный агент")
            {
                if(Player.Club.Name == "Свободный агент")
                {
                    List<Player> list = new List<Player>(Base.freeClub.Players);
                    int ind = list.IndexOf(Player);
                    list.RemoveAt(ind);
                    Base.freeClub.Players = list;
                }
                foreach (var temp in Base.Clubs)
                {
                    if (Player.Club.Name == temp.Name)
                    {
                        List<Player> list = new List<Player>(temp.Players);
                        int ind = list.IndexOf(Player);
                        list.RemoveAt(ind);
                        temp.Players = list;
                        break;
                    }
                }
                int index = 0;
                foreach (var item in Base.Clubs)
                {
                    if (item.Name == ClubBox.Text)
                    {
                        Player.Club = item;
                        if (Base.Clubs[index].Players == null)
                        {
                            List<Player> list = new List<Player> { Player };
                            Base.Clubs[index].Players = list;
                        }
                        else Base.Clubs[index].Players.Add(Player);
                        break;
                    }
                    index++;
                }
            }
            else
            {
                if (Player.Club != Base.freeClub)
                {
                    foreach (var temp in Base.Clubs)
                    {
                        if (Player.Club.Name == temp.Name)
                        {
                            List<Player> list = new List<Player>(temp.Players);
                            int ind = list.IndexOf(Player);
                            list.RemoveAt(ind);
                            temp.Players = list;
                            break;
                        }
                    }
                    Player.Club = Base.freeClub;
                    if (Base.freeClub.Players == null)
                    {
                        List<Player> list = new List<Player> { Player };
                        Base.freeClub.Players = list;
                    }
                    else Base.freeClub.Players.Add(Player);
                }
            }
            Player.PhotoPath = PhotoChange.Text;
            OnEdit_Click(sender, e);
            OnEdit.IsChecked = false;
            MessageBox.Show("Футболист изменён");
        }

        private void OKBut_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void PhotoBut_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog { Filter = "Все форматы|*.jpg;*.jpeg;*.gif;*.png;*.ico;*.bmp|*.JPG|*.jpg|*.JPEG|*.jpeg|*.GIF|*.gif|*.PNG|*.png|*.ICO|*.ico|*.BMP|*.bmp", InitialDirectory = Directory.GetCurrentDirectory() + "\\Content" };
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                PhotoChange.Text = filename;
            }
        }
    }
}
