using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для ShowPages.xaml
    /// </summary>
    public partial class ShowPages : Window
    {
        int index = 0;
        int length; int selector;
        Page last, next;
        Page1 last1, next1;
        Page2 last2, next2;
        List<Club> list = new List<Club>(Base.ReadAllClubs);
        List<Player> list1 = new List<Player>(Base.ReadAllPlayers);
        List<League> list2 = new List<League>(Base.ReadAllLeagues);
        
        public ShowPages(int selector)
        {
            InitializeComponent();
            this.selector = selector;
            try
            {
                switch (selector)
                {
                    case 1: length = list1.Count - 1; break;
                    case 2: length = list.Count - 1; break;
                    case 3: length = list2.Count - 1; break;
                }
                Update();
                LoadPages();
            }
            catch { }
        }
        private void Update()
        {
            try
            {
                switch (selector)
                {
                    case 1: loo.Content = new Page1(list1[index]); break;
                    case 2: loo.Content = new Page(list[index]); break;
                    case 3: loo.Content = new Page2(list2[index]); break;
                }
            }
            catch
            {
                Content = new NoContent();
            }

        }
        private void LoadPages()
        {
            switch (selector)
            {
                case 1:
                    {
                        if (index != 0 && index != length)
                        {
                            next1 = new Page1(list1[index + 1]);
                            last1 = new Page1(list1[index - 1]);
                        }
                        else
                        {
                            if (index != 0) last1 = new Page1(list1[index - 1]);
                            else next1 = new Page1(list1[index + 1]);
                        }
                    }
                    break;
                case 2:
                    {
                        if (index != 0 && index != length)
                        {
                            next = new Page(list[index + 1]);
                            last = new Page(list[index - 1]);
                        }
                        else
                        {
                            if (index != 0) last = new Page(list[index - 1]);
                            else next = new Page(list[index + 1]);
                        }
                    }
                    break;
                case 3:
                    {
                        if (index != 0 && index != length)
                        {
                            next2 = new Page2(list2[index + 1]);
                            last2 = new Page2(list2[index - 1]);
                        }
                        else
                        {
                            if (index != 0) last2 = new Page2(list2[index - 1]);
                            else next2 = new Page2(list2[index + 1]);
                        }
                    }
                    break;
            }
        }
        private void AnotherUpdate(bool mode)
        {
            if(mode==true)
            {
                switch (selector)
                {
                    case 1: loo.Content = next1; break;
                    case 2: loo.Content = next; break;
                    case 3: loo.Content = next2; break;
                }
            }
            else
            {
                switch (selector)
                {
                    case 1: loo.Content = last1;  break;
                    case 2: loo.Content = last; break;
                    case 3: loo.Content = last2; break;
                }
            }
        }
        private async void AtStart_Click(object sender, RoutedEventArgs e)
        {
            if(index !=0)
            {
                index = 0;
                Update();
                await Task.Run(() => Dispatcher.Invoke(() => LoadPages()));
            }
        }
        private async void Previous_Click(object sender, RoutedEventArgs e)
        {
            if(index!=0)
            {
                index--;
                AnotherUpdate(false);
                await Task.Run(() => Dispatcher.Invoke(() => LoadPages()));
            }
        }
        private async void Next_Click(object sender, RoutedEventArgs e)
        {
            if(index!=length)
            {
                index++;
                AnotherUpdate(true);
                await Task.Run(() => Dispatcher.Invoke(() =>LoadPages()));
            }
        }
        private async void AtEnd_Click(object sender, RoutedEventArgs e)
        {
            if (index != length)
            {
                index = length;
                Update();
                await Task.Run(() => Dispatcher.Invoke(() => LoadPages()));
            }
        }
    }
}
