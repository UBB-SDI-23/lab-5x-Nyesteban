using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab_1_Nyesteban.Repositories
{
    public class AppsRepository : IAppsRepository
    {
        private readonly StoreContext _context;

        public AppsRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<App>> GetApps()
        {
            if (_context.Apps == null)
            {
                throw new Exception($"Entity set 'StoreContext.Apps'  is null.");
            }
            var result = _context.Set<App>().ToList() as IEnumerable<App>;
            return Task.FromResult(result);
        }

        public async Task<App> GetAppById(int id)
        {
            if (_context.Apps == null)
            {
                throw new Exception($"Entity set 'StoreContext.Apps'  is null.");
            }
            var result = await _context.Apps.Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id);
            if (result is null)
            {
                throw new Exception($"App with Id={id} does not exist!");
            }

            return result;
        }

        public async Task<IEnumerable<App>> GetAppsPaginated(int skip, int take)
        {
            var result = await _context.Apps.Include(a => a.DevelopmentDetails).OrderBy(a => a.ID).Skip(skip).Take(take).ToListAsync() as IEnumerable<App>;
            return result;
        }

        public async Task<ActionResult<IEnumerable<App>>> GetAppsWithSizeLargerThan(int appSize)
        {
            if (_context.Apps == null)
            {
                throw new Exception($"Entity set 'StoreContext.Apps'  is null.");
            }
            var apps = from a in _context.Apps select a;
            apps = apps.Where(a => a.AppSize > appSize);
            return await apps.AsNoTracking().Take(50).ToListAsync();
        }

        public async Task AddApp(App app)
        {
            if (_context.Apps == null)
            {
                throw new Exception("Entity set 'StoreContext.Apps'  is null.");
            }
            if (app is null)
            {
                throw new Exception("Invalid app!");
            }

            _context.Set<App>().Add(app);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveApp(int id)
        {
            if (_context.Apps == null)
            {
                throw new Exception("Entity set 'StoreContext.Apps'  is null.");
            }

            var app = await GetAppById(id);

            if (app is null)
            {
                throw new Exception($"App with Id={id} does not exist!");
            }

            _context.Remove(app);
            await _context.SaveChangesAsync();
        }

        public async Task<App> UpdateApp(int id, App app)
        {
            if (id != app.ID)
            {
                throw new Exception("The ids are different!");
            }

            _context.Entry(app).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppExists(id))
                {
                    throw new Exception("App does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return app;
        }

        private bool AppExists(int id)
        {
            return (_context.Apps?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
