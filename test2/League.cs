using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace FootballManager
{
    [JsonObject(IsReference = true)]
    public class League:INotifyPropertyChanged, IDataErrorInfo
    {
        private string name;
        private string country;
        private string logoPath;
        private List<Club> clubs = new List<Club>();

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value, "Name");
        }
        public string Country
        {
            get => country;
            set => SetProperty(ref country, value, "Country");
        }

        [JsonIgnore]
        public List<Club> Clubs
        {
            get => clubs;
            set => SetProperty(ref clubs, value, "Clubs");
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

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        {
                            ErrorName = string.IsNullOrEmpty(Name) ? "Название не может быть пустым" : null;
                            return ErrorName;
                        }
                    case "Country":
                        {
                            ErrorCo = string.IsNullOrEmpty(Country) ? "Поле страна не может быть пустым" : null;
                            return ErrorCo;
                        }
                }
                return null;
            }
        }

        private void SetProperty<T>(ref T field, T value, string name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public League() { }
        public League(string _name, string _country, List<Club> _clubs, string _logoPath)
        {
            Name = _name;
            Country = _country;
            Clubs = _clubs;
            LogoPath = _logoPath;           
        }

    }
}