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
using lab_1_Nyesteban.Validators;
using Microsoft.AspNetCore.Authorization;

namespace lab_1_Nyesteban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private IGamesRepository _repo;

        public GamesController(IGamesRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            try
            {
                return Ok(await _repo.GetGames());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            try
            {
                return await _repo.GetGame(id);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet("paginated/{skip}/{take}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGamesPaginated([FromRoute] uint skip,
        [FromRoute] uint take)
        {
            var result = await _repo.GetGamesPaginated((int)skip, (int)take);
            return Ok(result);
        }

        // PUT: api/Games/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "moderator,admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            try
            {
                GameValidator.ValidateGame(game);
                return Ok(await _repo.PutGame(id, game));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize]
        [HttpPut("{id}/sameuser")]
        public async Task<IActionResult> PutGameByUser(int id, Game game)
        {
            try
            {
                GameValidator.ValidateGame(game);
                return Ok(await _repo.PutGame(id, game));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // POST: api/Games
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            try
            {
                GameValidator.ValidateGame(game);
                await _repo.PostGame(game);
                return CreatedAtAction("GetGame", new { id = game.ID }, game);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Game>> PostGameToOneToMany(int id, Game game)
        {
            try
            {
                GameValidator.ValidateGame(game);
                await _repo.PostGameToOneToMany(id, game);
                return CreatedAtAction("GetGame", new { id = game.ID }, game);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        // DELETE: api/Games/5
        [Authorize(Roles = "moderator,admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            try
            {
                await _repo.DeleteGame(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize]
        [HttpDelete("{id}/sameuser")]
        public async Task<IActionResult> DeleteGameByUser(int id)
        {
            try
            {
                await _repo.DeleteGame(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("bulk")]
        public async Task<IActionResult> BulkDeleteGames()
        {
            try
            {
                await _repo.BulkDeleteGames();
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpPost("bulk")]
        public async Task<IActionResult> GenerateGames()
        {
            try
            {
                await _repo.GenerateGames();
                return NoContent();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
