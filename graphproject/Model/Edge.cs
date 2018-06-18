using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace graphproject.Model
{
    public class Edge: Canvas
    {
        public int id;
        public int waga;

        public Line linia  { get; set; }
        public Label label { get; set; }
        public Vertex V1 { get; set; }
        public Vertex V2 { get; set; }

        public Edge(int id, int waga, Vertex zrodlo, Vertex ujscie)
        {
            this.id = id;
            this.waga = waga;
            
            V1 = zrodlo;
            V2 = ujscie;

            if (!V1.sasiedzi.Contains(V2) && !V2.sasiedzi.Contains(V1))
            {
                V1.sasiedzi.Add(V2);
                V2.sasiedzi.Add(V1);
            }

            linia = new Line()
            {
                Stroke = Brushes.Blue,
                StrokeThickness = 2,

                X1 = V1.wspolrzednaX+15,
                Y1 = V1.wspolrzednaY+15,

                X2 = V2.wspolrzednaX+15,
                Y2 = V2.wspolrzednaY+15
            };

            label = new Label()
            {
                Content = waga,
                FontSize = 12
            };

            SetLeft(label, (linia.X1 + linia.X2) / 2);
            SetTop(label, (linia.Y1 + linia.Y2) / 2);

            Children.Add(linia);
            Children.Add(label);
        }
    }
}
