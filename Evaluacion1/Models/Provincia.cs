using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Models
{
    public class Provincia
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public Pais Pais { get; set; }

        [Display(Name ="CPais")]
        [Required]
        public int PaisId { get; set; }

        public IEnumerable<Persona> Persona { get; set; }
    }
}
