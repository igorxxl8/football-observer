using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FootballManager
{
    /// <summary>
    /// Логика взаимодействия для Diagram.xaml
    /// </summary>
    public partial class Diagram : Window
    {
        List<Player> list = new List<Player>(Base.ReadAllPlayers.OrderByDescending(i => i.Goals));
        double radius;

        public Diagram()
        {
            InitializeComponent();
            Slider.Maximum = list.Count();
            Sel.PreviewTextInput += TextBox_PreviewTextInput;
            SizeChanged += Diagram_SizeChanged;
        }

        private void Diagram_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawDiagram();
        }

        void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))  e.Handled = true;
            if (!Int32.TryParse(e.Text, out int value)
                && value > list.Count) e.Handled = true;

        }
     
        private void Sel_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            if (Sel.Text!="" && Sel.Text != "0") DrawDiagram();
        }
        private void DrawDiagram()
        {
            Pan.Children.Clear();
            container.Children.Clear();
            TextBlock textBlock = new TextBlock { Text = "Обозначения:" };
            Pan.Children.Add(textBlock);
            Int32.TryParse(Sel.Text, out int count);
            var player = list.Take(count);
            var sum = player.Sum(i => i.Goals);
            var angles = player.Select(i => i.Goals * 2.0 * Math.PI / sum);
            var text = player.Select(i => i.Name + " (" + i.Club.Name + ")");
            var textList = text.ToList();
            radius = ActualHeight / 4;
            var startAngle = 0.0;
            var centerPoint = new Point(radius, radius);
            var syRadius = new Size(radius, radius);
            Random rand = new Random();
            if (angles.Count() == 1) angles = player.Select(i => 1.999999 * Math.PI);
            int index = 0;
            foreach (var angle in angles)
            {
                byte[] mas = new byte[3];
                rand.NextBytes(mas);
                Color color = new Color { A = mas[0], B = mas[1], G = mas[2] };
                BrushConverter brushConv = new BrushConverter();
                Brush brush = (Brush)brushConv.ConvertFrom(color.ToString());
                var endAngle = startAngle + angle;
                var startPoint = centerPoint;
                startPoint.Offset(radius * Math.Cos(startAngle), radius * Math.Sin(startAngle));
                var endPoint = centerPoint;
                endPoint.Offset(radius * Math.Cos(endAngle), radius * Math.Sin(endAngle));
                var angleDeg = angle * 180.0 / Math.PI;
                Path p = new Path()
                {
                    Stroke = brush,
                    Fill = brush,
                    Data = new PathGeometry(
                        new PathFigure[]
                        {
                            new PathFigure(
                                centerPoint,
                                new PathSegment[]
                                {
                                    new LineSegment(startPoint, isStroked:true),
                                    new ArcSegment(endPoint, syRadius,
                                    angleDeg, angleDeg > 180,
                                    SweepDirection.Clockwise, isStroked:true)
                                },
                                closed:true)
                        })
                };
                container.Children.Add(p);
                startAngle = endAngle;
                StackPanel stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
                Rectangle rectangle = new Rectangle { Height = 20, Width = 20, Fill = brush };
                TextBlock text1 = new TextBlock { Text = " - " + textList[index] };
                stackPanel.Children.Add(rectangle);
                stackPanel.Children.Add(text1);
                Pan.Children.Add(stackPanel);
                index++;
            }
        }
        private void MoreValue_Click(object sender, RoutedEventArgs e)
        {
            if (Int32.TryParse(Sel.Text, out int value) && value != list.Count)
            {
                value++;
                Sel.Text = value.ToString();
            }
        }
        private void LessValue_Click(object sender, RoutedEventArgs e)
        {
            if(Int32.TryParse(Sel.Text, out int value) && value !=1 && value != 0)
            {
                value--;
                Sel.Text = value.ToString();
            }
        }
        
    }
}
