using Newtonsoft.Json;
using PluginContracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Threading.Tasks;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary
    /// 

    public partial class MainWindow : Window
    {
        List<Player> temp = Base.Players;
        List<string> PictByteArray = new List<string>();
        Dictionary<string, IPlugin> Pluginss;
        public MainWindow()
        {
            InitializeComponent();
            SetWindowDataAsync();
        }
        void LoadPlugins()
        {
            Pluginss = new Dictionary<string, IPlugin>();
            foreach (var folderPath in ConfigurationManager.ConnectionStrings)
            {
                ICollection<IPlugin> plugins = GenericPluginLoader<IPlugin>.LoadPlugins(folderPath.ToString());
                if (plugins != null)
                {
                    foreach (var item in plugins)
                    {
                        Pluginss.Add(item.Name, item);
                        MenuItem itemMenu = new MenuItem { Header = item.Name };
                        itemMenu.Click += PluginActivity_Click;
                        itemMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.PluginIcon);
                        Plugins.Items.Add(itemMenu);
                    }
                }
            }
            if (Plugins.Items.Count != 0) Plugins.Visibility = Visibility.Visible;
        }
        void SetHotKeys()
        {
            KeyGestureConverter key = new KeyGestureConverter();
            KeyGesture keyG;
            object temp;
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["Open"]);
                keyG = (KeyGesture)temp;
                OpenKey.Gesture = keyG;
                OpenMenu.InputGestureText = ConfigurationManager.AppSettings["Open"];
            }
            catch { }
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["Save"]);
                keyG = (KeyGesture)temp;
                SaveKey.Gesture = keyG;
                SaveMenu.InputGestureText = ConfigurationManager.AppSettings["Save"];
            }
            catch { }
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["Exit"]);
                keyG = (KeyGesture)temp;
                ExitKey.Gesture = keyG;
                ExitBut.InputGestureText = ConfigurationManager.AppSettings["Exit"];
            }
            catch { }
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["AddPlayer"]);
                keyG = (KeyGesture)temp;
                AddPlKey.Gesture = keyG;
                AddPlMenu.InputGestureText = ConfigurationManager.AppSettings["AddPlayer"];
            }
            catch { }
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["AddClub"]);
                keyG = (KeyGesture)temp;
                AddLKey.Gesture = keyG;
                AddClMenu.InputGestureText = ConfigurationManager.AppSettings["AddClub"];
            }
            catch { }
            try
            {
                temp = key.ConvertFromString(ConfigurationManager.AppSettings["AddLea"]);
                keyG = (KeyGesture)temp;
                AddClKey.Gesture = keyG;
                AddLMenu.InputGestureText = ConfigurationManager.AppSettings["AddLea"];
            }
            catch { }
        }
        void SetPictograms()
        {
            OpenMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.Open);
            SaveMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.Save);
            ExitBut.Icon = ConvertFromBase64ToImage(Properties.Resources.Exit);
            AddPlMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.AddPl);
            AddClMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.AddCl);
            AddLMenu.Icon = ConvertFromBase64ToImage(Properties.Resources.AddL);
            ClubShow.Icon = ConvertFromBase64ToImage(Properties.Resources.ShowClub);
            PlayerShow.Icon = ConvertFromBase64ToImage(Properties.Resources.ShowPlayer);
            LeagueShow.Icon = ConvertFromBase64ToImage(Properties.Resources.ShowL);
        }
        async void SetWindowDataAsync()
        {
            SetHotKeys();
            SetPictograms();
            await Task.Run(() => Dispatcher.Invoke(() => LoadPlugins()));
        }
        Image ConvertFromBase64ToImage(string str)
        {
            byte[] arr = Convert.FromBase64String(str);
            BitmapImage bitmapImageCreater = new BitmapImage();
            bitmapImageCreater.BeginInit();
            bitmapImageCreater.StreamSource = new MemoryStream(arr);
            bitmapImageCreater.EndInit();
            Image image = new Image
            {
                Source = bitmapImageCreater
            };
            return image;
        }
        void PluginActivity_Click(object sender, RoutedEventArgs e)
        {
            if (sender is MenuItem b)
            {
                string key = b.Header.ToString();
                if (Pluginss.ContainsKey(key))
                {
                    IPlugin plugin = Pluginss[key];
                    plugin.Do();
                }
            }
        }
        void MainWindow_Activated(object sender, EventArgs e)
        {
            listpl.ItemsSource = new ObservableCollection<Player>(Base.ReadAllPlayers);
            listcl.ItemsSource = new ObservableCollection<Club>(Base.ReadAllClubs);
            listll.ItemsSource = new ObservableCollection<League>(Base.ReadAllLeagues);
        }
        void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            AddPlayerWindow addPlayerWindow = new AddPlayerWindow();
            addPlayerWindow.ShowDialog();
        }
        void AddClub_Click(object sender, RoutedEventArgs e)
        {
            AddClubWindow addClubWindow = new AddClubWindow();
            addClubWindow.ShowDialog();
            FilterChoice.Items.Clear();
            FilterChoice.Items.Add("Фильтрация");
            CheckBox tempcheckBox = new CheckBox() { Content = Base.freeClub.Name };
            FilterChoice.Items.Add(tempcheckBox);
            foreach (var item in Base.Clubs)
            {
                CheckBox checkBox = new CheckBox() { Content = item.Name };
                FilterChoice.Items.Add(checkBox);
            }
            FilterChoice.Items.Add("Упорядочить по возрастанию голов");
            FilterChoice.Items.Add("Упорядочить по убыванию голов");
            FilterChoice.Items.Add("Группировка по амплуа");
            FilterChoice.Items.Add("Минимальное количество голов");
            FilterChoice.Items.Add("Максимальное количество голов");
            FilterChoice.SelectedIndex = 0;
        }
        void AddLeague_Click(object sender, RoutedEventArgs e)
        {
            AddLeagueWindow addLeagueWindow = new AddLeagueWindow();
            addLeagueWindow.ShowDialog();
        }
        void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (ChoiceComboBox.SelectedIndex == 1)
            {
                if (listpl.Items.Count != 0)
                {
                    EditPlayerWindow editPlayerWindow = new EditPlayerWindow(Base.Players[listpl.SelectedIndex]);
                    editPlayerWindow.ShowDialog();
                }
            }
            else if (ChoiceComboBox.SelectedIndex == 2)
            {
                if (listcl.Items.Count != 0)
                {
                    EditClubWindow editClubWindow = new EditClubWindow(Base.Clubs[listcl.SelectedIndex]);
                    editClubWindow.ShowDialog();
                }
            }
            else if (ChoiceComboBox.SelectedIndex == 3)
            {
                if (listll.Items.Count != 0)
                {
                    EditLeagueWindow editLeagueWindow = new EditLeagueWindow(Base.Leagues[listll.SelectedIndex]);
                    editLeagueWindow.ShowDialog();
                }
            }
        }
        void Update(object sender, RoutedEventArgs e)
        {
            if (ChoiceComboBox.SelectedIndex == 1 && FilterChoice.Items.Count != 0)
            {
                FilterChoice.IsEnabled = true;
                Grid.SetRow(listcl, 3);
                Grid.SetRow(listll, 3);
                if (FilterChoice.SelectedIndex == 0)
                {
                    temp = new List<Player>(Base.ReadAllPlayers);
                    listpl.ItemsSource = new ObservableCollection<Player>(Base.ReadAllPlayers);
                    for (int i = 1, j = FilterChoice.Items.Count - Base.Clubs.Count + 1; i < j; i++)
                    {
                        try
                        {
                            CheckBox o = (CheckBox)FilterChoice.Items.GetItemAt(i);
                            o.IsChecked = false;
                            FilterChoice.Items.RemoveAt(i);
                            FilterChoice.Items.Insert(i, o);
                        }
                        catch { }
                    }
                }
                else if (FilterChoice.SelectedItem.ToString() != "Упорядочить по убыванию голов" && FilterChoice.SelectedItem.ToString() != "Упорядочить по возрастанию голов")
                {
                    List<string> list = new List<string>();
                    for (int i = 1, j = FilterChoice.Items.Count - Base.Clubs.Count + 1; i < j; i++)
                    {
                        try
                        {
                            CheckBox o = (CheckBox)FilterChoice.Items.GetItemAt(i);
                            if (o.IsChecked == true)
                            {
                                list.Add((string)o.Content);
                            }
                        }
                        catch{ }
                    }
                    temp = Base.SelectionParallel(list);
                    listpl.ItemsSource = Base.SelectionParallel(list);
                }
                else if (FilterChoice.SelectedItem.ToString() == "Упорядочить по возрастанию голов") listpl.ItemsSource = Base.OrderingParallel(true, temp);
                else listpl.ItemsSource = Base.OrderingParallel(false, temp);
                listpl.SelectedIndex = 0;
                Grid.SetRow(listpl, 2);
                if (FilterChoice.SelectedItem.ToString() == "Группировка по амплуа") listpl.ItemsSource = Base.GroupingParallel(temp);
                if (FilterChoice.SelectedItem.ToString() == "Минимальное количество голов") listpl.ItemsSource =  Base.AgregatingParallel(false, temp);
                if (FilterChoice.SelectedItem.ToString() == "Максимальное количество голов") listpl.ItemsSource = Base.AgregatingParallel(true, temp);
            }
            else if (ChoiceComboBox.SelectedIndex == 2)
            {
                FilterChoice.IsEnabled = false;
                Grid.SetRow(listpl, 3);
                Grid.SetRow(listll, 3);
                listcl.ItemsSource = new ObservableCollection<Club>(Base.ReadAllClubs);
                listcl.SelectedIndex = 0;
                Grid.SetRow(listcl, 2);
            }
            else if (ChoiceComboBox.SelectedIndex == 3)
            {
                FilterChoice.IsEnabled = false;
                Grid.SetRow(listpl, 3);
                Grid.SetRow(listcl, 3);
                listll.ItemsSource = new ObservableCollection<League>(Base.ReadAllLeagues);
                listll.SelectedIndex = 0;
                Grid.SetRow(listll, 2);
            }
        }
        void Remove_Click(object sender, RoutedEventArgs e)
        {
            if (ChoiceComboBox.SelectedIndex == 0) { return; }
            DialogWindow dialogWindow = new DialogWindow();
            if (ChoiceComboBox.SelectedIndex == 1)
            {
                if (listpl.Items.Count != 0 && dialogWindow.ShowDialog() == true)
                {
                    if (Base.Players[listpl.SelectedIndex].Club == Base.freeClub)
                    {
                        List<Player> list = new List<Player>(Base.freeClub.Players);
                        int ind = list.IndexOf(Base.Players[listpl.SelectedIndex]);
                        list.RemoveAt(ind);
                        Base.freeClub.Players = list;
                    }
                    else
                    {
                        foreach (var temp in Base.Clubs)
                        {
                            if (Base.Players[listpl.SelectedIndex].Club.Name == temp.Name)
                            {
                                List<Player> list = new List<Player>(temp.Players);
                                int ind = list.IndexOf(Base.Players[listpl.SelectedIndex]);
                                list.RemoveAt(ind);
                                temp.Players = list;
                                break;
                            }
                        }
                    }
                    Base.Players.RemoveAt(listpl.SelectedIndex);
                }
            }
            else if (ChoiceComboBox.SelectedIndex == 2)
            {
                if (listcl.Items.Count != 0 && dialogWindow.ShowDialog() == true)
                {
                    if (Base.Clubs[listcl.SelectedIndex].League == Base.freeLeague)
                    {
                        List<Club> list = new List<Club>(Base.freeLeague.Clubs);
                        int ind = list.IndexOf(Base.Clubs[listcl.SelectedIndex]);
                        list.RemoveAt(ind);
                        Base.freeLeague.Clubs = list;

                    }
                    else
                    {
                        foreach (var item in Base.Leagues)
                        {
                            if (item.Clubs != null)
                            {
                                foreach (var node in item.Clubs)
                                {
                                    if (node.Name == Base.Clubs[listcl.SelectedIndex].Name)
                                    {
                                        item.Clubs.Remove(node);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    foreach (var item in Base.Players)
                    {
                        if (item.Club.Name == Base.Clubs[listcl.SelectedIndex].Name)
                        {
                            item.Club = Base.freeClub;
                            Base.freeClub.Players.Add(item);
                        }
                    }
                    Base.Clubs.RemoveAt(listcl.SelectedIndex);
                }
            }
            else
            {
                if (listll.Items.Count != 0 && dialogWindow.ShowDialog() == true)
                {
                    foreach (var item in Base.Clubs)
                    {
                        if (item.League.Name == Base.Leagues[listll.SelectedIndex].Name)
                        {
                            item.League = Base.freeLeague;
                            Base.freeLeague.Clubs.Add(item);
                        }
                    }
                    Base.Leagues.RemoveAt(index: listll.SelectedIndex);
                }
            }
            Update(sender: sender, e: e);
        }
        void ChoiceComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) => Update(sender, e);
        void ExitBut_Click(object sender, RoutedEventArgs e)
        {
            DialogWindow dialog = new DialogWindow();
            bool? result = dialog.ShowDialog();
            if (result == true) Close();
        }
        void ShowClub_Click(object sender, RoutedEventArgs e)
        {
            ShowPages pages = new ShowPages(2);
            pages.ShowDialog();
            
        }
        void ShowPlayer_Click(object sender, RoutedEventArgs e)
        {
            ShowPages pages = new ShowPages(1);
            pages.ShowDialog();
        }
        void ShowLeague_Click(object sender, RoutedEventArgs e)
        {
            ShowPages pages = new ShowPages(3);
            pages.ShowDialog();
        }
        void ShowDiagram_Click(object sender, RoutedEventArgs e)
        {
            Diagram diagram = new Diagram();
            diagram.ShowDialog();
        }
        void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog fileDialog = new Microsoft.Win32.SaveFileDialog{ Filter = string.Format("EFE|*{0}", "efe"), InitialDirectory = Directory.GetCurrentDirectory()+"\\Content", DefaultExt = "EFE", AddExtension = true };
            string path;
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                path = fileDialog.FileName;
                List<string> arr = new List<string>();
                foreach (var item in Base.Clubs)
                {
                    string b = JsonConvert.SerializeObject(item);
                    arr.Add(b);
                }
                string c = JsonConvert.SerializeObject(Base.freeClub);
                arr.Add("<freeclubs>");
                arr.Add(c);
                foreach(var item in Base.Leagues)
                {
                    if(item.Clubs.Count == 0)
                    {
                        string b = JsonConvert.SerializeObject(item);
                        arr.Add(b);
                    }
                }
                arr.Add("end");
                using (Stream s = File.Create(path))
                {
                    using (var ds = new DeflateStream(s, CompressionMode.Compress))
                    {
                        using (TextWriter w = new StreamWriter(ds))
                        {
                            foreach (string word in arr)
                            {
                                w.Write(word + "\n");
                            }
                        }
                    }
                }
            }
        }
        void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Base.Clubs = new List<Club>();
            Base.Players = new List<Player>();
            Base.Leagues = new List<League>();
            Base.freeClub = new Club();
            Base.freeLeague = new League();
            string freeclubs;
            List<string> listClubs = new List<string>();
            List<string> empthyLeagues = new List<string>();
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog { Filter = string.Format("EFE|*{0}", "efe"), InitialDirectory = Directory.GetCurrentDirectory()+"\\Content" };
            bool? result = fileDialog.ShowDialog();
            string filename;
            if (result == true)
            {
                filename = fileDialog.FileName;
                using (Stream s = File.OpenRead(filename))
                {
                    using (var ds = new DeflateStream(s, CompressionMode.Decompress))
                    {
                        using (TextReader r = new StreamReader(ds))
                        {
                            string temp = "";
                            while (true)
                            {
                                temp = r.ReadLine();
                                if (temp == "<freeclubs>")
                                    break;
                                listClubs.Add(temp);
                            }
                            temp = r.ReadLine();
                            freeclubs = temp;
                            while (true)
                            {
                                temp = r.ReadLine();
                                if (temp == "end")
                                    break;
                                empthyLeagues.Add(temp);
                            }
                        }
                    }
                }
                foreach (var item in listClubs)
                {
                    bool isHad = false;
                    int pos = 0;
                    Club tempC = (Club)JsonConvert.DeserializeObject(item, typeof(Club));
                    foreach (var node in Base.Clubs)
                    {
                        if (tempC.Name == node.Name)
                        {
                            isHad = true;
                            pos = Base.Clubs.IndexOf(node);
                            break;
                        }
                    }
                    if (isHad)
                    {
                        if (Base.Clubs.Count!=0) Base.Clubs.RemoveAt(pos);
                    }
                    Base.Clubs.Add(tempC);
                }
                Base.freeClub = (Club)JsonConvert.DeserializeObject(freeclubs, typeof(Club));
                foreach(var item in Base.Clubs)
                {
                    if(item.Players!=null)
                    {
                        bool isHad = false;
                        int pos = 0;
                        foreach (var node in item.Players)
                        {
                            foreach (var note in Base.Players)
                            {
                                if (note.Name == node.Name)
                                {
                                    isHad = true;
                                    pos = Base.Players.IndexOf(note);
                                    break;
                                }
                            }
                            if(isHad)
                            {
                                if (Base.Players.Count!=0) Base.Players.RemoveAt(pos);
                            }
                            node.Club = item;
                            Base.Players.Add(node);
                        }
                    }
                    if (item.League.Name != "Вне лиги")
                    {
                        bool isHad = false;
                        int pos = 0;
                        foreach (var note in Base.Leagues)
                        {
                            if (item.League.Name == note.Name)
                            {
                                isHad = true;
                                pos = Base.Leagues.IndexOf(note);
                                break;
                            }
                        }
                        if (isHad)
                        {
                            Base.Leagues.RemoveAt(pos);
                        }
                        foreach (var i in Base.Clubs)
                        {
                            if(i.League.Name == item.League.Name)
                            item.League.Clubs.Add(i);
                        }
                        Base.Leagues.Add(item.League);
                    }
                    else
                    {
                        item.League = Base.freeLeague;
                        Base.freeLeague.Clubs.Add(item);
                    }
                }
                if(Base.freeClub.Players !=null)
                {
                    bool isHad = false;
                    int pos = 0;
                    foreach (var item in Base.freeClub.Players)
                    {
                        foreach (var note in Base.Players)
                        {
                            if(note.Name == item.Name)
                            {
                                isHad = true;
                                pos = Base.Players.IndexOf(note);
                                break;
                            }
                        }
                        if(isHad)
                        {
                            if (Base.Players.Count != 0) Base.Players.RemoveAt(pos);
                        }
                        item.Club = Base.freeClub;
                        Base.Players.Add(item);
                    }
                }
                foreach(var i in empthyLeagues)
                {
                    bool isHad = false;
                    int pos = 0;
                    League tempFL = (League)JsonConvert.DeserializeObject(i, typeof(League));
                    foreach (var node in Base.Leagues)
                    {
                        if (tempFL.Name == node.Name)
                        {
                            isHad = true;
                            pos = Base.Leagues.IndexOf(node);
                            break;
                        }
                    }
                    if (isHad)
                    {
                        if (Base.Leagues.Count != 0) Base.Leagues.RemoveAt(pos);
                    }
                    Base.Leagues.Add(tempFL);
                }
            }
            Update(sender, e);
            FilterChoice.Items.Clear();
            FilterChoice.Items.Add("Фильтрация");
            CheckBox tempcheckBox = new CheckBox() { Content = Base.freeClub.Name };
            FilterChoice.Items.Add(tempcheckBox);
            foreach (var item in Base.Clubs)
            {
                CheckBox checkBox = new CheckBox() { Content = item.Name };  
                FilterChoice.Items.Add(checkBox);
            }
            FilterChoice.Items.Add("Упорядочить по возрастанию голов");
            FilterChoice.Items.Add("Упорядочить по убыванию голов");
            FilterChoice.Items.Add("Группировка по амплуа");
            FilterChoice.Items.Add("Минимальное количество голов");
            FilterChoice.Items.Add("Максимальное количество голов");
            FilterChoice.SelectedIndex = 0;
        }
        
    }
}