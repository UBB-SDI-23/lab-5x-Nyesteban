using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using System.Runtime.ConstrainedExecution;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using lab_1_Nyesteban.DTOs;
using System.ComponentModel.Design;
using lab_1_Nyesteban.Repositories.Interfaces;
using System.Data.Entity;
using lab_1_Nyesteban.Validators;

namespace lab_1_Nyesteban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private ICompaniesRepository _repo;

        public CompaniesController(ICompaniesRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Companies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Company>>> GetCompanies()
        {
            try
            {
                return Ok(await _repo.GetCompanies());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/Companies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Company>> GetCompany(int id)
        {
            try
            {
                return await _repo.GetCompany(id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // PUT: api/Companies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompany(int id, Company company)
        {
            try
            {
                CompanyValidator.ValidateCompany(company);
                return Ok(await _repo.PutCompany(id, company));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}/game")]
        public async Task<IActionResult> PutCompanyByNewGame(int id, Game game)
        {
            try
            {
                return Ok(await _repo.PutCompanyByNewGame(id, game));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}/game/{gameid}")]
        public async Task<IActionResult> PutCompanyGameId(int id, int gameid)
        {
            try
            {
                return Ok(await _repo.PutCompanyGameId(id, gameid));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPut("{id}/games")]
        public async Task<IActionResult> PutCompanyGameIds(int id, List<int> gameids)
        {
            try
            {
                return Ok(await _repo.PutCompanyGameIds(id, gameids));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // POST: api/Companies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Company>> PostCompany(Company company)
        {
            try
            {
                CompanyValidator.ValidateCompany(company);
                await _repo.PostCompany(company);
                return CreatedAtAction("GetCompany", new { id = company.ID }, company);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        /*
        [HttpPost("{id1}/{id2}/App")]
        public async Task<ActionResult<Company>> PostDevelopmentDetail(int id1, int id2)
        {
            var app = await _context.Apps.SingleOrDefaultAsync(a => a.ID == id1);
            if (app == null)
            {
                return Problem("App is null.");
            }
            DevelopmentDetail adder = new DevelopmentDetail();
            adder.App = app;
            var company = await _context.Companies.SingleOrDefaultAsync(a => a.ID == id2);
            if (company == null)
            {
                return Problem("App is null.");
            }
            adder.Company = company;
            adder.AppId = id1;
            adder.CompanyId = id2;
            app = await _context.Apps.Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id1);
            company = await _context.Companies.Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id2);
            company.DevelopmentDetails.Add(adder);
            app.DevelopmentDetails.Add(adder);
            _context.DevelopmentDetails.Add(adder);
            await _context.SaveChangesAsync();
            return NoContent();
        }*/

        // DELETE: api/Companies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _repo.DeleteCompany(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("GameRating")]
        public async Task<IEnumerable<CompanyGamesDTO>> GetCompaniesOrderedByAverageGameRating()
        {
            //return await _repo.GetCompaniesOrderedByAverageGameRating();
            var result = (await _repo.GetCompaniesAllData())
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

        [HttpGet("AppCount")]
        public async Task<IEnumerable<CompanyAppsDTO>> GetCompaniesOrderedByAppCount()
        {
            var result = (await _repo.GetCompaniesAllData())
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
    }
}
