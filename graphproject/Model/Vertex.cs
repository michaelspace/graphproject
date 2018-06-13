using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace graphproject.Model
{
    public class Vertex: Canvas
    {
        public int id;

        public string nazwa { get; set; }
        public double wspolrzednaX { get; set; }
        public double wspolrzednaY { get; set; }

        public Ellipse circle { get; set; }
        public Label label { get; set; }

        public List<Vertex> sasiedzi { get; set; }
        public List<Edge> krawedzie { get; set; } // do wyznaczenia macierzy

        public Vertex(int id, string nazwa, double wspolrzednaX, double wspolrzednaY)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.wspolrzednaX = wspolrzednaX;
            this.wspolrzednaY = wspolrzednaY;

            krawedzie = new List<Edge>();
            sasiedzi = new List<Vertex>();

            circle = new Ellipse
            {
                Width = 30,
                Height = 30,
                Stroke = new SolidColorBrush(Colors.BurlyWood),
                StrokeThickness = 2,
                Fill = new SolidColorBrush(Colors.LightYellow)
            };

            label = new Label()
            {
                Content = nazwa
            };

            Children.Add(circle);
            Children.Add(label);
        }

    }
}
