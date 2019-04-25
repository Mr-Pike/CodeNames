using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeNames.Interfaces;
using CodeNames.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeNames.Controllers
{
    public class ParametersController : Controller
    {
        private readonly IParametersService _parametersService;

        public ParametersController(IParametersService parametersService)
        {
            _parametersService = parametersService;
        }

        // GET: Parameters
        public ActionResult Index()
        {
            return View(_parametersService.FindAll());
        }

        // GET: Teams/Edit/2.
        public async Task<IActionResult> Edit(short id)
        {
            Parameters parameters = await _parametersService.FindById(id);
            if (parameters == null)
            {
                TempData["Error"] = "Parameter doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            return View(parameters);
        }

        // POST: Teams/Edit/2.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Description,Value")] Parameters parameter)
        {
            if (id != parameter.Id)
            {
                TempData["Error"] = "Parameter doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Parameter updated.";
            await _parametersService.Update(parameter);
            return RedirectToAction(nameof(Index));
        }
    }
}