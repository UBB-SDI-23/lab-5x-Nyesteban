using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Models;
using lab_1_Nyesteban.Repositories;
using lab_1_Nyesteban.Repositories.Interfaces;
using lab_1_Nyesteban.Validators;

namespace lab_1_Nyesteban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        private IAppsRepository _repo;

        public AppsController(IAppsRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Apps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<App>>> GetApps()
        {
            try
            {
                return Ok(await _repo.GetApps());
            }
            catch(Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/Apps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<App>> GetApp(int id)
        {
            try
            {
                return Ok(await _repo.GetAppById(id));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("paginated/{skip}/{take}")]
        public async Task<ActionResult<IEnumerable<App>>> GetAppsPaginated([FromRoute] uint skip,
        [FromRoute] uint take)
        {
            var result = await _repo.GetAppsPaginated((int)skip, (int)take);
            return Ok(result);
        }


        [HttpGet("{appSize}/filter")]
        public async Task<ActionResult<IEnumerable<App>>> GetAppsWithSizeLargerThan(int appSize)
        {
            try
            {
                return await _repo.GetAppsWithSizeLargerThan(appSize);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // PUT: api/Apps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutApp(int id, App app)
        {
            try
            {
                AppValidator.ValidateApp(app);
                return Ok(await _repo.UpdateApp(id,app));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // POST: api/Apps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<App>> PostApp(App app)
        {
            try
            {
                AppValidator.ValidateApp(app);
                await _repo.AddApp(app);
                return CreatedAtAction("GetApp", new { id = app.ID }, app);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // DELETE: api/Apps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApp(int id)
        {
            try
            {
                await _repo.RemoveApp(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
