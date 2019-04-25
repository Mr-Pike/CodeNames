using CodeNames.Data;
using CodeNames.Enums;
using CodeNames.Interfaces;
using CodeNames.Models;
using CoreHtmlToImage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Services
{
    public class ParametersService : IParametersService
    {
        private readonly ApplicationDbContext _context;

        public ParametersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Parameters> FindAll()
        {
            return _context.Parameters;
        }

        public async Task<Parameters> FindById(short id)
        {
            try
            {
                return await _context.Parameters.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Parameters> Update(Parameters parameter)
        {
            try
            {
                Parameters parameterIni = await FindById(parameter.Id);
                parameterIni.Description = parameter.Description;
                parameterIni.Value = parameter.Value;

                _context.Update(parameterIni);
                await _context.SaveChangesAsync();

                return parameter;
            }
            catch (Exception e)
            {
                throw new Exception($"Unable to save changes: {e.Message}");
            }
        }
    }
}
