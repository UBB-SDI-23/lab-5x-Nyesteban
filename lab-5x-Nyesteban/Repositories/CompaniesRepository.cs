using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.DTOs;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab_1_Nyesteban.Repositories
{
    public class CompaniesRepository : ICompaniesRepository
    {
        private readonly StoreContext _context;

        public CompaniesRepository(StoreContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Company>> GetCompanies()
        {
            if (_context.Companies == null)
            {
                throw new Exception($"Entity set 'StoreContext.Companies'  is null.");
            }
            var result = _context.Set<Company>().Take(100).ToList() as IEnumerable<Company>;
            return Task.FromResult(result);
        }

        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            if (_context.Companies == null)
            {
                throw new Exception($"Entity set 'StoreContext.Companies'  is null.");
            }

            var company = await _context.Companies.Include(a => a.Games).Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id);

            if (company == null)
            {
                throw new Exception($"Company with Id={id} does not exist!");
            }

            return company;
        }

        public Task<IEnumerable<Company>> GetCompaniesAllData()
        {
            var result = _context.Set<Company>().Include(c => c.Games).ToList() as IEnumerable<Company>;
            return Task.FromResult(result);
        }

        public async Task<Company> PutCompany(int id, Company company)
        {
            if (id != company.ID)
            {
                throw new Exception("The ids are different!");
            }

            _context.Entry(company).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    throw new Exception($"Company with Id={id} does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return company;
        }

        public async Task<Company> PutCompanyByNewGame(int id, Game game)
        {
            var company = await _context.Companies.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            if (company == null)
            {
                throw new Exception("Company is null.");
            }
            company.Games.Add(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    throw new Exception($"Company with Id={id} does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return company;
        }

        public async Task<Company> PutCompanyGameId(int id, int gameid)
        {
            var company = await _context.Companies.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            if (company == null)
            {
                throw new Exception("Company is null.");
            }
            var game = await _context.Games.SingleOrDefaultAsync(a => a.ID == gameid);
            if (game == null)
            {
                throw new Exception("Game is null.");
            }
            company.Games.Add(game);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    throw new Exception($"Company with Id={id} does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return company;
        }

        public async Task<Company> PutCompanyGameIds(int id, List<int> gameids)
        {
            var company = await _context.Companies.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            if (company == null)
            {
                throw new Exception("Company is null.");
            }
            foreach (int gameid in gameids)
            {
                var game = await _context.Games.SingleOrDefaultAsync(a => a.ID == gameid);
                if (game == null)
                {
                    throw new Exception("Game is null.");
                }
                company.Games.Add(game);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompanyExists(id))
                {
                    throw new Exception($"Company with Id={id} does not exist!");
                }
                else
                {
                    throw;
                }
            }

            return company;
        }

        public async Task PostCompany(Company company)
        {
            if (_context.Companies == null)
            {
                throw new Exception("Entity set 'StoreContext.Companies'  is null.");
            }
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCompany(int id)
        {
            if (_context.Companies == null)
            {
                throw new Exception($"Entity set 'StoreContext.Companies'  is null.");
            }
            var company = await _context.Companies.FindAsync(id);
            if (company == null)
            {
                throw new Exception($"Company with Id={id} does not exist!");
            }

            _context.Companies.Remove(company);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyGamesDTO>> GetCompaniesOrderedByAverageGameRating()
        {
            var result = (await GetCompaniesAllData())
                .OrderBy(c => c.Games?.Select(g => g.GameRating).DefaultIfEmpty().Average())
                .Select(c => new CompanyGamesDTO
                {
                    CompanyName = c.CompanyName,
                    CompanyDescription = c.CompanyDescription,
                    CompanyRevenue = c.CompanyRevenue,
                    CompanyEstablishmentYear = c.CompanyEstablishmentYear,
                    CompanyRating = c.CompanyRating,
                    AverageGameRating = c.Games?.Select(g => g.GameRating).DefaultIfEmpty().Average() ?? 0
                });
            return result;
        }

        public async Task<IEnumerable<CompanyAppsDTO>> GetCompaniesOrderedByAppCount()
        {

            var result = (await _context.Companies.Include(a => a.DevelopmentDetails).ToListAsync())
                .OrderBy(c => c.DevelopmentDetails?.Where(dd => dd.CompanyId == c.ID).Count())
                .Select(c => new CompanyAppsDTO
                {
                    CompanyName = c.CompanyName,
                    CompanyDescription = c.CompanyDescription,
                    CompanyRevenue = c.CompanyRevenue,
                    CompanyEstablishmentYear = c.CompanyEstablishmentYear,
                    CompanyRating = c.CompanyRating,
                    CompanyAppCount = c.DevelopmentDetails?.Where(dd => dd.CompanyId == c.ID).Count() ?? 0
                });
            return result;
        }

        private bool CompanyExists(int id)
        {
            return (_context.Companies?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
