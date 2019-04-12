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
    public class TeamsController : Controller
    {
        private ITeamsService _teamsService;

        public TeamsController(ITeamsService teamService)
        {
            _teamsService = teamService;
        }

        // GET: Teams.
        public IActionResult Index()
        {
            return View(_teamsService.FindAll());
        }

        // GET: Teams/Edit/2.
        public async Task<IActionResult> Edit(int id)
        {
            Teams team = await _teamsService.FindById(id);
            if (team == null)
            {
                TempData["Error"] = "Team doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            return View(team);
        }

        // POST: Teams/Edit/2.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int id, [Bind("Id,Name,Color")] Teams team)
        {
            if (id != team.Id)
            {
                TempData["Error"] = "Team doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            TempData["Success"] = "Team updated.";
            await _teamsService.Update(team);
            return RedirectToAction(nameof(Index));
        }
    }
}
