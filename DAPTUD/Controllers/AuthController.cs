using DAPTUD.Models;
using DAPTUD.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAPTUD.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/<NguoiDungController>
        private readonly NguoiDungService cusService;
        private readonly ShipperService shipperService;

        public AuthController(NguoiDungService _cusService, ShipperService _shipperService)
        {
            cusService = _cusService;
            shipperService = _shipperService;
        }

        [HttpPost, Route("login")]
        public IActionResult Login([FromBody]NguoiDung user)
        {
            if(user == null)
            {
                return BadRequest("Invalid client request");
            }
            var checkCusExist = cusService.GetUserByEmailAndPassword(user.email, user.matKhau).Result;
            if (checkCusExist != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                string role = "";
                switch (checkCusExist.loaiND)
                {
                    case 1: role = "User"; break;
                    case 2: role = "Shop"; break;
                    case 3: role = "Admin"; break;

                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44349",
                    audience: "https://localhost:44349",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, User = checkCusExist });
            }
            return Unauthorized();
        }

        [HttpPost, Route("register")]
        public IActionResult Register([FromBody] NguoiDung user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var checkCusExist = cusService.GetUserByEmail(user.email).Result;

            if(checkCusExist != null)
            {
                return BadRequest("Email Already exist");
            }
            else
            {
                user.loaiND = 1;
                var newCust = cusService.CreateAsync(user);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                string role = "";
                switch (user.loaiND)
                {
                    case 1: role = "User"; break;
                    case 2: role = "Shop"; break;
                    case 3: role = "Admin"; break;

                }
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.email),
                    new Claim(ClaimTypes.Role, role)
                };

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44349",
                    audience: "https://localhost:44349",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, User = checkCusExist });
            }
        }

        [HttpPost, Route("shipper/login")]
        public IActionResult LoginShipper([FromBody] Shipper user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }
            var checkCusExist = shipperService.GetUserByEmailAndPassword(user.email, user.matKhau).Result;
            if (checkCusExist != null)
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44349",
                    audience: "https://localhost:44349",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, User = checkCusExist });
            }
            return Unauthorized();
        }

        [HttpPost, Route("shipper/register")]
        public IActionResult RegisterShipper([FromBody] Shipper user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var checkCusExist = cusService.GetUserByEmail(user.email).Result;

            if (checkCusExist != null)
            {
                return BadRequest("Email Already exist");
            }
            else
            {
                var newCust = shipperService.CreateAsync(user);
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


                var tokeOptions = new JwtSecurityToken(
                    issuer: "https://localhost:44349",
                    audience: "https://localhost:44349",
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signinCredentials
                );
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(new { Token = tokenString, User = checkCusExist });
            }
        }
    }
}
