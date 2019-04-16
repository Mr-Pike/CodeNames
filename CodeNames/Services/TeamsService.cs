using CodeNames.Data;
using CodeNames.Interfaces;
using CodeNames.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly ApplicationDbContext _context;

        public TeamsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Teams> FindAll()
        {
            return _context.Teams;
        }

        public async Task<Teams> FindById(int id)
        {
            try
            {
                return await _context.Teams.SingleOrDefaultAsync(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Teams> Update(Teams teams)
        {
            _context.Update(teams);
            await _context.SaveChangesAsync();

            return teams;
        }

        public List<SelectListItem> SelectList(bool realTeam)
        {
            IEnumerable<Teams> teams = FindAll();

            if (realTeam)
            {
                teams = teams.Where(x => x.Id < 3);
            }

            List<SelectListItem> listSelectListItem = new List<SelectListItem>();

            foreach (var team in teams)
            {
                listSelectListItem.Add(new SelectListItem()
                {

                    Text = team.Name,
                    Value = team.Id.ToString()
                });
            }

            return listSelectListItem;
        }
    }
}
