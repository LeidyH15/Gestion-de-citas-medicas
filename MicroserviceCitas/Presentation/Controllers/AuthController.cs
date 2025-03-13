using MicroserviceCitas.Application.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace MicroserviceCitas.Presentation.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private static readonly string SecretKey = ConfigurationManager.AppSettings["JwtSecretKey"];
        private static readonly byte[] _key;

        static AuthController()
        {
            if (string.IsNullOrEmpty(SecretKey) || SecretKey.Length < 16)
            {
                throw new Exception("La clave secreta debe tener al menos 16 caracteres y estar configurada en web.config.");
            }
            _key = Encoding.UTF8.GetBytes(SecretKey);
        }

        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login([FromBody] LoginDTO login)
        {
            if (login == null || login.Username != "admin" || login.Password != "inetum")
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, login.Username),
                    new Claim(ClaimTypes.Role, "Admin")
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new { Token = tokenHandler.WriteToken(token) });
        }
    }
}
