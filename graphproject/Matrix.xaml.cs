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
using System.Windows.Shapes;

namespace graphproject
{
    /// <summary>
    /// Interaction logic for Matrix.xaml
    /// </summary>
    public partial class Matrix : Window
    {
        private int[,] macierzWagInts;
        private string[,] macierzNazwMiast;
        private string[] macierzNazwStrings;
        private int rozmiar;

        public Matrix(int[,] wagi, string[,] macierzNazw, string[] nazwy)
        {
            InitializeComponent();

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

                //for (int i = 1; i <=rozmiar; i++)
                //{
                //    var label = new Label()
                //    {
                //        Content = macierzWagInts[i-1,i-1],
                //        HorizontalAlignment = HorizontalAlignment.Center,
                //        VerticalAlignment = VerticalAlignment.Center

                //    };
                //    myOutputGrid.Children.Add(label);
                //    Grid.SetRow(label, i);
                //    Grid.SetColumn(label, i);
                //}


                for (int i = 0; i < rozmiar; i++)
                {
                    for (int j = 0; j < rozmiar; j++)
                    {
                        if (i==j)
                        {
                            macierzNazwMiast[i, j] = "0";
                        }
                        else if (macierzWagInts[i, j] >= 1073741822)
                        {
                            macierzNazwMiast[i, j] = "0";
                        }
                        else
                        {
                            string[] tmp = macierzNazwMiast[i, j].Split('.');
                            string prev_id = tmp[tmp.Length - 2];

                            try
                            {
                                int id;
                                bool correct_id = int.TryParse(prev_id, out id);
                                macierzNazwMiast[i, j] = macierzNazwStrings[id-1];
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
                                Content = macierzNazwMiast[i - 1, j - 1] + "/" + macierzWagInts[i - 1, j - 1],
                                HorizontalAlignment = HorizontalAlignment.Center,
                                VerticalAlignment = VerticalAlignment.Center

                            };
                            myOutputGrid.Children.Add(label);
                            Grid.SetRow(label, i);
                            Grid.SetColumn(label, j);
                        }
                        else
                        {
                            var label = new Label()
                            {
                                Content = "0",
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
/*
<Grid.ColumnDefinitions>
<ColumnDefinition Width = "*" />
< ColumnDefinition Width="*"/>
</Grid.ColumnDefinitions>
<Grid.RowDefinitions>
<RowDefinition Height = "*" />
< RowDefinition Height="*"/>
</Grid.RowDefinitions>
        
<Label Content = "Test" Grid.Row="1" Grid.Column="1" VerticalAlignment="Center" HorizontalAlignment="Center"/>
*/