using CodeNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface ITeamsService
    {
        IEnumerable<Teams> FindAll();
        Task<Teams> FindById(int id);
        Task<Teams> Update(Teams teams);
    }
}
