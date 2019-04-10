using CodeNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IGamesService
    {
        List<Games> FindAll();
        Task<Games> Generate();
    }
}
