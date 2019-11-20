using Evaluacion1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.ViewModel
{
    public class PaisViewModel
    {
        public Pais Pais { get; set; }

        //Listado de provincia
        public List<Provincia> Provincia { get; set; }
    }
}
