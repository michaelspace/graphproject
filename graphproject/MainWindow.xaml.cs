﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
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
        
    }
}
