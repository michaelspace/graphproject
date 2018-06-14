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
      //  public List<Vertex> wierzcholki = new List<Vertex>();
     //   public List<Edge> krawedzie = new List<Edge>();
        public int idWierzcholka;
        public int idKrawedzi;
        private Vertex referenceToVertex;
        private Vertex anotherVertex;

        private City losoweMiasta;
        private string tempCity;

        public MainWindow()
        {
            InitializeComponent();
            graf = new Graph();

            wierzcholek = false;
            pierwszaKrawedz = true;
            idWierzcholka = 0;
            idKrawedzi = 1;
            referenceToVertex = null;
            anotherVertex = null;

            losoweMiasta = new City();
            tempCity = "Podaj Miasto";
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            sbyte value;
            bool isNum = sbyte.TryParse(e.Text, out value);
            Console.WriteLine(isNum);
            var text = sender as TextBox;
            try
            {
                if (text.Text.Length == 0 && e.Text == "0")
                    isNum = false;

                if (text.Text.Length > 3)
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
            if (idWierzcholka >13)
            {
                MessageBox.Show("Osiągnięto limit wierzchołków!", "Uwaga", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                wierzcholek = true;
                referenceToVertex = null;
                anotherVertex = null;
                if (NazwaWierzcholka.Text == tempCity)
                {
                    tempCity = losoweMiasta.Miasta;
                    NazwaWierzcholka.Text = tempCity;

                }

                this.Cursor = Cursors.Cross;
            }
        }

        private void DodajWierzcholek(object sender, MouseButtonEventArgs e)
        {
          //  var canvas = canvas;
            if (wierzcholek)
            {

                Vertex w = new Vertex(idWierzcholka++, NazwaWierzcholka.Text, Mouse.GetPosition(this).X - 45, Mouse.GetPosition(this).Y - 45);

                Canvas.SetLeft(w, Mouse.GetPosition(this).X - 45);
                Canvas.SetTop(w, Mouse.GetPosition(this).Y - 45);
                DrawableCanvas.Children.Add(w);
                if(!graf.wierzcholki.Contains(w))
                    graf.wierzcholki.Add(w);
                
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
                        anotherVertex.circle.Stroke = new SolidColorBrush(Colors.BurlyWood);
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

               


            foreach (var item in graf.krawedzie)
            {
                if (referenceToVertex.id.Equals(item.V1.id))
                {
                   // removeOldEdges.Add(item);
                   // DrawableCanvas.Children.Remove(item);
                    item.linia.X1 = referenceToVertex.wspolrzednaX+15;
                    item.linia.Y1 = referenceToVertex.wspolrzednaY+15;

                    Canvas.SetLeft(item.label, (item.linia.X1 + item.linia.X2) / 2);
                    Canvas.SetTop(item.label, (item.linia.Y1 + item.linia.Y2) / 2);

                }
                if (referenceToVertex.id.Equals(item.V2.id))
                {
                  //  removeOldEdges.Add(item);
                    // DrawableCanvas.Children.Remove(item);
                    item.linia.X2 = referenceToVertex.wspolrzednaX+15;
                    item.linia.Y2 = referenceToVertex.wspolrzednaY+15;


                    Canvas.SetLeft(item.label, (item.linia.X1 + item.linia.X2) / 2);
                    Canvas.SetTop(item.label, (item.linia.Y1 + item.linia.Y2) / 2);
                }
            }

        }


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            krawedz = true;
            referenceToVertex = null;
            anotherVertex = null;
            if (Waga.Text == "0" || Waga.Text == "00" || Waga.Text == "000" || Waga.Text == "0000")
            {
                Waga.Text = "1";
            }
            this.Cursor = Cursors.Cross;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int[,] macierzWagInts = new int[graf.wierzcholki.Count, graf.wierzcholki.Count];
            string[] macierzNazwStrings = new string[graf.wierzcholki.Count];


            //foreach (var item in graf.krawedzie)
            //{
            //    Console.WriteLine(item.V1.nazwa + " id: " + item.V1.id + "; " + item.V2.nazwa + " id: " + item.V2.id + "; ");
            //}

           for (int i = 0; i < graf.wierzcholki.Count; i++)
            {
                macierzNazwStrings[i] = graf.wierzcholki[i].nazwa;
                for (int j = 0; j < graf.wierzcholki.Count; j++)
                {
                    macierzWagInts[i, j] = 0;
                }
            }

            foreach (var item in graf.krawedzie)
            {
                macierzWagInts[item.V1.id, item.V2.id] = item.waga;
                macierzWagInts[item.V2.id, item.V1.id] = item.waga;
            }

            // Wyświetlanie macierzy wejściowej
            //for (int i = 0; i < graf.wierzcholki.Count; i++)
            //{
            //    Console.Write(macierzNazwStrings[i] + " | ");
            //}

            //Console.WriteLine();
            //for (int i = 0; i < graf.wierzcholki.Count; i++)
            //{
            //    for (int j = 0; j < graf.wierzcholki.Count; j++)
            //    {
            //        if (j == 0)
            //        {
            //            Console.Write(macierzNazwStrings[i] + " | ");
            //        }
            //        Console.Write(macierzWagDoubles[i,j] + " | ");
            //    }

            //    Console.WriteLine();
            //}

            Algorytm FloydWarshall = new Algorytm();
            FloydWarshall.FloydWarshall(macierzWagInts);

            Matrix outputMatrix = new Matrix(FloydWarshall.MacierzWag, FloydWarshall.MacierzNazw, macierzNazwStrings);
            outputMatrix.ShowDialog();

        }

        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            referenceToVertex = null;
            anotherVertex = null;
            foreach (var item in graf.wierzcholki)
            {
                DrawableCanvas.Children.Remove(item);
            }
            foreach (var item in graf.krawedzie)
            {
                DrawableCanvas.Children.Remove(item);
            }

            graf = null;
            graf = new Graph();
            losoweMiasta = null;
            losoweMiasta = new City();
            tempCity = "Podaj Miasto";
            NazwaWierzcholka.Text = tempCity;
        }

        private void onAutorzy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System najkrótszych połączeń kolejowych między wybranymi miejscowościami w oparciu o algorytm Floyda-Warshalla. Autorzy projektu: \n\n- Michał Kocisz\n\n- Kamil Paździorek\n\n- Grzegorz Jarząbek\n\n\u00a9 2018 SGGW - Grafy i Sieci", "Autorzy",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
