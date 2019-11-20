using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Models
{
    public class Pais
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Continente { get; set; }

        public IEnumerable<Provincia> Provincia { get; set; }
    }
}
