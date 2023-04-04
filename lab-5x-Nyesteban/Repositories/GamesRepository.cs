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

        private bool GameExists(int id)
        {
            return (_context.Games?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
