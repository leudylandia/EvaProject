using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evaluacion1.Service;
using Evaluacion1.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Evaluacion1.Models;
using Newtonsoft.Json.Serialization;

namespace Evaluacion1.Controllers
{
    public class PersonasController : Controller
    {
        private readonly IPersonaService _personaService;
        private readonly IProvinciaService _provinciaService;
        private readonly IPaisService _paisService;

        public PersonasController(IPersonaService personaService, IProvinciaService provinciaService, IPaisService paisService)
        {
            this._personaService = personaService;
            this._provinciaService = provinciaService;
            this._paisService = paisService;
        }

        public IActionResult Index()
        {
            var personaVM = new List<PersonaViewModel>();

            try
            {
                var personaDB = _personaService.GetAll();

                foreach (var item in personaDB)
                {
                    personaVM.Add(new PersonaViewModel
                    {
                        Persona = item
                    });
                }
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(personaVM);
        }

        public IActionResult Create()
        {
            var personaVM = new PersonaViewModel();

            try
            {
                personaVM = new PersonaViewModel
                {
                    Persona = new Models.Persona(),
                    //Provincia = _provinciaService.GetAll(),
                    PaisDDL = _paisService.GetAll()
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(personaVM);
        }

        [HttpPost]
        public IActionResult Create(PersonaViewModel model)
        {   
            try
            {
                if (!ModelState.IsValid)
                {
                    var personaVM = new PersonaViewModel
                    {
                        Persona = model.Persona,
                        //Provincia = _provinciaService.GetAll(),
                        PaisDDL = _paisService.GetAll()
                    };
                    return View(personaVM);
                }
                    

                var persona = new Persona()
                {
                    Apellido = model.Persona.Apellido,
                    Nombre = model.Persona.Nombre,
                    Cedula = model.Persona.Cedula,
                    FechaNacimiento = model.Persona.FechaNacimiento,
                    ProvinciaId = model.Persona.ProvinciaId
                };

                _personaService.Create(persona);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index), "Personas");
        }

        public IActionResult Edit (int? id)
        {
            var personaVM = new PersonaViewModel();

            try
            {
                if (id == null)
                    return View("Error");

                var personaDB = _personaService.GetPersona(id);

                if (personaDB == null)
                    return View("Error");

                personaVM = new PersonaViewModel
                {
                    Persona = personaDB,
                    Provincia = _provinciaService.GetAll()
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(personaVM);
        }

        [HttpPost]
        public IActionResult Edit(int id, PersonaViewModel model)
        {
            try
            {
                if (id != model.Persona.Id)
                    return View("Error");

                if (!ModelState.IsValid)
                {
                    var personaVM = new PersonaViewModel
                    {
                        Persona = model.Persona,
                        Provincia = _provinciaService.GetAll()
                    };
                    return View(personaVM);
                }

                var personaDB = _personaService.GetPersona(id);

                if (personaDB == null)
                    return View("Error");

                personaDB.Apellido = model.Persona.Apellido;
                personaDB.Nombre = model.Persona.Nombre;
                personaDB.Cedula = model.Persona.Cedula;
                personaDB.FechaNacimiento = model.Persona.FechaNacimiento;
                personaDB.ProvinciaId = model.Persona.ProvinciaId;

                _personaService.Edit(personaDB);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            var personaVM = new PersonaViewModel();
            try
            {
                if (id == null)
                    return View("Error");

                var personaDB = _personaService.GetPersona(id);

                if (personaDB == null)
                    return View("Error");

                personaVM = new PersonaViewModel
                {
                    Persona = personaDB
                };
            }
            catch (Exception)
            {
                return View("Error");
            }

            return View(personaVM);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var personaDB = _personaService.GetPersona(id);
                if (personaDB == null)
                    return View("Error");

                _personaService.Delete(personaDB);
            }
            catch (Exception)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

       [HttpGet]
        //[Route("action/{texto}")]
        public IActionResult GetPersonaCedula(string Ced)
        {
            var personaVM = new List<PersonaViewModel>();
            ViewData["Filtro"] = Ced;

            try
            {
                if (Ced == null)
                    return RedirectToAction(nameof(Index));

                var personaDB = _personaService.GetPersonaByCedula(Ced);
                
                foreach (var item in personaDB)
                {
                    personaVM.Add(new PersonaViewModel
                    {
                        Persona = item
                    });
                }

            }
            catch (Exception)
            {
                personaVM = null;
            }

            return View("Index", personaVM);
        }

        [HttpPost]
        public JsonResult GetProvincias(int id, PersonaViewModel model)
        {
            var personaVM = _provinciaService.GetProvinciaByPaisSinInclude(id);

            //personaVM.ProvinciaDDL = _provinciaService.GetProvinciaByPaisSinInclude(id);

            return Json(personaVM);

//return Json(personaVM);
        }

        [HttpPost]
        public IActionResult GetProvincias2(PersonaViewModel model)
        {
            var personaVM2 = _provinciaService.GetProvinciaByPaisSinInclude(1);

            if (!ModelState.IsValid)
            {
                model.Persona.Nombre = "Prueba modo on";
                var personaVM = new PersonaViewModel
                {
                    
                    Persona = model.Persona,
                    //Provincia = _provinciaService.GetAll(),
                    PaisDDL = _paisService.GetAll()
                };
                return View(personaVM);
            }
                

            //personaVM.ProvinciaDDL = _provinciaService.GetProvinciaByPaisSinInclude(id);

            return Json(personaVM2);

            //return Json(personaVM);
        }
    }
}