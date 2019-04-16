using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeNames.Models;
using CodeNames.Services;
using CodeNames.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CodeNames.Controllers
{
    public class GamesController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IGamesService _gamesService;
        private readonly ITeamsService _teamsService;
        private readonly IWordsService _wordsService;

        public GamesController(IHttpContextAccessor context, IGamesService gamesService, ITeamsService teamsService, IWordsService wordsService)
        {
            _context = context;
            _gamesService = gamesService;
            _teamsService = teamsService;
            _wordsService = wordsService;
        }

        // GET: Games
        public IActionResult Index(string sortOrder, string currentFilter, int? pageNumber)
        {
            return View(_gamesService.FindAll());
        }

        // GET: Games/Details/5
        public async Task<IActionResult> Details(int id)
        {
            IEnumerable<ViewGames> viewGames = _gamesService.FindViewGamesById(id);
            if (viewGames == null || viewGames.Count() != 25)
            {
                TempData["Error"] = "Team doesn't exist.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Game"] = await _gamesService.FindById(id);
            ViewData["Teams"] = _teamsService.FindAll();

            return View(viewGames);
        }

        // GET: Games/Generate
        public async Task<IActionResult> Generate()
        {
            Games game = await _gamesService.Generate();
            return RedirectToAction("Details", new { id = game.Id });
        }

        // GET: Games/GridColorHtml/5
        public IActionResult GridColorHtml(int id)
        {
            return View(_gamesService.FindViewGamesById(id));
        }

        // POST: Games/FoundWord
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FoundWord([FromBody]Gameswords gameword)
        {
            var result = await _gamesService.FoundWord(gameword.GameId, gameword.WordId, gameword.TeamId);

            return new JsonResult(result);
        }

        // GET: Games/GridColor/5
        public IActionResult GridColor(int id)
        {
            //var url = $"https://localhost:44381/Games/GridColorHtml/{id}";
            string url = $"{Request.HttpContext.Request.Scheme}://{Request.Host.Value}/Games/GridColorHtml/{id}";
            string path = _gamesService.GridColor(id, url);

            return File(new FileStream(path, FileMode.Open), "image/png", $"grid-{id}.png");
        }

        // GET: Games/Create
        public IActionResult Create()
        {
            ViewData["RealTeamsSelect"] = _teamsService.SelectList(true);
            ViewData["TeamsSelect"] = _teamsService.SelectList(false);
            ViewData["WordsSelect"] = _wordsService.SelectList();

            return View();
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Games games)
        {
            return View();
        }

        // GET: Games/Edit/5
        public IActionResult Edit(int? id)
        {
            return View();
        }

        // POST: Games/Edit/5
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public IActionResult EditPost()
        {
            return View();
        }

        // GET: Games/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Games games = await _gamesService.FindById(id);
            if (games == null)
            {
                TempData["Error"] = "Game not exists.";
            }

            if (await _gamesService.Delete(games))
            {
                TempData["Success"] = "Game removed.";
            }
            else
            {
                TempData["Error"] = "Game not deleted: error occured.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
