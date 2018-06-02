using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphproject
{
    class Algorytm
    {
        int[,] graph_matrix = { { 0, 2, 1, 0, 0, 0},
                                    {2, 0, 0, 2, 0, 11},
                                    {1, 0, 0, 2, 0, 0},
                                    {0, 2, 2, 0, 3, 0},
                                    {0, 0, 0, 3, 0, 0},
                                    {0, 11, 0, 0, 0, 0} };

        const int inf = int.MaxValue / 2 - 1; //infinity div 2

        static void FloydWarshall(int[,] graph_matrix)
        {
            int n = graph_matrix.GetLength(0);

            //shortest distance between every pair of vertices
            int[,] dist = new int[n, n];

            //"next" array remembers course of the path between vertices and finally we'll reconstruct path
            int[,] next = new int[n, n];

            for (int i = 0; i < n; i++) //initial part of the algorithm
            {
                for (int j = 0; j < n; j++)
                {
                    if (graph_matrix[i, j] != 0) dist[i, j] = graph_matrix[i, j];
                    else dist[i, j] = inf; //inf value when there isn't edge between "i" and "j" vertices

                    next[i, j] = j;
                }
            }

            for (int k = 0; k < n; k++) //main part of the algorithm
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (dist[i, j] > dist[i, k] + dist[k, j])
                        {
                            dist[i, j] = dist[i, k] + dist[k, j];
                            next[i, j] = next[i, k];

                            if (i == j) dist[i, j] = 0;
                        }
                    }
                }
            }

            string[,] paths = new string[n, n]; //matrix with paths reconstructed

            for (int i = 0; i < n; i++) //reconstructing paths for every pair of the vertices
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j) paths[i, j] = "null"; //path from A to A vertice is empty
                    else paths[i, j] = Path(next, i, j);
                }
            }

            Console.WriteLine("----------DISTANCE----------");
            Display<int>(dist);
            Console.WriteLine("------------PATH------------");
            Display<string>(paths);
        }

        static string Path(int[,] next, int u, int v)
        {
            string path = Convert.ToString(u); //first vertice in the path

            while (u != v)
            {
                u = next[u, v];
                path = path + "." + Convert.ToString(u); //reconstruction
            }

            string[] path2 = path.Split('.');
            string path3 = "";

            //add 1 to every vertice because in C# we count array index starting from 0
            for (int i = 0; i < path2.Length; i++)
            {
                int ver = Convert.ToInt32(path2[i]);
                ver++; //adding 1 to vertice
                path3 = path3 + Convert.ToString(ver) + ".";
            }
            path3 = path3.Remove(path3.Length - 1, 1);
            return path3;
        }

        static void Display<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + ", ");
                }
                Console.WriteLine();
            }
        }
    }
}
