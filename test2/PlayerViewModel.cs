using System.Windows;
using System.ComponentModel;
using System.Windows.Data;

namespace FootballManager
{
    public class PlayerViewModel: DependencyObject
    {
        public string FilterText
        {
            get { return (string)GetValue(FilterTextProperty); }
            set { SetValue(FilterTextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterText.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextProperty =
            DependencyProperty.Register("FilterText", typeof(string), typeof(PlayerViewModel), new PropertyMetadata("", FilterText_Changed));

        private static void FilterText_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlayerViewModel current)
            {
                current.Items.Filter = null;
                current.Items.Filter = current.FilterPlayer;
            }
        }

        public ICollectionView Items
        {
            get { return (ICollectionView)GetValue(ItemsProperty); }
            set { SetValue(ItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Items.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemsProperty =
            DependencyProperty.Register("Items", typeof(ICollectionView), typeof(PlayerViewModel), new PropertyMetadata(null));


        public PlayerViewModel()
        {
            Items = CollectionViewSource.GetDefaultView(Base.ReadAllPlayers);
            Items.Filter = FilterPlayer;
        }

        private bool FilterPlayer(object obj)
        {
            bool result = true;
            Player current = obj as Player;
            if(!string.IsNullOrWhiteSpace(FilterText) && current !=null && !current.Name.Contains(FilterText))
            {
                result = false;
            }
            return result;
        }
    }
}
