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
        private IThemesService _themesService;

        public WordsController(IWordsService wordsService, IThemesService themesService)
        {
            _wordsService = wordsService;
            _themesService = themesService;
        }

        // GET: Words
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["IdSortParm"] = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["NameSortParm"] = sortOrder == "Name" ? "name_desc" : "Name";
            ViewData["ThemesSortParm"] = sortOrder == "ThemesName" ? "themesname_desc" : "ThemesName";

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

        // GET: Words/Create
        public IActionResult Create()
        {
            ViewData["ThemesSelect"] = _themesService.SelectList(null);

            return View();
        }

        // POST: Words/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Words words)
        {
            try
            {
                await _wordsService.Create(words, Request.Form["Themes"]);
                TempData["Success"] = "Word created.";
            }
            catch
            {
                TempData["Error"] = "Word not created, already exists.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Words/Edit/2.
        public async Task<IActionResult> Edit(int id)
        {
            Words words = await _wordsService.FindById(id);
            if (words == null)
            {
                TempData["Error"] = "Word not exists.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["ThemesSelect"] = _themesService.SelectList(words.Themeswords);

            return View(words);
        }

        // POST: Words/Edit/2.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, Words words)
        {
            if (id != words.Id)
            {
                TempData["Error"] = "Word doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Word updated.";
            await _wordsService.Update(words, Request.Form["Themes"]);
            return RedirectToAction(nameof(Index));
        }

        // POST: Words/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Words words = await _wordsService.FindById(id);
            if (words == null)
            {
                TempData["Error"] = "Word not exists.";
            }

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