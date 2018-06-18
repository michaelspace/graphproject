using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using graphproject.Model;
using HorizontalAlignment = System.Windows.HorizontalAlignment;
using Label = System.Windows.Controls.Label;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace graphproject
{
    /// <summary>
    /// Interaction logic for Matrix.xaml
    /// </summary>
    public partial class Matrix : Window
    {
        private Canvas dCanvas;
        private List<Edge> krawedzie;
        private int[,] macierzWagInts;
        private string[,] macierzNazwMiast;
        private string[] macierzNazwStrings;
        private int rozmiar;

        public Matrix(int[,] wagi, string[,] macierzNazw, string[] nazwy, List<Edge> kr, Canvas d)
        {
            InitializeComponent();
            krawedzie = kr;
            dCanvas = d;
            macierzWagInts = wagi;
            macierzNazwMiast = macierzNazw;
            macierzNazwStrings = nazwy;
            rozmiar = nazwy.Length;

            if (rozmiar >1)
            {
                for (int i = 0; i <=rozmiar ; i++)
                {

                    myOutputGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    myOutputGrid.RowDefinitions.Add(new RowDefinition());
                    for (int j = 0; j <= rozmiar; j++)
                    {
                         var rectangle = new Rectangle()
                        {
                            Stroke = Brushes.Blue,
                            Fill = Brushes.Transparent
                        };
                        myOutputGrid.Children.Add(rectangle);
                        Grid.SetRow(rectangle, i);
                        Grid.SetColumn(rectangle, j);
                    }
                   

                }
                for (int i = 1; i <= rozmiar; i++)
                {
                    var label1 = new Label()
                    {
                        Content = macierzNazwStrings[i-1],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center

                    };
                    var label2 = new Label()
                    {
                        Content = macierzNazwStrings[i - 1],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center

                    };

                    myOutputGrid.Children.Add(label1);
                    Grid.SetRow(label1,0);
                    Grid.SetColumn(label1,i);

                    myOutputGrid.Children.Add(label2);
                    Grid.SetRow(label2, i);
                    Grid.SetColumn(label2, 0);


                }


                //for (int i = 0; i < rozmiar; i++)
                //{
                //    for (int j = 0; j < rozmiar; j++)
                //    {
                //        if (i==j)
                //        {
                //            macierzNazwMiast[i, j] = "-";
                //        }
                //        else if (macierzWagInts[i, j] >= 1073741822)
                //        {
                //            macierzNazwMiast[i, j] = "-";
                //        }
                //        else
                //        {
                //            string[] tmp = macierzNazwMiast[i, j].Split('.');
                //            string prev_id = tmp[tmp.Length - 2];

                //            try
                //            {
                //                int id;
                //                bool correct_id = int.TryParse(prev_id, out id);
                //                macierzNazwMiast[i, j] = macierzNazwStrings[id-1];
                //            }
                //            catch (Exception e)
                //            {
                //                Console.WriteLine(e);
                //                throw;
                //            }
                //        }
                //    }
                //}

                for (int i = 0; i < rozmiar; i++)
                {
                    for (int j = 0; j < rozmiar; j++)
                    {
                        if (i == j)
                        {
                            macierzNazwMiast[i, j] = "-";
                        }
                        else if (macierzWagInts[i, j] >= 1073741822)
                        {
                            macierzNazwMiast[i, j] = "-";
                        }
                        else
                        {
                            string[] tmp = macierzNazwMiast[i, j].Split('.');
                            int[] index = new int[tmp.Length];

                            try
                            {
                                for (int k = 0; k < tmp.Length; k++)
                                {
                                    bool correct_id = int.TryParse(tmp[k], out index[k]);
                                }
                                macierzNazwMiast[i, j] = String.Empty;
                                for (int k = 0; k < index.Length; k++)
                                {
                                    if (k != index.Length - 1)
                                    {
                                        macierzNazwMiast[i, j] = String.Concat(macierzNazwMiast[i, j],
                                            macierzNazwStrings[index[k] - 1], "-");
                                    }
                                    else
                                    {
                                        macierzNazwMiast[i, j] = String.Concat(macierzNazwMiast[i, j],
                                            macierzNazwStrings[index[k] - 1]);
                                    }
                                }
                                
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                                throw;
                            }
                        }
                    }
                }

                for (int i = 1; i <=rozmiar ; i++)
                {
                    for (int j = 1; j <=rozmiar ; j++)
                    {
                        if (i != j && macierzWagInts[i-1,j-1]<1073741822)
                        {
                            var label = new Label()
                            {
                                Content =  macierzWagInts[i - 1, j - 1],/* = macierzNazwMiast[i - 1, j - 1] + ": " +macierzWagInts[i - 1, j - 1]*/
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center
                                

                            };
                            label.MouseEnter += delegate(object sender, MouseEventArgs args) {
                                var tmp = sender as Label;
                                tmp.Background = Brushes.Chocolate;
                            };
                            label.MouseLeave += delegate(object sender, MouseEventArgs args) {
                                var tmp = sender as Label;
                                tmp.Background = Brushes.Transparent;
                            };
                            label.MouseLeftButtonDown += delegate(object s, MouseButtonEventArgs a)
                            {
                                var tmp = s as Label;
                                tmp.Background = Brushes.Gold;
                            };
                            label.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs args)
                            {
                                int row, column;
                                var lbl = sender as Label;
                                lbl.Background = Brushes.Transparent;
                                if (lbl != null)
                                {
                                    row = Grid.GetRow(lbl);
                                    column = Grid.GetColumn(lbl);
                                    string[] tmp = macierzNazwMiast[row-1, column-1].Split('-');
                                    if (tmp.Length > 1)
                                    {
                                        foreach (var krw in krawedzie)
                                        {
                                            dCanvas.Children.Remove(krw);
                                            krw.linia.Stroke = Brushes.Blue;
                                            dCanvas.Children.Add(krw);
                                        }

                                        for (int k = 0; k < tmp.Length; k++)
                                        {
                                            for (int l = 0 ; l < tmp.Length; l++)
                                            {
                                                if (k != l)
                                                {
                                                    foreach (var krw in krawedzie)
                                                    {
                                                        if ((krw.V1.nazwa == tmp[k] || krw.V2.nazwa == tmp[k]) &&
                                                            (krw.V1.nazwa == tmp[l] || krw.V2.nazwa == tmp[l]))
                                                        {
                                                            dCanvas.Children.Remove(krw);
                                                            krw.linia.Stroke = Brushes.Red;
                                                            dCanvas.Children.Add(krw);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    
                                }
                            };
                            myOutputGrid.Children.Add(label);
                            Grid.SetRow(label, i);
                            Grid.SetColumn(label, j);
                        }
                        else
                        {
                            var label = new Label()
                            {
                                Content = "-",
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center

                            };
                            myOutputGrid.Children.Add(label);
                            Grid.SetRow(label, i);
                            Grid.SetColumn(label, j);
                        }
                        
                    }
                }


            }
        }


        
    }
}