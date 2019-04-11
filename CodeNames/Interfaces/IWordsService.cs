using CodeNames.Models;
using CodeNames.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IWordsService
    {
        Task<PaginatedListService<Words>> Paginate(string searchString, string sortOrder, int? pageNumber);
        Words FindById(int id);
        Task<Words> Create(Words word);
        Task<Words> Update(Words word);
        Task<bool> Delete(Words word);
    }
}
