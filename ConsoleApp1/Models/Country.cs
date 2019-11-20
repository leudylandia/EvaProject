using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class Country
    {
        public Pais pais { get; set; }
    }

    public class Pais
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Continente { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} /n Pais: {1} /n Continente: {2} /n", Id, Name, Continente);
        }
    }
}
