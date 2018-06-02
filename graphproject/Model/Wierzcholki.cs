using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphproject.Model
{
    public class Wierzcholek
    {
        public int id;
        public string nazwa;
        public double wspolrzednaX;
        public double wspolrzednaY;

        public Wierzcholek(int id, string nazwa, double wspolrzednaX, double wspolrzednaY)
        {
            this.id = id;
            this.nazwa = nazwa;
            this.wspolrzednaX = wspolrzednaX;
            this.wspolrzednaY = wspolrzednaY;
        }
    }
}
