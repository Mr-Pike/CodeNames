using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeNames.Models;
using CodeNames.Services;
using CodeNames.Interfaces;

namespace CodeNames.Controllers
{
    public class WordsController : Controller
    {
        private IWordsService _wordsService;

        public WordsController(IWordsService wordsService)
        {
            _wordsService = wordsService;
        }

        // GET: Words
        [HttpGet]
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            return View(await _wordsService.Paginate(searchString, sortOrder, pageNumber));
        }

        // GET: Words/5
        /*[HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            Departement departement = _wordsService.FindById(id);
            if (departement == null)
            {
                return NotFound();
            }

            return Ok(departement);
        }*/

        // POST api/Words
        /*[HttpPost]
        public async Task<IActionResult> Post([FromBody] Departement departement)
        {
            try
            {
                departement = await _wordsService.Create(departement);

                return Ok(departement);
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }*/

        // PUT api/Words/5
        /*[HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Departement departement)
        {
            // Vérifier la cohérence.
            if (departement.Id != id)
            {
                return BadRequest();
            }

            // Vérifier si le département existe.
            Departement departementInitial = _wordsService.FindById(id);
            if (departementInitial == null)
            {
                return NotFound();
            }

            // Mettre à jour le département.
            try
            {
                departement = await _wordsService.Update(departement, departementInitial);
                return Ok(departement);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("error", e.Message);
                return new BadRequestObjectResult(ModelState);
            }
        }*/

        // PUT api/Words/5
        /*[HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            // Vérifier si le département existe.
            Departement departement = _wordsService.FindById(id);
            if (departement == null)
            {
                return NotFound();
            }

            // Essayer de supprimer le département.
            if (await _wordsService.Delete(departement))
            {
                return NoContent();
            }

            return BadRequest();
        }*/
    }
}
