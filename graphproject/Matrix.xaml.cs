using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private readonly Canvas _dCanvas;
        private readonly List<Edge> _krawedzie;
        private readonly int[,] _macierzWagInts;
        private readonly string[,] _macierzNazwMiast;

        public string[] MacierzNazwStrings { get; set; }

        public int Rozmiar { get; }

        public string[,] MacierzNazwMiast => _macierzNazwMiast;

        public int[,] MacierzWagInts => _macierzWagInts;

        public List<Edge> Krawedzie => _krawedzie;

        public Canvas DCanvas => _dCanvas;

        public Matrix(int[,] wagi, string[,] macierzNazw, string[] nazwy, List<Edge> kr, Canvas d)
        {
            InitializeComponent();
            _krawedzie = kr;
            _dCanvas = d;
            _macierzWagInts = wagi;
            _macierzNazwMiast = macierzNazw;
            MacierzNazwStrings = nazwy;
            Rozmiar = nazwy.Length;

            this.InitializeWindow();
        }

        /// <summary>
        /// This method initializes the matrix window. Adds the rows, columns, routes with some events.
        /// </summary>
        private void InitializeWindow()
        {
            if (Rozmiar > 1)
            {
                for (int i = 0; i <= Rozmiar; i++)
                {

                    myOutputGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    myOutputGrid.RowDefinitions.Add(new RowDefinition());
                    for (int j = 0; j <= Rozmiar; j++)
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
                for (int i = 1; i <= Rozmiar; i++)
                {
                    var label1 = new Label()
                    {
                        Content = MacierzNazwStrings[i - 1],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center

                    };
                    var label2 = new Label()
                    {
                        Content = MacierzNazwStrings[i - 1],
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center

                    };

                    myOutputGrid.Children.Add(label1);
                    Grid.SetRow(label1, 0);
                    Grid.SetColumn(label1, i);

                    myOutputGrid.Children.Add(label2);
                    Grid.SetRow(label2, i);
                    Grid.SetColumn(label2, 0);


                }

                for (int i = 0; i < Rozmiar; i++)
                {
                    for (int j = 0; j < Rozmiar; j++)
                    {
                        if (i == j)
                        {
                            _macierzNazwMiast[i, j] = "-";
                        }
                        else if (_macierzWagInts[i, j] >= 1073741822)
                        {
                            _macierzNazwMiast[i, j] = "-";
                        }
                        else
                        {
                            string[] tmp = _macierzNazwMiast[i, j].Split('.');
                            int[] index = new int[tmp.Length];

                            try
                            {
                                for (int k = 0; k < tmp.Length; k++)
                                {
                                    bool correctId = int.TryParse(tmp[k], out index[k]);
                                }
                                _macierzNazwMiast[i, j] = String.Empty;
                                for (int k = 0; k < index.Length; k++)
                                {
                                    if (k != index.Length - 1)
                                    {
                                        _macierzNazwMiast[i, j] = String.Concat(_macierzNazwMiast[i, j],
                                            MacierzNazwStrings[index[k] - 1], "-");
                                    }
                                    else
                                    {
                                        _macierzNazwMiast[i, j] = String.Concat(_macierzNazwMiast[i, j],
                                            MacierzNazwStrings[index[k] - 1]);
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

                for (int i = 1; i <= Rozmiar; i++)
                {
                    for (int j = 1; j <= Rozmiar; j++)
                    {
                        if (i != j && _macierzWagInts[i - 1, j - 1] < 1073741822)
                        {
                            var label = new Label()
                            {
                                Content = _macierzWagInts[i - 1, j - 1],
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center

                            };
                            label.MouseEnter += delegate (object sender, MouseEventArgs args)
                            {
                                var tmp = sender as Label;
                                tmp.Background = Brushes.Chocolate;
                            };
                            label.MouseLeave += delegate (object sender, MouseEventArgs args)
                            {
                                var tmp = sender as Label;
                                tmp.Background = Brushes.Transparent;
                            };
                            label.MouseLeftButtonDown += delegate (object s, MouseButtonEventArgs a)
                            {
                                var tmp = s as Label;
                                tmp.Background = Brushes.Gold;
                            };
                            label.MouseLeftButtonUp += delegate (object sender, MouseButtonEventArgs args)
                            {
                                var lbl = sender as Label;
                                lbl.Background = Brushes.Transparent;

                                int row = Grid.GetRow(lbl);
                                int column = Grid.GetColumn(lbl);

                                string[] tmp = _macierzNazwMiast[row - 1, column - 1].Split('-');

                                if (tmp.Length > 1)
                                {
                                    this.ClearMatrix();

                                    for (int k = 0; k < tmp.Length - 1; k++)
                                    {

                                        foreach (var krw in _krawedzie)
                                        {
                                            if ((krw.V1.nazwa == tmp[k] || krw.V2.nazwa == tmp[k]) &&
                                                (krw.V1.nazwa == tmp[k + 1] || krw.V2.nazwa == tmp[k + 1]))
                                            {
                                                _dCanvas.Children.Remove(krw);
                                                krw.linia.Stroke = Brushes.Red;
                                                _dCanvas.Children.Add(krw);
                                            }
                                        }


                                    }
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


        private void Matrix_OnClosing(object sender, CancelEventArgs e)
        {
            this.ClearMatrix();
        }

        /// <summary>
        /// Method to clear matrix. Sets red edges to default.
        /// </summary>
        private void ClearMatrix()
        {
            if (Rozmiar > 1 && Krawedzie.Count > 0)
            {
                foreach (var krw in Krawedzie)
                {
                    DCanvas.Children.Remove(krw);
                    krw.linia.Stroke = Brushes.Blue;
                    DCanvas.Children.Add(krw);
                }
            }
        }
    }
}