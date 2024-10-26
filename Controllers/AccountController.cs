using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webApi_build_Real.ApplictionContext;
using webApi_build_Real.Dto;
using webApi_build_Real.Models;
using webApi_build_Real.Repository.implementation;

namespace webApi_build_Real.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
       
        private readonly IUnitOfWork _now;

        public AccountController(IUnitOfWork now, IConfiguration builder)
        {
            _now = now;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginReDto loginReq)
        {
            var user = await _now.UserRepository.Authenticate(loginReq.UserName, loginReq.Password);
            if (user == null)
            {
                return Unauthorized("Invalid user or password");
            }
            var loginRes = new LoginReDto();
            loginRes.UserName = loginReq.UserName;
            loginRes.Token = CreateJWT(user);
            return Ok(loginRes);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(LoginReDto loginReq)
        {
           if(await _now.UserRepository.UserAlreadyExists(loginReq.UserName))
            {
                return BadRequest("User already exists, please tey somthing else");
            }
            else
            {
                _now.UserRepository.Register(loginReq.UserName, loginReq.Password);
                await _now.SaveAsync();
                return StatusCode(201);
            }
        }

//create jwt token in this..?->
        private string CreateJWT(Usercs user)
        {
       //  var secretKey = GetSection("AppSettings:Key").Value;
         //var key = new SymmetricSecurityKey(Encoding.UTF8
         //   .GetBytes(secretKey));
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Name,user.UserName),
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            //var singingCredentials =new SigningCredentials(
            //           key,SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1),
             //   SigningCredentials = singingCredentials
            };
            var tokenHandandler = new JwtSecurityTokenHandler();
            var token = tokenHandandler.CreateToken(tokenDescriptor);
            return tokenHandandler.WriteToken(token);
        }
    }
}
