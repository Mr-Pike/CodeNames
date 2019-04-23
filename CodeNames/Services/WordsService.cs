using CodeNames.Data;
using CodeNames.Interfaces;
using CodeNames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Services
{
    public class WordsService : IWordsService
    {
        private readonly ApplicationDbContext _context;
        private int pageSize = 10;

        public WordsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Words> FindAll()
        {
            return _context.Words;
        }

        public async Task<PaginatedListService<WordsView>> Paginate(string searchString, string sortOrder, int? pageNumber)
        {
            var words = from w in _context.WordsView
                        select w;

            if (!string.IsNullOrEmpty(searchString))
            {
                words = words.Where(x => x.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    words = words.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    words = words.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    words = words.OrderByDescending(s => s.Name);
                    break;
                case "ThemesName":
                    words = words.OrderBy(s => s.ThemesName);
                    break;
                case "themesname_desc":
                    words = words.OrderByDescending(s => s.ThemesName);
                    break;
                default:
                    words = words.OrderBy(s => s.Name);
                    break;
            }

            return await PaginatedListService<WordsView>.CreateAsync(words.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public async Task<Words> FindById(int id)
        {
            try
            {
                return await _context.Words.Include(x => x.Themeswords).SingleOrDefaultAsync(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Words> Create(Words word, string themes)
        {
            try
            {
                // Check if themes is null or empty.
                if (!string.IsNullOrEmpty(themes))
                {
                    string[] themesSplited = themes.Split(',');
                    foreach (var theme in themesSplited)
                    {
                        word.Themeswords.Add(new Themeswords() { ThemeId = int.Parse(theme) });
                    }
                }


                await _context.AddAsync(word);
                await _context.SaveChangesAsync();

                return word;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<Words> Update(Words word, string themes)
        {
            try
            {
                // Delete all themes.
                var wordIni = await FindById(word.Id);
                _context.RemoveRange(wordIni.Themeswords);
                await _context.SaveChangesAsync();

                // Update name's world.
                wordIni.Name = word.Name;

                // Check if themes is null or empty.
                if (!string.IsNullOrEmpty(themes))
                {
                    string[] themesSplited = themes.Split(',');
                    foreach (var theme in themesSplited)
                    {
                        wordIni.Themeswords.Add(new Themeswords() { ThemeId = int.Parse(theme) });
                    }
                }

                _context.Update(wordIni);
                await _context.SaveChangesAsync();

                return wordIni;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<bool> Delete(Words word)
        {
            try
            {
                _context.Remove(word);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<SelectListItem> SelectList()
        {
            IEnumerable<Words> words = FindAll();
            List<SelectListItem> listSelectListItem = new List<SelectListItem>();

            foreach (var word in words)
            {
                listSelectListItem.Add(new SelectListItem()
                {
                    
                    Text = word.Name,
                    Value = word.Id.ToString(),
                });
            }

            return listSelectListItem;
        }
    }
}
