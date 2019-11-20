using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evaluacion1.Models;
using Evaluacion1.Service;
using Evaluacion1.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Evaluacion1.Controllers
{
    public class PaisController : Controller
    {
        private readonly IPaisService _paisService;
        private readonly IProvinciaService _provinciaService;

        public PaisController(IPaisService paisService, IProvinciaService provinciaService)
        {
            this._paisService = paisService;
            this._provinciaService = provinciaService;
        }

        public IActionResult Index()
        {
            var paisDB = _paisService.GetAll();
            var paisVM = new List<PaisViewModel>();

            foreach (var item in paisDB)
            {
                paisVM.Add(new PaisViewModel
                {
                    Pais = item
                });
            }

            return View(paisVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(PaisViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var pais = new Pais
            {
                Name = model.Pais.Name,
                Continente = model.Pais.Continente
            };

           // _paisService.Create(pais);

            return RedirectToAction("Index", "Pais");
        }

        public IActionResult Edit(int? id)
        {
            var paisVM = new PaisViewModel();
            try
            {
                if (id == null)
                {
                    return View("Error");
                }

                var paisDB = _paisService.GetPais(id);

                if (paisDB == null)
                    return View("Error");

                paisVM = new PaisViewModel
                {
                    Pais = paisDB
                };
            }
            catch (Exception)
            {
                throw;
            }

            return View(paisVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, PaisViewModel model)
        {
            if (id != model.Pais.Id)
                return View("Error");

            if (!ModelState.IsValid)
                return View(model);
            
            var paisDB = _paisService.GetPais(id);

            if (paisDB == null)
                return View("Error");

            paisDB.Name = model.Pais.Name;
            paisDB.Continente = model.Pais.Continente;

            _paisService.Edit(paisDB);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
                return View("Error");

            var paisDB = _paisService.GetPais(id);

            if (paisDB == null)
                return View("Error");

            var paisVM = new PaisViewModel
            {
                Pais = paisDB
            };

            return View(paisVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var paisDB = _paisService.GetPais(id);
            _paisService.Delete(paisDB);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult _Detail(int? id)
        {
            var paisVM = new PaisViewModel();

            try
            {
                if (id == null)
                    return View("Error");

                var provinciaDB = _provinciaService.GetProvinciaByPais(id);

                if (provinciaDB == null)
                    return View("Error");

                paisVM = new PaisViewModel
                {
                    Provincia = provinciaDB
                };
                
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(paisVM);
        }

        public IActionResult Detail(int? id)
        {
            var paisVM = new PaisViewModel();

            try
            {
                if (id == null)
                    return View("Error");

                var paisDB = _paisService.GetProvinciaByPais(id);

                paisVM = new PaisViewModel
                {
                    Pais = paisDB
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(paisVM);
        }
    }

}