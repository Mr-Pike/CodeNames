using CodeNames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Interfaces
{
    public interface IParametersService
    {
        IEnumerable<Parameters> FindAll();
        Task<Parameters> FindById(short id);
        Task<Parameters> Update(Parameters parameter);
    }
}
