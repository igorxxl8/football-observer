using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class AddPlayerWindow : Window
    {

        
        public AddPlayerWindow()
        {
            InitializeComponent();
            foreach(var item in Base.Clubs)
            {
                AddClBox.Items.Add(item.Name);
            }
            AgeText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            NumText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            GoalsText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
            AssistText.PreviewTextInput += new TextCompositionEventHandler(TextBox_IsDigit);
        }

        void TextBox_IsDigit(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void AddPlayerButton_Click(object sender, RoutedEventArgs e)
        {
            bool error = false;
            if (string.IsNullOrEmpty(FioText.Text) || string.IsNullOrWhiteSpace(FioText.Text))
            {
                MessageBox.Show("Некорректный ввод! ФИО игрока не может быть пустой строкой или пробелом");
                error = true;
                FioText.Clear();
            }
            foreach (var item in Base.Players)
            {
                if(FioText.Text == item.Name)
                {
                    MessageBox.Show("Ошибка. Игрок с таким ФИО уже существует!");
                    error = true;
                    FioText.Clear();
                }
            }
            if (!Int32.TryParse(NumText.Text, out int number) || number > 99 || number <=0)
            {
                MessageBox.Show("Игровой номер должен быть положительным числом, не превышающим 99");
                error = true;
                NumText.Clear();
            }
            if (!Int32.TryParse(AgeText.Text, out int age) || age < 15 || age > 50) 
            {
                MessageBox.Show("Неприемлимый возраст для профессионального футболиста. \nВозраст находится в пределе от 16 до 50");
                error = true;
                AgeText.Clear();
            }
            if (!Int32.TryParse(GoalsText.Text, out int goals) || goals < 0 || goals > 1300) 
            {
                MessageBox.Show("Введенное количество голов превышает успех самого результативного игрока в истории. (введите от 0 до 1330)");
                error = true;
                GoalsText.Clear();
            }
            if (!Int32.TryParse(AssistText.Text, out int assist) || assist < 0 || assist > 2000)
            {
                MessageBox.Show("Слишком большое количество голевых передач (введите от 0 до 4000)");
                error = true;
                AssistText.Clear();
            }

            if (error)
            {
                return;
            }
            var temp = new Player(FioText.Text, goals, assist, PosText.Text, number, age, null, ImText.Text);
            if (AddClBox.Text != "Свободный агент")
            {
                int index = 0;
                foreach (var item in Base.Clubs)
                {
                    if (item.Name == AddClBox.Text)
                    {
                        temp.Club = item;
                        if(Base.Clubs[index].Players == null)
                        {
                            List<Player> list = new List<Player> { temp };
                            Base.Clubs[index].Players = list;
                        }
                        else Base.Clubs[index].Players.Add(temp);
                        break;
                    }
                    index++;
                }
                if(index==Base.Clubs.Count)
                {
                    MessageBox.Show("Такой клуб не найден. \nВыберите из списка сушествующих или Свободный агент, обозначающее что игрок не состоит в клубе!");
                    AddClText.Text="Свободный агент";
                    AddClBox.SelectedIndex = 0;
                    return;
                }
            }
            else
            {
                temp.Club = Base.freeClub;
                if (Base.freeClub.Players == null)
                {
                    List<Player> list = new List<Player> { temp };
                    Base.freeClub.Players = list;
                }
                else Base.freeClub.Players.Add(temp);
            }
            Base.Players.Add(temp);
            MessageBox.Show("Футболист добавлен");
            DialogResult = true;
        }

        private void AddLogoButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDialog = new Microsoft.Win32.OpenFileDialog{ Filter = "Все форматы|*.jpg;*.jpeg;*.gif;*.png;*.ico;*.bmp|*.JPG|*.jpg|*.JPEG|*.jpeg|*.GIF|*.gif|*.PNG|*.png|*.ICO|*.ico|*.BMP|*.bmp", InitialDirectory = "E:\\Учеба_3_семестр\\ИСП\\test2\\test2\\Content\\Изображения"};
            bool? result = fileDialog.ShowDialog();
            if (result == true)
            {
                string filename = fileDialog.FileName;
                ImText.Text = filename;
           
            }
        }
    }
}
