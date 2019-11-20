using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Models
{
    public class Persona
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Apellido { get; set; }
        [Required]
        public string Cedula { get; set; }

        [Display(Name ="Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode =false)]
        public DateTime FechaNacimiento { get; set; }

        public Provincia Provincia { get; set; }

        [Required]
        [Display(Name ="Provincia/Ciudad")]
        public int ProvinciaId { get; set; }
    }
}
