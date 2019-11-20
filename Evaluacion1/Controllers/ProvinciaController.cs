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
    public class ProvinciaController : Controller
    {
        private readonly IProvinciaService _provinciaService;
        private readonly IPaisService _paisService;

        public ProvinciaController(IProvinciaService provinciaService, IPaisService paisService)
        {
            this._provinciaService = provinciaService;
            this._paisService = paisService;
        }

        public IActionResult Index()
        {
            var provinciaVM = new List<ProvinciaViewModel>();
            var provinciaDB = _provinciaService.GetAll();

            foreach (var item in provinciaDB)
            {
                provinciaVM.Add(new ProvinciaViewModel
                {
                    Provincia = item
                });
            }
            return View(provinciaVM);
        }

        public IActionResult Create()
        {
            var paisDB = _paisService.GetAll();
            var paisVM = new ProvinciaViewModel();

            var p = new ProvinciaViewModel();

            p.Pais = paisDB;

            paisVM = new ProvinciaViewModel
            {
                
                Pais = paisDB,
                Provincia = new Provincia()
            };

            return View(paisVM);
        }

        [HttpPost]
        public IActionResult Create(ProvinciaViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var provincia = new Provincia
                {
                    Name = model.Provincia.Name,
                    PaisId = model.Provincia.PaisId
                };

                _provinciaService.Create(provincia);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index), "Provincia");
        }

        public IActionResult Edit(int? id)
        {
            var provinciaVM = new ProvinciaViewModel();

            try
            {
                if (id == null)
                    return View("Error");

                var provinciaDB = _provinciaService.GetProvincia(id);

                if (provinciaDB == null)
                    return View("Error");

                provinciaVM = new ProvinciaViewModel
                {
                    Provincia = provinciaDB,
                    Pais = _paisService.GetAll()
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(provinciaVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, ProvinciaViewModel model)
        {
            try
            {
                if (id != model.Provincia.Id)
                    return View("Error");

                if (!ModelState.IsValid)
                    return View(model);

                var provinciaDB = _provinciaService.GetProvincia(id);

                if (provinciaDB == null)
                    return View("Error");

                provinciaDB.Name = model.Provincia.Name;
                provinciaDB.PaisId = model.Provincia.PaisId;

                _provinciaService.Edit(provinciaDB);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index), "Provincia");
        }

        public IActionResult Delete(int? id)
        {
            var provinciaVM = new ProvinciaViewModel();

            try
            {
                if (id == null)
                    return View("Error");

                var provinciaDB = _provinciaService.GetProvincia(id);

                if(provinciaDB == null)
                    return View("Error");

                provinciaVM = new ProvinciaViewModel
                {
                    Provincia = provinciaDB
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(provinciaVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var provinciaDB = _provinciaService.GetProvincia(id);
            _provinciaService.Delete(provinciaDB);
            return RedirectToAction(nameof(Index));

        }
    }
}