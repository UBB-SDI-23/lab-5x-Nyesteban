using lab_1_Nyesteban.DAL;
using lab_1_Nyesteban.Repositories.Interfaces;
using lab_5x_Nyesteban.DTOs;
using lab_5x_Nyesteban.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Cryptography;
using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using lab_1_Nyesteban.Models;

namespace lab_5x_Nyesteban.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserDTORegister request)
        {
            //User user = new User();
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var check = await _context.Users.SingleOrDefaultAsync(a => a.Username == request.Username);
            if(check != null)
                return BadRequest("Username is already in use!");
            if (request.Bio == null || request.Location == null || request.RealName == null || request.EMail == null || request.Website == null)
                return BadRequest("One of the fields is null!");
            if(request.Bio.Length <= 3)
                return BadRequest("Bio is too short!");
            if (request.Location.Length <= 3)
                return BadRequest("Location is too short!");
            if (request.RealName.Length <= 1)
                return BadRequest("Name is too short!");
            if (request.EMail.Length <= 3)
                return BadRequest("E-Mail is too short!");
            if (request.Website.Length <= 2)
                return BadRequest("Website is too short!");
            if(request.EMail.ToLower().Contains('@') == false)
                return BadRequest("E-Mail must contain at least one @!");
            if (request.Website.ToLower().Contains('.') == false)
                return BadRequest("E-Mail must contain at least one .!");
            /*
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.Bio = request.Bio;
            user.Location = request.Location;
            user.RealName = request.RealName;
            user.EMail = request.EMail;
            user.Website = request.Website;
            */
            var randomNumber = new byte[32];
            string refreshToken = "";
            RandomNumberGenerator.Create().GetBytes(randomNumber);
            refreshToken = Convert.ToBase64String(randomNumber);
            refreshToken = refreshToken.Replace("/","");
            var code = refreshToken;
            var time = DateTime.Now;
            var reg = new RegistrationCode();
            reg.code = code;
            reg.generatedTime = time;
            _context.RegistrationCodes.Add(reg);
            //_context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(code);
        }

        [HttpPost("register/confirm/{confirmation_code}")]
        public async Task<ActionResult<User>> RegisterConfirm(UserDTORegister request, string confirmation_code)
        {
            int checker = 0;
            List<RegistrationCode> deleter = new List<RegistrationCode>();
            foreach (var reg in _context.RegistrationCodes)
                if (DateTime.Now - reg.generatedTime > new TimeSpan(0, 10, 0))
                {
                    deleter.Add(reg);
                }
            foreach (var reg in deleter)
            {
                _context.RegistrationCodes.Remove(reg);
                await _context.SaveChangesAsync();
            }
            foreach (var reg in _context.RegistrationCodes)
                if (reg.code.Trim() == confirmation_code.Trim())
                {
                    checker = 1;
                    break;
                }
            if(checker == 0)
                return BadRequest("Wrong code!");
            var check = await _context.Users.SingleOrDefaultAsync(a => a.Username == request.Username);
            if (check != null)
                return BadRequest("Username is already in use!");
            if (request.Bio == null || request.Location == null || request.RealName == null || request.EMail == null || request.Website == null)
                return BadRequest("One of the fields is null!");
            if (request.Bio.Length <= 3)
                return BadRequest("Bio is too short!");
            if (request.Location.Length <= 3)
                return BadRequest("Location is too short!");
            if (request.RealName.Length <= 1)
                return BadRequest("Name is too short!");
            if (request.EMail.Length <= 3)
                return BadRequest("E-Mail is too short!");
            if (request.Website.Length <= 2)
                return BadRequest("Website is too short!");
            if (request.EMail.ToLower().Contains('@') == false)
                return BadRequest("E-Mail must contain at least one @!");
            if (request.Website.ToLower().Contains('.') == false)
                return BadRequest("E-Mail must contain at least one .!");
            User user = new User();
            int maxid = await _context.Users.MaxAsync(e => e.ID);
            user.ID = ++maxid;
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.Bio = request.Bio;
            user.Location = request.Location;
            user.RealName = request.RealName;
            user.EMail = request.EMail;
            user.Website = request.Website;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(UserDTOLogin request)
        {
            var user = await _context.Users.SingleOrDefaultAsync(a => a.Username == request.Username);
            if (user == null)
                return BadRequest("User not found!");
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                return BadRequest("Bad password!");
            string token = CreateToken(user);
            return Ok(token);
        }

        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            int total = 0;
            var user = await _context.Users.SingleOrDefaultAsync(a => a.ID == id);
            if (user == null)
                return BadRequest("User not found!");
            return Ok(user);
        }

        [HttpGet("getuser/{id}/count")]
        public async Task<ActionResult<User>> GetUserEntriesCount(int id)
        {
            int total = 0;
            var userapps = await _context.Users.Include(a => a.Apps)/*.Include(a=>a.Games).Include(a => a.Apps).Include(a => a.DevelopmentDetails).Include(a => a.Apps)*/.SingleOrDefaultAsync(a => a.ID == id);
            if (userapps == null)
                return BadRequest("User not found!");
            var usergames = await _context.Users.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            var userdevs = await _context.Users.Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id);
            var usercompanies = await _context.Users.Include(a => a.Companies).SingleOrDefaultAsync(a => a.ID == id);
            total = userapps.Apps.Count() + usergames.Games.Count() + userdevs.DevelopmentDetails.Count() + usercompanies.Companies.Count();
            return Ok(total);
        }

        [HttpGet("getuser/{id}/appcount")]
        public async Task<ActionResult<User>> GetUserAppsCount(int id)
        {
            int total = 0;
            var userapps = await _context.Users.Include(a => a.Apps)/*.Include(a=>a.Games).Include(a => a.Apps).Include(a => a.DevelopmentDetails).Include(a => a.Apps)*/.SingleOrDefaultAsync(a => a.ID == id);
            if (userapps == null)
                return BadRequest("User not found!");
            total = userapps.Apps.Count();
            return Ok(total);
        }

        [HttpGet("getuser/{id}/gamecount")]
        public async Task<ActionResult<User>> GetUserGamesCount(int id)
        {
            int total = 0;
            var usergames = await _context.Users.Include(a => a.Games).SingleOrDefaultAsync(a => a.ID == id);
            if (usergames == null)
                return BadRequest("User not found!");
            total = usergames.Games.Count();
            return Ok(total);
        }

        [HttpGet("getuser/{id}/devcount")]
        public async Task<ActionResult<User>> GetUserDevCount(int id)
        {
            int total = 0;
            var userdev = await _context.Users.Include(a => a.DevelopmentDetails).SingleOrDefaultAsync(a => a.ID == id);
            if (userdev == null)
                return BadRequest("User not found!");
            total = userdev.DevelopmentDetails.Count();
            return Ok(total);
        }

        [HttpGet("getuser/{id}/companycount")]
        public async Task<ActionResult<User>> GetUserCompaniesCount(int id)
        {
            int total = 0;
            var usercompanies = await _context.Users.Include(a => a.Companies).SingleOrDefaultAsync(a => a.ID == id);
            if (usercompanies == null)
                return BadRequest("User not found!");
            total = usercompanies.Companies.Count();
            return Ok(total);
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
