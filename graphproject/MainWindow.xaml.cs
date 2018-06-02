using graphproject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace graphproject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool wierzcholek;
        public bool krawedz;
        public bool koniecKrawedzi;
        public bool pierwszaKrawedz;
        public double x1;
        public double y1;
        public List<Wierzcholek> wierzcholki = new List<Wierzcholek>();
        public int id;


        public MainWindow()
        {
            InitializeComponent();
            wierzcholek = false;
            pierwszaKrawedz = true;
            id = 1;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wierzcholek = true;
            this.Cursor = Cursors.Cross;
        }

        private void DodajWierzcholek(object sender, MouseButtonEventArgs e)
        {
            if (wierzcholek)
            {
                Ellipse circle = new Ellipse();
                circle.Width = 30;
                circle.Height = 30;
                circle.Stroke = new SolidColorBrush(Colors.Black);
                circle.StrokeThickness = 1;

                Label label = new Label();
                label.Content = NazwaWierzcholka.Text;


                Canvas.SetLeft(label, Mouse.GetPosition(this).X - 45);
                Canvas.SetTop(label, Mouse.GetPosition(this).Y - 45);
                circle.Fill = new SolidColorBrush(Colors.Blue);
                Canvas.SetLeft(circle, Mouse.GetPosition(this).X - 45);
                Canvas.SetTop(circle, Mouse.GetPosition(this).Y - 45);
                canvas.Children.Add(circle);
                canvas.Children.Add(label);


                Wierzcholek w = new Wierzcholek(id, NazwaWierzcholka.Text, Mouse.GetPosition(this).X - 45, Mouse.GetPosition(this).Y - 45);
                wierzcholki.Add(w);
                id++;

                this.Cursor = Cursors.AppStarting;
                wierzcholek = false;
            }

            if (krawedz)
            {
                Line line = new Line();
                line.Stroke = Brushes.Red;
                line.StrokeThickness = 4;

                if (koniecKrawedzi)
                {
                    line.X2 = Mouse.GetPosition(this).X - x1 - 30;
                    line.Y2 = Mouse.GetPosition(this).Y - y1 - 30;
                    koniecKrawedzi = false;
                    krawedz = false;
                    Canvas.SetLeft(line, x1);
                    Canvas.SetTop(line, y1);
                    canvas.Children.Add(line);
                    this.Cursor = Cursors.AppStarting;
                }
                else
                {
                    x1 = Mouse.GetPosition(this).X - 30;
                    y1 = Mouse.GetPosition(this).Y - 30;
                    koniecKrawedzi = true;
                }
            }
                    
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            krawedz = true;
            this.Cursor = Cursors.Cross;
        }
    }
}
