using CodeNames.Data;
using CodeNames.Interfaces;
using CodeNames.Models;
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

        public async Task<PaginatedListService<Words>> Paginate(string searchString, string sortOrder, int? pageNumber)
        {
            var words = from w in _context.Words
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
                default:
                    words = words.OrderBy(s => s.Name);
                    break;
            }

            return await PaginatedListService<Words>.CreateAsync(words.AsNoTracking(), pageNumber ?? 1, pageSize);
        }

        public Words FindById(int id)
        {
            try
            {
                return _context.Words.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Words> Create(Words word)
        {
            try
            {
                await _context.AddAsync(word);
                await _context.SaveChangesAsync();

                return word;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<Words> Update(Words word)
        {
            try
            {
                _context.Update(word);
                await _context.SaveChangesAsync();

                return word;
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
    }
}
