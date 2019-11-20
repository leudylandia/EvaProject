using Evaluacion1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evaluacion1.Service
{
    public interface IProvinciaService
    {
        bool Create(Provincia provincia);
        bool Edit(Provincia provincia);
        bool Delete(Provincia provincia);
        IEnumerable<Provincia> GetAll();
        Provincia GetProvincia(int? id);
        List<Provincia> GetProvinciaByPais(int? paisId);

        List<Provincia> GetProvinciaByPaisSinInclude(int? paisId);
    }
    public class ProvinciaService : IProvinciaService
    {
        public readonly EvaluacionDbContext _context;

        public ProvinciaService(EvaluacionDbContext context)
        {
            _context = context;
        }

        public bool Create(Provincia provincia)
        {
            try
            {
                _context.Add(provincia);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Delete(Provincia provincia)
        {
            try
            {
                _context.Remove(provincia);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Edit(Provincia provincia)
        {
            try
            {
                _context.Update(provincia);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Provincia> GetAll()
        {
            var result = new List<Provincia>();

            try
            {
                result = _context.Provincias.Include(p => p.Pais).ToList();
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        public Provincia GetProvincia(int? id)
        {
            try
            {
                return _context.Provincias.Include(p => p.Pais).Single(p => p.Id == id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Provincia> GetProvinciaByPais(int? paisId)
        {
            var result = new List<Provincia>();

            try
            {
                result = _context.Provincias.Include(p => p.Pais).Where(p => p.PaisId == paisId).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public List<Provincia> GetProvinciaByPaisSinInclude(int? paisId)
        {
            var result = new List<Provincia>();

            try
            {
                result = _context.Provincias.Where(p => p.PaisId == paisId).ToList();
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
