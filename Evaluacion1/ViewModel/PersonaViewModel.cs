using Evaluacion1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.ViewModel
{
    public class PersonaViewModel
    {
        public Persona Persona { get; set; }
        public IEnumerable<Provincia> Provincia { get; set; }


        //De prueba
        public IEnumerable<Provincia> ProvinciaDDL { get; set; }
        public IEnumerable<Pais> PaisDDL { get; set; }



    }
}
