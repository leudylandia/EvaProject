using Evaluacion1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.ViewModel
{
    public class ProvinciaViewModel
    {
        public Provincia Provincia { get; set; }
        public IEnumerable<Pais> Pais { get; set; }
    }
}
