using graphproject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private Graph graf;

        public bool wierzcholek;
        public bool krawedz;
        public bool koniecKrawedzi;
        public bool pierwszaKrawedz;
        public double x1;
        public double y1;
        public List<Vertex> wierzcholki = new List<Vertex>();
        public List<Edge> krawedzie = new List<Edge>();
        public int idWierzcholka;
        public int idKrawedzi;
        private Vertex referenceToVertex;
        private Vertex anotherVertex;

        public MainWindow()
        {
            InitializeComponent();
            graf = new Graph();

            wierzcholek = false;
            pierwszaKrawedz = true;
            idWierzcholka = 1;
            idKrawedzi = 1;
            referenceToVertex = null;
            anotherVertex = null;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            sbyte value;
            bool isNum = sbyte.TryParse(e.Text, out value);
            Console.WriteLine(isNum);
            var text = sender as TextBox;
            try
            {
                if (text.Text.Length == 0 && e.Text == "-") isNum = true;
                if (text.Text.Length!=0 && 
                    ((text.Text[0] == '-' && text.Text.Length > 3) || (text.Text[0] != '-' && text.Text.Length > 2)))
                    isNum = false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            
            e.Handled = !isNum;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            wierzcholek = true;
            this.Cursor = Cursors.Cross;
        }

        private void DodajWierzcholek(object sender, MouseButtonEventArgs e)
        {
          //  var canvas = canvas;
            if (wierzcholek)
            {
               
                idWierzcholka++;

                Vertex w = new Vertex(idWierzcholka, NazwaWierzcholka.Text, Mouse.GetPosition(this).X - 45, Mouse.GetPosition(this).Y - 45);

                Canvas.SetLeft(w, Mouse.GetPosition(this).X - 45);
                Canvas.SetTop(w, Mouse.GetPosition(this).Y - 45);
                DrawableCanvas.Children.Add(w);
                if(!graf.wierzcholki.Contains(w))
                    graf.wierzcholki.Add(w);

                
                wierzcholki.Add(w);
                this.Cursor = Cursors.AppStarting;
                wierzcholek = false;

                

                w.MouseLeftButtonDown += delegate(object o, MouseButtonEventArgs args)
                {
                    referenceToVertex = o as Vertex;

                    if (anotherVertex == null)
                        anotherVertex = referenceToVertex;

                    referenceToVertex.circle.Stroke = new SolidColorBrush(Colors.Red);

                    this.Cursor = Cursors.Hand;
                    DrawableCanvas.MouseMove += DrawableCanvasOnMouseMove;

                    if (krawedz  && anotherVertex != referenceToVertex)
                    {

                        if (!anotherVertex.sasiedzi.Contains(referenceToVertex))
                        {
                            var krawedz = new Edge(idKrawedzi++, Convert.ToInt32(Waga.Text), anotherVertex, referenceToVertex);

                            Canvas.SetZIndex(krawedz, -1);
                            DrawableCanvas.Children.Add(krawedz);
                            
                            if (!graf.krawedzie.Contains(krawedz))
                                graf.krawedzie.Add(krawedz);

                            this.Cursor = Cursors.AppStarting;
                            
                        }
                        
                        krawedz = false;
                        anotherVertex = null;
                    }
                };

                w.MouseLeftButtonUp += delegate (object o, MouseButtonEventArgs args)
                {
                    DrawableCanvas.MouseMove -= DrawableCanvasOnMouseMove;
                    this.Cursor = Cursors.AppStarting;
                    if(referenceToVertex!=null && !krawedz)
                        referenceToVertex.circle.Stroke = new SolidColorBrush(Colors.BurlyWood);
                    referenceToVertex = null;
                };
            }

            
                    
        }

        private void DrawableCanvasOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            
                Canvas.SetLeft(referenceToVertex, mouseEventArgs.GetPosition(this).X - 45);
                referenceToVertex.wspolrzednaX = mouseEventArgs.GetPosition(this).X - 45;
                Canvas.SetTop(referenceToVertex, mouseEventArgs.GetPosition(this).Y - 45);
                referenceToVertex.wspolrzednaY = mouseEventArgs.GetPosition(this).Y - 45;
            var removeOldEdges = new List<Edge>();

            foreach (var item in graf.krawedzie)
            {
                if (referenceToVertex.id.Equals(item.V1.id))
                {
                   // removeOldEdges.Add(item);
                   // DrawableCanvas.Children.Remove(item);
                    item.linia.X1 = referenceToVertex.wspolrzednaX+15;
                    item.linia.Y1 = referenceToVertex.wspolrzednaY+15;



                }
                if (referenceToVertex.id.Equals(item.V2.id))
                {
                  //  removeOldEdges.Add(item);
                    // DrawableCanvas.Children.Remove(item);
                    item.linia.X2 = referenceToVertex.wspolrzednaX+15;
                    item.linia.Y2 = referenceToVertex.wspolrzednaY+15;



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
                        foreach (var item in wierzcholki[j-1].krawedzie)
                        {
                            for (int k = 1; k < wierzcholki.Count +1; k++)
                            {
                                if (k!=j)
                                {
                                    foreach (var item2 in wierzcholki[k-1].krawedzie)
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
