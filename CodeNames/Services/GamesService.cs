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
    public class GamesService : IGamesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public GamesService(ApplicationDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public IEnumerable<Games> FindAll()
        {
            return _context.Games.OrderByDescending(x => x.Id);
        }

        public async Task<Games> FindById(int id)
        {
            try
            {
                return await _context.Games.Include(x => x.Gameswords).SingleOrDefaultAsync(x => x.Id == id);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Games> Create(Games games)
        {
            try
            {
                games.NextToPlayTeamId = games.StartTeamId;
                games.CreatedAt = DateTime.Now;

                for (short i = 0; i < (short)games.Gameswords.Count; i++)
                {
                    games.Gameswords.ElementAt(i).Order = i;
                }

                await _context.AddAsync(games);
                await _context.SaveChangesAsync();

                return games;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<Games> Update(Games games)
        {
            try
            {
                // Delete all game words.
                var gameIni = await FindById(games.Id);
                _context.RemoveRange(gameIni.Gameswords);
                await _context.SaveChangesAsync();

                // Update data.
                gameIni.StartTeamId = gameIni.NextToPlayTeamId = games.StartTeamId;
                gameIni.NextToPlayTeamId = games.StartTeamId;
                gameIni.UpdatedAt = DateTime.Now;

                for (short i = 0; i < (short)games.Gameswords.Count; i++)
                {
                    games.Gameswords.ElementAt(i).Order = i;
                }

                gameIni.Gameswords = games.Gameswords;

                _context.Update(gameIni);
                await _context.SaveChangesAsync();

                return gameIni;
            }
            catch (Exception e)
            {
                throw new Exception("Unable to save changes : " + e.Message);
            }
        }

        public async Task<Gameswords> FindByGameWordId(int id, int wordId)
        {
            try
            {
                return await _context.Gameswords.SingleOrDefaultAsync(x => x.GameId == id && x.WordId == wordId);
            }
            catch
            {
                return null;
            }
        }

        public IEnumerable<GamesView> FindViewGamesById(int id)
        {
            try
            {
                return _context.GamesView.Where(x => x.GameId == id).OrderBy(x => x.Order);
            }
            catch
            {
                return null;
            }
        }

        public async Task<Games> Generate(int[] themes)
        {
            // Get all words.
            List<int> wordsId = (themes == null || themes.Count() == 0) 
                ? _context.Words.Select(t => t.Id).ToList() 
                : _context.Themeswords.Where(x => themes.Contains(x.ThemeId)).Select(group => group.WordId).Distinct().ToList();

            // Check if the number of words if sufficient.
            if (wordsId.Count < 25)
            {
                throw new Exception("Number of words for selected themes is not sufficient for generating a new game. Please add extra words or themes");
            }

            // Initialize game.
            Games game = new Games()
            {
                ScoreBlueTeam = 0,
                ScoreRedTeam = 0,
                RoundBlueTeam = 0,
                RoundRedTeam = 0,
                CreatedAt = DateTime.Now
            };

            // Affect word to team and shuffle.
            short extraTeamId = (short)(new Random()).Next(1, 3);
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
                extraTeamId
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
            game.StartTeamId = extraTeamId;
            game.NextToPlayTeamId = extraTeamId;
            await _context.AddAsync(game);
            await _context.SaveChangesAsync();

            return game;
        }

        public string GridColor(int id, string url)
        {
            string fileGridColor = $"{_hostingEnvironment.WebRootPath}\\grids\\grid-{id}.png";

            var converter = new HtmlConverter();
            //var bytes = converter.FromUrl("http://google.com");
            var bytes = converter.FromUrl(url, 500, ImageFormat.Png, 100);
            File.WriteAllBytes(fileGridColor, bytes);

            return fileGridColor;
        }

        public async Task<Games> FoundWord(int id, int wordId, short? teamId)
        {
            // Word is found.
            Gameswords gameWord = await FindByGameWordId(id, wordId);
            gameWord.Find = true;
            _context.Update(gameWord);

            // Update point game.
            //TODO: See Points (?).
            Games game = await FindById(id);
            if (gameWord.TeamId == teamId)
            {
                if (teamId == (short)TeamsEnums.BlueTeam)
                {
                    game.ScoreBlueTeam++;
                }
                else
                {
                    game.ScoreRedTeam++;
                }
            }
            else
            {

            }

            await _context.SaveChangesAsync();

            return game;
        }

        public async Task<bool> Delete(Games games)
        {
            try
            {
                _context.Remove(games);
                await _context.SaveChangesAsync();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
