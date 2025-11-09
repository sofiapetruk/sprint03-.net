using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace Sprint02.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AuthController : Controller
    {
        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Login do Usuário e Geração de Token JWT",
            Description = "Endpoint para autenticar um usuário (simulado) e gerar um token de acesso JWT válido por 1 hora."
        )]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult Login()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("zBrWZoQJkbVlun_66TN0hx_cN83mrwFm8y3AH9RG5f8X9AyqyrXMe-lLdUV5l2qE");

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, "user") }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new { Token = tokenString });
        }
    }
}
