using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Models
{
    public class EvaluacionDbContext : DbContext
    {
        public EvaluacionDbContext(DbContextOptions<EvaluacionDbContext> options)
            :base(options)
        {

        }

        public DbSet<Pais> Pais { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Persona> Personas { get; set; }
    }
}
