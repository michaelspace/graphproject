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
        private readonly Random rd;
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

        public List<string> MiastaList { get; private set; }

        public City()
        {
            rd = new Random();

            miasta = new List<string>
            {
                "Gdańsk",
                "Gdynia",
                "Szczecin",
                "Olsztyn",
                "Łódź",
                "Kraków",
                "Poznań",
                "Warszawa",
                "Wrocław",
                "Kielce",
                "Opole",
                "Lublin",
                "Bydgoszcz",
                "Katowice",
                "Rzeszów"
            };

            MiastaList = new List<string>
            {
                "Gdańsk",
                "Gdynia",
                "Szczecin",
                "Olsztyn",
                "Łódź",
                "Kraków",
                "Poznań",
                "Warszawa",
                "Wrocław",
                "Kielce",
                "Opole",
                "Lublin",
                "Bydgoszcz",
                "Katowice",
                "Rzeszów"
            };
        }

        public void Dodaj_Miasto(string m)
        {
            miasta.Add(m);
        }
    }
}
