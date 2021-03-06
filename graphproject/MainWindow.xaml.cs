﻿using graphproject.Model;
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
        private Graph _graf;

        public bool Wierzcholek { get; set; }
        public bool Krawedz { get; set; }

        public int IdWierzcholka { get; set; }
        public int IdKrawedzi { get; set; }
        private Vertex ReferenceToVertex { get; set; }
        private Vertex AnotherVertex { get; set; }

        private City LosoweMiasta { get; set; }
        private string TempCity { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _graf = new Graph();

            Wierzcholek = false;
            IdWierzcholka = 0;
            IdKrawedzi = 0;
            ReferenceToVertex = null;
            AnotherVertex = null;

            LosoweMiasta = new City();
            TempCity = "Podaj Miasto";
        }
        /// <summary>
        /// Zapobiega wprowadzeniu nieprawidlowych wag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            sbyte value;
            bool isNum = sbyte.TryParse(e.Text, out value);

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

        /// <summary>
        /// Umozliwia dodanie wierzcholka, gdy limit nie zostal osiagniety
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (IdWierzcholka > 13)
            {
                MessageBox.Show("Osiągnięto limit wierzchołków!", "Uwaga", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            else
            {
                Wierzcholek = true;
                ReferenceToVertex = null;
                AnotherVertex = null;
                if (NazwaWierzcholka.Text == TempCity || LosoweMiasta.MiastaList.Contains(NazwaWierzcholka.Text))
                {
                    TempCity = LosoweMiasta.Miasta;
                    NazwaWierzcholka.Text = TempCity;

                }
                else
                {
                    TempCity = NazwaWierzcholka.Text;
                    if (!LosoweMiasta.MiastaList.Contains(TempCity))
                    {
                        LosoweMiasta.MiastaList.Add(TempCity);
                    }
                }
                this.Cursor = Cursors.Cross;
            }
        }
        /// <summary>
        /// Umozliwia dodanie krawedzi lub wierzcholka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajElementWierzcholekKrawedz(object sender, MouseButtonEventArgs e)
        {
            if (Wierzcholek)
            {

                Vertex w = new Vertex(IdWierzcholka++, NazwaWierzcholka.Text, Mouse.GetPosition(this).X - 45, Mouse.GetPosition(this).Y - 45);

                Canvas.SetLeft(w, Mouse.GetPosition(this).X - 45);
                Canvas.SetTop(w, Mouse.GetPosition(this).Y - 45);
                DrawableCanvas.Children.Add(w);
                if (!_graf.wierzcholki.Contains(w))
                    _graf.wierzcholki.Add(w);

                this.Cursor = Cursors.Arrow;
                Wierzcholek = false;



                w.MouseLeftButtonDown += delegate (object o, MouseButtonEventArgs args)
                {
                    myGrid.PreviewMouseUp += OnMouseBtnUp;

                    ReferenceToVertex = o as Vertex;

                    if (AnotherVertex == null)
                        AnotherVertex = ReferenceToVertex;

                    ReferenceToVertex.circle.Stroke = new SolidColorBrush(Colors.Red);

                    if (Krawedz)
                    {
                        this.Cursor = Cursors.Cross;
                    }
                    else
                    {
                        this.Cursor = Cursors.Hand;
                    }

                    DrawableCanvas.MouseMove += DrawableCanvasOnMouseMove;

                    if (Krawedz && AnotherVertex != ReferenceToVertex)
                    {

                        if (!AnotherVertex.sasiedzi.Contains(ReferenceToVertex))
                        {
                            var krawedz = new Edge(IdKrawedzi++, Convert.ToInt32(Waga.Text), AnotherVertex, ReferenceToVertex);

                            Canvas.SetZIndex(krawedz, -1);
                            DrawableCanvas.Children.Add(krawedz);

                            if (!_graf.krawedzie.Contains(krawedz))
                                _graf.krawedzie.Add(krawedz);



                        }

                        Krawedz = false;
                        AnotherVertex.circle.Stroke = new SolidColorBrush(Colors.BurlyWood);
                        AnotherVertex = null;
                    }
                };
            }



        }
        /// <summary>
        /// Pomocniczy event
        /// </summary>
        /// <param name="oo"></param>
        /// <param name="argss"></param>
        private void OnMouseBtnUp(object oo, MouseButtonEventArgs argss)
        {
            DrawableCanvas.MouseMove -= DrawableCanvasOnMouseMove;
            if (ReferenceToVertex != null && !Krawedz)
            {

                ReferenceToVertex.circle.Stroke = new SolidColorBrush(Colors.BurlyWood);
                this.Cursor = Cursors.Arrow;
            }

            ReferenceToVertex = null;
            myGrid.PreviewMouseUp -= OnMouseBtnUp;
        }

        /// <summary>
        /// Event do zmiany pozycji wierzcholka
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void DrawableCanvasOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            double x = mouseEventArgs.GetPosition(this).X - 45;
            double y = mouseEventArgs.GetPosition(this).Y - 45;

            if (x > 0 && x < 700 && y > 0 && y < 230)
            {
                Canvas.SetLeft(ReferenceToVertex, x);
                ReferenceToVertex.wspolrzednaX = x;
                Canvas.SetTop(ReferenceToVertex, y);
                ReferenceToVertex.wspolrzednaY = y;


                foreach (var item in _graf.krawedzie)
                {
                    if (ReferenceToVertex.id.Equals(item.V1.id))
                    {
                        item.linia.X1 = ReferenceToVertex.wspolrzednaX + 15;
                        item.linia.Y1 = ReferenceToVertex.wspolrzednaY + 15;

                        Canvas.SetLeft(item.label, (item.linia.X1 + item.linia.X2) / 2);
                        Canvas.SetTop(item.label, (item.linia.Y1 + item.linia.Y2) / 2);

                    }

                    if (ReferenceToVertex.id.Equals(item.V2.id))
                    {
                        item.linia.X2 = ReferenceToVertex.wspolrzednaX + 15;
                        item.linia.Y2 = ReferenceToVertex.wspolrzednaY + 15;


                        Canvas.SetLeft(item.label, (item.linia.X1 + item.linia.X2) / 2);
                        Canvas.SetTop(item.label, (item.linia.Y1 + item.linia.Y2) / 2);
                    }
                }
            }

        }

        /// <summary>
        /// Umozliwia dodanie krawedzi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DodajKrawedz(object sender, RoutedEventArgs e)
        {
            Krawedz = true;
            ReferenceToVertex = null;
            AnotherVertex = null;
            if (Waga.Text == "0" || Waga.Text == "00" || Waga.Text == "000" || Waga.Text == "0000")
            {
                Waga.Text = "1";
            }
            this.Cursor = Cursors.Cross;
        }

        /// <summary>
        /// Wyznacza wynikowa macierz
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WyznaczMacierz(object sender, RoutedEventArgs e)
        {
            int[,] macierzWagInts = new int[_graf.wierzcholki.Count, _graf.wierzcholki.Count];
            string[] macierzNazwStrings = new string[_graf.wierzcholki.Count];


            for (int i = 0; i < _graf.wierzcholki.Count; i++)
            {
                macierzNazwStrings[i] = _graf.wierzcholki[i].nazwa;
                for (int j = 0; j < _graf.wierzcholki.Count; j++)
                {
                    macierzWagInts[i, j] = 0;
                }
            }

            foreach (var item in _graf.krawedzie)
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

            Algorytm floydWarshall = new Algorytm();
            floydWarshall.FloydWarshall(macierzWagInts);

            Matrix outputMatrix = new Matrix(floydWarshall.MacierzWag, floydWarshall.MacierzNazw, macierzNazwStrings, _graf.krawedzie, DrawableCanvas);
            outputMatrix.ShowDialog();

        }
        /// <summary>
        /// Umozliwia resetowanie canvasa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetBtn_Click(object sender, RoutedEventArgs e)
        {
            ReferenceToVertex = null;
            AnotherVertex = null;
            IdWierzcholka = 0;
            foreach (var item in _graf.wierzcholki)
            {
                DrawableCanvas.Children.Remove(item);
            }
            foreach (var item in _graf.krawedzie)
            {
                DrawableCanvas.Children.Remove(item);
            }

            _graf = null;
            _graf = new Graph();
            LosoweMiasta = null;
            LosoweMiasta = new City();
            TempCity = "Podaj Miasto";
            NazwaWierzcholka.Text = TempCity;
        }
        /// <summary>
        /// Messagebox z autorami
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onAutorzy_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("System najkrótszych połączeń kolejowych między wybranymi miejscowościami w oparciu o algorytm Floyda-Warshalla. Autorzy projektu: \n\n- Michał Kocisz\n\n- Kamil Paździorek\n\n- Grzegorz Jarząbek\n\n\u00a9 2018 Grafy i Sieci", "Autorzy",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
