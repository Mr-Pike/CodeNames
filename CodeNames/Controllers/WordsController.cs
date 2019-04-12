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

        // POST api/Words
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Words words)
        {
            try
            {
                await _wordsService.Create(words);
                TempData["Success"] = "Word created.";
            }
            catch
            {
                TempData["Error"] = "Word not created, already exists.";
            }

            return RedirectToAction(nameof(Index));
        }

        // Get api/Words/5
        public async Task<IActionResult> Delete(int id)
        {
            // Vérifier si le département existe.
            Words words = _wordsService.FindById(id);
            if (words == null)
            {
                TempData["Error"] = "Word not exists.";
            }

            // Essayer de supprimer le département.
            if (await _wordsService.Delete(words))
            {
                TempData["Success"] = "Word removed.";
            }
            else
            {
                TempData["Error"] = "Word not deleted: error occured.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
