using Evaluacion1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Service
{
    public interface IPersonaService
    {
        IEnumerable<Persona> GetAll();
        bool Create(Persona persona);
        bool Edit(Persona persona);
        bool Delete(Persona persona);
        Persona GetPersona(int? id);
        IEnumerable<Persona> GetPersonaByCedula(string cedula);
    }
    public class PersonaService : IPersonaService
    {
        private readonly EvaluacionDbContext _context;

        public PersonaService(EvaluacionDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Persona> GetAll()
        {
            var result = new List<Persona>();

            try
            {
                result = _context.Personas.Include(p => p.Provincia).Include(p => p.Provincia.Pais).ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public bool Create(Persona persona)
        {
            try
            {
                _context.Add(persona);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Edit(Persona persona)
        {
            try
            {
                _context.Update(persona);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Persona persona)
        {
            try
            {
                _context.Remove(persona);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Persona GetPersona(int? id)
        {
            var result = new Persona();

            try
            {
                result = _context.Personas.Include(p => p.Provincia).SingleOrDefault(p => p.Id == id);
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public IEnumerable<Persona> GetPersonaByCedula(string cedula)
        {
            var result = new List<Persona>();

            try
            {
                result = _context.Personas.Include(p => p.Provincia).Include(p => p.Provincia.Pais).Where(p => p.Cedula == cedula).ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }
    }
}
