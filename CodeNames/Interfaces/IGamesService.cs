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
        Task<Games> FindById(int id);
        IEnumerable<ViewGames> FindViewGamesById(int id);
        string GridColor(int id, string currentUrl);
        Task<Games> Generate();
        Task<Games> FoundWord(int id, int wordId, short? teamId);
        Task<bool> Delete(Games games);
    }
}
