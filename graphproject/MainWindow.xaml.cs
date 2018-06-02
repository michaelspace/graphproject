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
        public int idWierzcholka;
        public int idKrawedzi;


        public MainWindow()
        {
            InitializeComponent();
            wierzcholek = false;
            pierwszaKrawedz = true;
            idWierzcholka = 1;
            idKrawedzi = 1;
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


                Wierzcholek w = new Wierzcholek(idWierzcholka, NazwaWierzcholka.Text, Mouse.GetPosition(this).X - 45, Mouse.GetPosition(this).Y - 45);
                wierzcholki.Add(w);
                idWierzcholka++;

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
                    foreach (var item in wierzcholki)
                    {
                        var odl = Math.Sqrt((Mouse.GetPosition(this).X - 30 - item.wspolrzednaX - 15) * (Mouse.GetPosition(this).X - 30 - item.wspolrzednaX - 15) + (Mouse.GetPosition(this).Y - 30 - item.wspolrzednaY - 15) * (Mouse.GetPosition(this).Y - 30 - item.wspolrzednaY - 15));
                        if (odl < 15 && item.wspolrzednaX != x1-15)
                        {
                            line.X2 = item.wspolrzednaX + 15 -x1;
                            line.Y2 = item.wspolrzednaY + 15 -y1;
                            item.lista.Add(new Krawedzie(idKrawedzi, Convert.ToInt32(Waga.Text)));
                            idKrawedzi++;
                        }
                    }

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

                    foreach (var item in wierzcholki)
                    {
                        var odl = Math.Sqrt((x1-item.wspolrzednaX - 15)*(x1-item.wspolrzednaX - 15) + (y1-item.wspolrzednaY - 15 )*(y1-item.wspolrzednaY - 15 ));
                        if (odl<15)
                        {
                            x1 = item.wspolrzednaX + 15;
                            y1 = item.wspolrzednaY + 15;



                            item.lista.Add(new Krawedzie(idKrawedzi, Convert.ToInt32(Waga.Text)));
                        }
                    }
                    koniecKrawedzi = true;
                }
            }
                    
        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            krawedz = true;
            this.Cursor = Cursors.Cross;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            double[,] macierz = new double[wierzcholki.Count, wierzcholki.Count];

            for (int i = 1; i < wierzcholki.Count +1; i++)
            {
                for (int j = 1; j < wierzcholki.Count +1; j++)
                {
                    if (i==j)
                    {
                        macierz[i-1, i-1] = 0;
                    }

                    else
                    {
                        foreach (var item in wierzcholki[j-1].lista)
                        {
                            for (int k = 1; k < wierzcholki.Count +1; k++)
                            {
                                if (k!=j)
                                {
                                    foreach (var item2 in wierzcholki[k-1].lista)
                                    {
                                        if (item2.id == item.id)
                                        {
                                            macierz[j-1, k-1] = item2.waga;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

        }
    }
}
