using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphproject.Model
{
    class Graph
    {
        public List<Vertex> wierzcholki;
        public List<Edge> krawedzie;

        public Graph()
        {
            wierzcholki = new List<Vertex>();
            krawedzie = new List<Edge>();
        }
    }
}
