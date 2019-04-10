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

        public GamesController(IHttpContextAccessor context, IGamesService gamesService)
        {
            _context = context;
            _gamesService = gamesService;
        }

        // GET: Games
        public IActionResult Index()
        {
            return View();
        }

        // GET: Games/Details/5
        [HttpGet("{id}")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return View();
        }

        // GET: Games/Generate
        public async Task<IActionResult> Generate()
        {
            return View("../Games/Show", await _gamesService.Generate());
        }

        // GET: Games/GridColorHtml/5
        public IActionResult GridColorHtml(int id)
        {
            ViewData["ViewGames"] = _gamesService.FindById(id).ToArray();
            return View();
        }

        // GET: Games/GridColor/5
        public IActionResult GridColor(int id)
        {
            //var url = $"https://localhost:44381/Games/GridColorHtml/{id}";
            string url = $"{Request.HttpContext.Request.Scheme}://{Request.Host.Value}/Games/GridColorHtml/{id}";
            string path = _gamesService.GridColor(id, url);

            return File(new FileStream(path, FileMode.Open), "image/png", $"grid-{id}.png");
        }

        // POST: Games/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("")] Games games)
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
        [HttpPut("{id}")]
        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}
