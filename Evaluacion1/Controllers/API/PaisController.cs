using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evaluacion1.Models;
using Evaluacion1.Service;
using Evaluacion1.ViewModel;

namespace Evaluacion1.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaisController : ControllerBase
    {
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            this._paisService = paisService;
        }

        // GET: api/Pais
        [HttpGet]
        public IActionResult GetPais()
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

            return Ok(paisVM);
        }

        // GET: api/Pais/5
        [HttpGet("{id}")]
        public IActionResult GetPais([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest("Peticion no valida");

            var paisDB = _paisService.GetPais(id);
            var paisVM = new PaisViewModel();

            if (paisDB == null)
                return NotFound();

            paisVM = new PaisViewModel
            {
                Pais = paisDB
            };

            return Ok(paisVM);
        }

        // POST: api/Pais
        [HttpPost]
        public IActionResult PostPais([FromBody] PaisViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var pais = new Pais
            {
                Name = model.Pais.Name,
                Continente = model.Pais.Continente
            };

            _paisService.Create(pais);

            return Ok("Created");
        }

        // PUT: api/Pais/5
        [HttpPut("{id}")]
        public IActionResult EditPais([FromRoute] int id, [FromBody] PaisViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != model.Pais.Id)
                return BadRequest();

            var pais = _paisService.GetPais(model.Pais.Id);

            if (pais == null)
                return NotFound();

            pais.Name = model.Pais.Name;
            pais.Continente = model.Pais.Continente;

            _paisService.Edit(pais);

            return NoContent();
        }



        // DELETE: api/Pais/5
        [HttpDelete("{id}")]
        public IActionResult DeletePais([FromRoute] int? id)
        {
            if (id == null)
                return BadRequest();

            var pais = _paisService.GetPais(id);

            if (pais == null)
                return NotFound();

            _paisService.Delete(pais);

            return Ok("Deleted");
        }

        
    }
}