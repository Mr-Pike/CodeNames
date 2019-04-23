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
    public class ThemesController : Controller
    {
        private IThemesService _themesService;

        public ThemesController(IThemesService themesService)
        {
            _themesService = themesService;
        }

        // GET: Themes
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

            return View(await _themesService.Paginate(searchString, sortOrder, pageNumber));
        }

        // POST: Themes
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] Themes themes)
        {
            try
            {
                await _themesService.Create(themes);
                TempData["Success"] = "Theme created.";
            }
            catch
            {
                TempData["Error"] = "Theme not created, already exists.";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Themes/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Themes themes = await _themesService.FindById(id);
            if (themes == null)
            {
                TempData["Error"] = "Theme not exists.";
            }

            if (await _themesService.Delete(themes))
            {
                TempData["Success"] = "Theme removed.";
            }
            else
            {
                TempData["Error"] = "Theme not deleted: error occured.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}