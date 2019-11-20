using Evaluacion1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Service
{
    public interface IPaisService
    {
        bool Create(Pais pais);
        IEnumerable<Pais> GetAll();
        Pais GetPais(int? id);
        bool Edit(Pais pais);
        bool Delete(Pais pais);
        bool PaisExists(int id);
        Pais GetProvinciaByPais(int? paisId);
    }
    public class PaisService : IPaisService //Aca heredamos de la interface declarada mas arriba, para implementar sus metodos
    {
        private readonly EvaluacionDbContext _context;

        public PaisService(EvaluacionDbContext context)
        {
            this._context = context;
        }

        public bool Create(Pais pais)
        {
            try
            {
                _context.Add(pais);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Pais pais)
        {
            try
            {
                _context.Remove(pais);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Edit(Pais pais)
        {
            try
            {
                _context.Update(pais);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Pais> GetAll()
        {
            var result = new List<Pais>();

            try
            {
                result = _context.Pais.ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public Pais GetPais(int? id)
        {
            var pais = new Pais();

            try
            {
                pais = _context.Pais.Single(p => p.Id == id);
            }
            catch (Exception)
            {
                pais = null;
            }

            return pais;
        }

        public Pais GetProvinciaByPais(int? paisId)
        {
            var result = new Pais();

            try
            {
                result =  _context.Pais.Include(p => p.Provincia).SingleOrDefault(p => p.Id == paisId);
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool PaisExists(int id)
        {
            return _context.Pais.Any(e => e.Id == id);
        }
    }
}
