using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Entity;
using System.ComponentModel.Design;

namespace lab_1_Nyesteban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevelopmentDetailsController : ControllerBase
    {
        private IDevelopmentDetailsRepository _repo;

        public DevelopmentDetailsController(IDevelopmentDetailsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/DevelopmentDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DevelopmentDetail>>> GetDevelopmentDetails()
        {
            try
            {
                return Ok(await _repo.GetDevelopmentDetails());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/DevelopmentDetails/5
        [HttpGet("{id1}/{id2}")]
        public async Task<ActionResult<DevelopmentDetail>> GetDevelopmentDetail(int id1, int id2)
        {
            try
            {
                return await _repo.GetDevelopmentDetail(id1, id2);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

            // PUT: api/DevelopmentDetails/5
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPut("{id1}/{id2}")]
            public async Task<IActionResult> PutDevelopmentDetail(int id1, int id2, DevelopmentDetail developmentDetail)
            {
                try
                {
                    return Ok(await _repo.PutDevelopmentDetail(id1, id2, developmentDetail));
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
            }

            // POST: api/DevelopmentDetails
            // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
            [HttpPost]
            public async Task<ActionResult<DevelopmentDetail>> PostDevelopmentDetail(DevelopmentDetail developmentDetail)
            {
                try
                {
                    await _repo.PostDevelopmentDetail(developmentDetail);
                    return Ok();
                }
                catch (Exception e)
                {
                    return Problem(e.Message);
                }
                /*
                try
                {
                   await _context.SaveChangesAsync(); 
                }
                catch (DbUpdateException)
                {
                    if (DevelopmentDetailExists(developmentDetail.CompanyId))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }*/

                //return CreatedAtAction("GetDevelopmentDetail", new { id = developmentDetail.CompanyId }, developmentDetail);
            }

            [HttpGet("{companyId}/Apps")]
            public Task<IEnumerable<App>> GetAppsByCompanyId(int companyId)
            {
                return _repo.GetAppsByCompanyId(companyId);
            }

            [HttpGet("{appId}/Companies")]
            public Task<IEnumerable<Company>> GetCompaniesByAppId(int appId)
            {
            return _repo.GetCompaniesByAppId(appId);
        }

        // DELETE: api/DevelopmentDetails/5
        [HttpDelete("{id1}/{id2}")]
        public async Task<IActionResult> DeleteDevelopmentDetail(int id1, int id2)
        {
            try
            {
                await _repo.DeleteDevelopmentDetail(id1,id2);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
        
    }
}
