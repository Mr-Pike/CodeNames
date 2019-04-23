using CodeNames.Models;
using CodeNames.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IThemesService
    {
        IEnumerable<Themes> FindAll();
        Task<PaginatedListService<Themes>> Paginate(string searchString, string sortOrder, int? pageNumber);
        Task<Themes> FindById(int id);
        Task<Themes> Create(Themes theme);
        Task<bool> Delete(Themes theme);
        List<SelectListItem> SelectList(ICollection<Themeswords> themesWords);
    }
}
