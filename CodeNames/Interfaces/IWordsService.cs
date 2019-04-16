using CodeNames.Models;
using CodeNames.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IWordsService
    {
        IEnumerable<Words> FindAll();
        Task<PaginatedListService<Words>> Paginate(string searchString, string sortOrder, int? pageNumber);
        Task<Words> FindById(int id);
        Task<Words> Create(Words word);
        Task<bool> Delete(Words word);
        List<SelectListItem> SelectList();
    }
}
