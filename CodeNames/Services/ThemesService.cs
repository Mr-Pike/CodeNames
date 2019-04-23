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
    public class ThemesService : IThemesService
    {
        private readonly ApplicationDbContext _context;
        private int pageSize = 10;

        public ThemesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Themes> FindAll()
        {
            return _context.Themes;
        }

        public async Task<PaginatedListService<Themes>> Paginate(string searchString, string sortOrder, int? pageNumber)
        {
            var themes = from t in _context.Themes
                        select t;

            if (!string.IsNullOrEmpty(searchString))
            {
                themes = themes.Where(x => x.Name.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "id_desc":
                    themes = themes.OrderByDescending(s => s.Name);
                    break;
                case "Name":
                    themes = themes.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    themes = themes.OrderByDescending(s => s.Name);
                    break;
                default:
                    themes = themes.OrderBy(s => s.Name);
                    break;
            }

            return await PaginatedListService<Themes>.CreateAsync(themes.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public async Task<Themes> FindById(int id)
        {
            try
            {
                return await _context.Themes.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Themes> Create(Themes theme)
        {
            try
            {
                await _context.AddAsync(theme);
                await _context.SaveChangesAsync();

                return theme;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<bool> Delete(Themes theme)
        {
            try
            {
                _context.Remove(theme);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
