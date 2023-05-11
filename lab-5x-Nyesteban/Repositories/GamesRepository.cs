using Bogus;
using EFCore.BulkExtensions;
using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab_1_Nyesteban.Repositories
{
    public class GamesRepository : IGamesRepository
    {
        private readonly StoreContext _context;

        public GamesRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Game>> GetGames()
        {
            if (_context.Games == null)
            {
                throw new Exception($"Entity set 'StoreContext.Games'  is null.");
            }
            var result = _context.Set<Game>().ToList() as IEnumerable<Game>;
            return Task.FromResult(result);
        }

        public async Task GenerateGames()
        {
            if (_context.Games == null)
            {
                throw new Exception($"Entity set 'StoreContext.Games'  is null.");
            }
            var gameIds = 0;
            var testGames = new Faker<Game>().RuleFor(g => g.ID, f => ++gameIds).RuleFor(g => g.GameName, f => f.Lorem.Word()).RuleFor(g => g.GameDescription, f => f.Lorem.Sentence()).RuleFor(g => g.GameLength, f => f.Random.Int(1,100)).RuleFor(g => g.GameSize, f => f.Random.Int(1, 100000))
                .RuleFor(g => g.GameRating, f => f.Random.Decimal(1,5)).RuleFor(g => g.CompanyID, f => f.Random.Int(1, 100000)).RuleFor(g => g.UserID, f => f.Random.Int(1, 10000));
            var games = testGames.Generate(1000000);
            _context.BulkInsert(games);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<Game>> GetGame(int id)
        {
            if (_context.Games == null)
            {
                throw new Exception($"Entity set 'StoreContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                throw new Exception($"Game with Id={id} does not exist!");
            }

            return game;
        }

        public async Task<IEnumerable<Game>> GetGamesPaginated(int skip, int take)
        {
            var result = await _context.Games.Include(a => a.User).OrderBy(g => g.ID).Skip(skip).Take(take).ToListAsync() as IEnumerable<Game>;
            return result;
        }

        public async Task<Game> PutGame(int id, Game game)
        {
            if (id != game.ID)
            {
                throw new Exception("The ids are different!");
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    throw new Exception("Game does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return game;
        }

        public async Task PostGame(Game game)
        {
            if (_context.Games == null)
            {
                throw new Exception("Entity set 'StoreContext.Games'  is null.");
            }

            if (game is null)
            {
                throw new Exception("Invalid game!");
            }
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task PostGameToOneToMany(int id, Game game)
        {
            if (_context.Games == null)
            {
                throw new Exception("Entity set 'StoreContext.Games'  is null.");
            }
            _context.Games.Add(game);
            var company = await _context.Companies.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            if (company == null)
            {
                throw new Exception("Company is null.");
            }
            company.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGame(int id)
        {
            if (_context.Games == null)
            {
                throw new Exception("Entity set 'StoreContext.Games'  is null.");
            }
            var game = await _context.Games.FindAsync(id);
            if (game == null)
            {
                throw new Exception("Invalid game!");
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
        }

        public async Task BulkDeleteGames()
        {
            if (_context.Games == null)
            {
                throw new Exception($"Entity set 'StoreContext.Games'  is null.");
            }
            var games = _context.Games.ToList();
            _context.BulkDelete(games);
            await _context.SaveChangesAsync();
        }

        private bool GameExists(int id)
        {
            return (_context.Games?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
