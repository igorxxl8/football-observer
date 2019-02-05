using System.ComponentModel;
using System.Windows;
using System.Windows.Data;

namespace FootballManager
{
    public class LeagueViewModel:DependencyObject
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(LeagueViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is LeagueViewModel current)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterLeague;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(LeagueViewModel), new PropertyMetadata(null));

        public LeagueViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Base.ReadAllLeagues);
            Items.Filter = FilterLeague;
        }
        private bool FilterLeague(object obj)
        {
            bool result = true;
            League current = obj as League;
            if(!string.IsNullOrWhiteSpace(FilterText) && current!=null && !current.Name.Contains(FilterText))
            {
                result = false;
            }
            return result;
        }

    }
}
