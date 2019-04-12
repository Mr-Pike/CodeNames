using CodeNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IGamesService
    {
        IEnumerable<Games> FindAll();
        IEnumerable<ViewGames> FindById(int id);
        string GridColor(int id, string currentUrl);
        Task<Games> Generate();
    }
}
