using CodeNames.Data;
using CodeNames.Enums;
using CodeNames.Interfaces;
using CodeNames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeNames.Services
{
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;

        public GamesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Games> FindAll()
        {
            return _context.Games.ToList();
        }

        public async Task<Games> Generate()
        {
            // Get all words.
            List<int> wordsId = _context.Words.Select(t => t.Id).ToList();
            Games game = new Games()
            {
                ScoreAteam = 0,
                ScoreBteam = 0,
                CreatedAt = DateTime.Now
            };

            // Affect word to team and shuffle.
            List<short> teamsId = new List<short>(new short[] {
                // 1 Loose team.
                (short)TeamsEnums.LooseTeam,

                // 7 Neutral team.
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,
                (short)TeamsEnums.NeutralTeam,

                // 8 Red Team.
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,
                (short)TeamsEnums.RedTeam,

                // 8 Blue Team.
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,
                (short)TeamsEnums.BlueTeam,

                // 1 Extra Blue/Red Team.
                (short)(new Random()).Next(1, 2)
            });

            // Shuffle teams and words.
            teamsId.Shuffle();
            wordsId.Shuffle();

            // Fill randomly games word.
            for (short i = 0; i < 25; i++)
            { 
                game.Gameswords.Add(new Gameswords()
                {
                    TeamId = teamsId[i],
                    WordId = wordsId[i],
                    Order = i,
                    Find = false,
                });
            }

            // Create game.
            await _context.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }
    }
}
