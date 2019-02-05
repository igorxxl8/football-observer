using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using Newtonsoft.Json;

namespace FootballManager
{
    public class Club:INotifyPropertyChanged, IDataErrorInfo
    {
        private string name;
        private string coach;
        private string logoPath;
        private League league;
        private List<Player> players = new List<Player>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set { SetProperty(ref name, value, "Name"); }
        }
        public string Coach
        {
            get => coach;
            set { SetProperty(ref coach, value, "Coach"); }
        }
        public List<Player> Players
        {
            get => players;
            set { SetProperty(ref players, value, "Players"); }
        }
        
        [JsonIgnore]
        public string Error
        {
            get { return null; }
        }
        
        public string LogoPath { get => logoPath; set => logoPath = value; }

        [JsonIgnore]
        public string ErrorName { get; private set; }

        [JsonIgnore]
        public string ErrorCo { get; private set; }
        public League League
        {
            get => league;
            set { SetProperty(ref league, value, "League"); }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        {
                            ErrorName =  string.IsNullOrEmpty(Name) ? "Название не может быть пустым" : null;
                            return ErrorName;
                        }
                    case "Coach":
                        {
                            ErrorCo = string.IsNullOrEmpty(Coach) ? "Поле тренер не может быть пустым" : null;
                            return ErrorCo;
                        }
                    case "Players":return null;
                }
                return null;
            }
        }

        private void SetProperty<T>(ref T field, T value, string name)
        {
            if(!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public Club() { }
        public Club(string _name, string _coach, List<Player> _players, League _league, Image _logo, string logoPath)
        {
            Name = _name;
            Coach = _coach;
            Players = _players;
            LogoPath = logoPath;
            League = _league;
        }
        public Club(Club item)
        {
            Name = item.Name;
            Coach = item.Coach;
            Players = item.Players;
            LogoPath = item.LogoPath;
        }
    }
}
