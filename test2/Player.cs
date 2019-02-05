using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace FootballManager
{
    [JsonObject(IsReference =true)]
    public class Player : INotifyPropertyChanged, IDataErrorInfo
    {

        private int age;
        private int number;
        private int goals;
        private int assist;
        private string fIO;
        private string position;
        private string photoPath;
        private Club club;

        public event PropertyChangedEventHandler PropertyChanged;
        private void SetProperty<T>(ref T field, T value, string name)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public string Name
        {
            get => fIO;
            set => SetProperty(ref fIO, value, "Name");
        }
        public int Goals
        {
            get => goals;
            set => SetProperty(ref goals, value, "Goals"); 
        }
        public int Assist
        {
            get => assist;
            set => SetProperty(ref assist, value, "Assist"); 
        }
        public string Position
        {
            get => position;
            set => SetProperty(ref position, value, "Position");
        }
        public int Age
        {
            get => age;
            set => SetProperty(ref age, value, "Age"); 
        }

        [JsonIgnore]
        public Club Club
        {
            get => club;
            set => SetProperty(ref club, value, "Club");
        }
        public int Number
        {
            get => number;
            set => SetProperty(ref number, value, "Number");
        }

        
        public string PhotoPath
        {
            get => photoPath;
            set => SetProperty(ref photoPath, value, "PhotoPath");
        }
        [JsonIgnore]
        public string ErrorAge { get; private set; }

        [JsonIgnore]
        public string ErrorNum { get; private set; }

        [JsonIgnore]
        public string ErrorGo { get; private set; }

        [JsonIgnore]
        public string ErrorAs { get; private set; }

        [JsonIgnore]
        public string ErrorName { get; private set; }

        [JsonIgnore]
        public string Error { get => null; }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Name":
                        {
                            ErrorName= string.IsNullOrEmpty(Name) ? "ФИО не может быть пустым" : null;
                            return ErrorName;
                        }
                    case "Goals":
                        {
                            ErrorGo =  (goals < 0 || goals > 1330) ? "Неправильное количество голов. Введенное количество голов превышает успех самого результативного игрока в истории. (введите от 0 до 1330)" : null;
                            return ErrorGo;
                        }
                    case "Assist":
                        {
                            ErrorAs =  (assist < 0 || assist > 2000) ? "Неправильное количество голевых передач. Слишком большое количество голевых передач (введите от 0 до 4000" : null;
                            return ErrorAs;
                        }
                    case "Age":
                        {
                           ErrorAge = (age < 15 || age > 50) ? "Неправильный возраст для профессионального футболиста (введите от 15 до 50)" : null;
                            return ErrorAge;
                        }
                    case "Number":
                        {
                            ErrorNum = (number < 0 || number > 99) ? "Неправильный номер игрока (введите от 0 до 99)" : null;
                            return ErrorNum;
                        }
                }
                return null;
            }
        }

        public Player() { }
        public Player(string _FIO, int _goals, int _assist, string _position, int _gameNumber, int _age, Club _club, string _photoPath)
        {
            Name = _FIO;
            Goals = _goals;
            Assist = _assist;
            Position = _position;
            Number = _gameNumber;
            Age = _age;
            Club = _club;
            PhotoPath = _photoPath;
        }
        
    }
}
