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
        int[,] graph_matrix = { { 0, 1, 2, 5, 3},
                                    {1, 0, 0, 1, 0},
                                    {2, 0, 0, 3, 0},
                                    {5, 1, 3, 0, 0},
                                    {3, 0, 0, 0, 0} };

        public MainWindow()
        {
            InitializeComponent();

            Floyd_Warshall(graph_matrix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Cross;
        }

        private void DodajWierzcholek(object sender, MouseButtonEventArgs e)
        {
            if (this.Cursor == Cursors.Cross)
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

                this.Cursor = Cursors.AppStarting;
            }
                    
        }

        const int inf = int.MaxValue / 2 - 1; //infinity div 2

        static void Floyd_Warshall(int[,] graph_matrix)
        {
            // "distance" array contains lenght of the shortest path between every pair of vertices
            // "prev" array contains vertices (separated by dots) which are in particular path


            int n = graph_matrix.GetLength(0);
            n++; //algorithm works on matrix which size is n+1

            int[,] matrix = new int[n, n];

            for (int i = 0; i < n; i++) matrix[i, 0] = 0;
            for (int j = 0; j < n; j++) matrix[0, j] = 0;

            for (int i = 1; i < n; i++) //extending matrix to n+1 size
            {
                for (int j = 1; j < n; j++)
                {
                    matrix[i, j] = graph_matrix[i - 1, j - 1];
                }
            }

            int[,,] dist = new int[n, n, n]; //shortest distance between every pair of vertices
            string[,] prev = new string[n, n]; //course of the shortest path between vertices

            for (int i = 1; i < n; i++) //initial part of the algorithm
            {
                for (int j = 1; j < n; j++)
                {
                    if (matrix[i, j] != 0)
                    {
                        dist[i, j, 0] = matrix[i, j];
                        prev[i, j] = Convert.ToString(i)+'.'; //vertices in path are separated by dots
                    }

                    else
                    {
                        dist[i, j, 0] = inf;
                        prev[i, j] = Convert.ToString(i)+'.';
                    }
                }
            }

            for (int k = 1; k < n; k++) //main part of the algorithm
            {
                for (int i = 1; i < n; i++)
                {
                    for (int j = 1; j < n; j++)
                    {
                        if (dist[i, k, k - 1] + dist[k, j, k - 1] < dist[i, j, k - 1])
                        {
                            dist[i, j, k] = dist[i, k, k - 1] + dist[k, j, k - 1];
                            if (i == j) prev[i, j] = "null";
                            else prev[i, j] = prev[i, j] + Convert.ToString(k)+'.';
                        }
                        else dist[i, j, k] = dist[i, j, k - 1];
                    }
                }
            }

            int[,] distance = new int[n, n];

            for (int i = 0; i < n; i++) //converting 3-dimension "dist" matrix to 2-dimension "distance" matrix with constant k = size of matrix
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) distance[i, j] = 0;
                    else distance[i, j] = dist[i, j, n - 1]; //n-1
                }
            }

            for (int i = 1; i < n; i++) //adding last vertice to path
            {
                for (int j = 1; j < n; j++)
                {
                    if (prev[i, j] != "null") prev[i, j] = prev[i, j] + Convert.ToString(j);
                }
            }

            Console.WriteLine("-----DISTANCE-----");
            Display<int>(distance);
            Console.WriteLine("-----PATH-----");
            Display<string>(prev);
        }

        static void Display<T>(T[,] matrix)
        {
            for (int i = 1; i < matrix.GetLength(0); i++)
            {
                for (int j = 1; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }

    }
}
