using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodeNames.Models;
using CodeNames.Data;

namespace CodeNames.Controllers
{
    public class GamesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GamesController(ApplicationDbContext context)
        {
            _context = context;
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

        // GET: Games/Create
        public IActionResult Create()
        {
            return View();
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
