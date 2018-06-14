using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graphproject.Model
{
    public class City
    {
        private List<string> miasta;
        private readonly Random rd = new Random();
        public string Miasta
        {
            get
            {
                string miasto = "Miasto " + rd.Next(1, 100);
                if (miasta.Count > 1)
                {
                    miasto = miasta[rd.Next(0, miasta.Count)];
                    miasta.Remove(miasto);
                }
                
                return miasto;
            }
        }

        public City()
        {
            miasta = new List<string>();
            miasta.Add("Gdańsk");
            miasta.Add("Gdynia");
            miasta.Add("Szczecin");
            miasta.Add("Olsztyn");
            miasta.Add("Łódź");
            miasta.Add("Kraków");
            miasta.Add("Poznań");
            miasta.Add("Warszawa");
            miasta.Add("Wrocław");
            miasta.Add("Kielce");
            miasta.Add("Opole");
            miasta.Add("Lublin");
            miasta.Add("Bydgoszcz");
            miasta.Add("Katowice");
            miasta.Add("Rzeszów");
        }

        public void Dodaj_Miasto(string m)
        {
            miasta.Add(m);
        }
    }
}
